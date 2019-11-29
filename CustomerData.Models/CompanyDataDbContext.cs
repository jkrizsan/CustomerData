using CompanyData.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyData.Data
{
    public class CompanyDataDbContext : DbContext
    {
        public CompanyDataDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Report> Reports { get; set; }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var reportEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(reportEntries);
            return result;
        }

        private List<ReportEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var reportEntries = new List<ReportEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Report || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                var reportEntry = new ReportEntry(entry);
                reportEntry.TableName = entry.Metadata.GetTableName();
                reportEntries.Add(reportEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        reportEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        reportEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            reportEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            reportEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                reportEntry.OldValues[propertyName] = property.OriginalValue;
                                reportEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            // Save report entities that have all the modifications
            foreach (var reportEntry in reportEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Reports.Add(reportEntry.ToReport());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return reportEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<ReportEntry> reportEntries)
        {
            if (reportEntries == null || reportEntries.Count == 0)
            {
                return Task.CompletedTask;
            }

             foreach (var reportEntry in reportEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in reportEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        reportEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        reportEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Report entry
                Reports.Add(reportEntry.ToReport());
            }

            return SaveChangesAsync();
        }
    }

    
}
