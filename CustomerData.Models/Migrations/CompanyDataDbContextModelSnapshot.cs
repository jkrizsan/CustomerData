﻿// <auto-generated />
using System;
using CompanyData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyData.Data.Migrations
{
    [DbContext(typeof(CompanyDataDbContext))]
    partial class CompanyDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CompanyData.Data.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfContacts")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfOrders")
                        .HasColumnType("int");

                    b.Property<double>("TotalIncome")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Companys");
                });

            modelBuilder.Entity("CompanyData.Data.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Income")
                        .HasColumnType("float");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumnerOfOrders")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("CompanyData.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContactId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("OrderPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CompanyData.Data.Models.Contact", b =>
                {
                    b.HasOne("CompanyData.Data.Models.Company", null)
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("CompanyData.Data.Models.Order", b =>
                {
                    b.HasOne("CompanyData.Data.Models.Contact", null)
                        .WithMany("Orders")
                        .HasForeignKey("ContactId");
                });
#pragma warning restore 612, 618
        }
    }
}
