using CompanyData.Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyData.Data
{
    public class ReportEntry
    {
        public ReportEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Report ToReport()
        {
            var report = new Report();
            report.TableName = TableName;
            report.DateTime = DateTime.UtcNow;
            report.KeyValues = JsonConvert.SerializeObject(KeyValues);
            report.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            report.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            return report;
        }

    }
}
