﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace WebProjekat.Migrations
{
    [DbContext(typeof(ProdavnicaRacunaraContext))]
    [Migration("20211130225616_Web1")]
    partial class Web1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Configuration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CPUID")
                        .HasColumnType("int");

                    b.Property<int?>("GPUID")
                        .HasColumnType("int");

                    b.Property<int?>("MBID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int?>("RAMID")
                        .HasColumnType("int");

                    b.Property<int?>("STORAGEID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CPUID");

                    b.HasIndex("GPUID");

                    b.HasIndex("MBID");

                    b.HasIndex("RAMID");

                    b.HasIndex("STORAGEID");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("Models.Customer.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("ID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Models.Customer.CustomerContacts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.ToTable("CustomerContacts");
                });

            modelBuilder.Entity("Models.Parts.GraphicsCard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int?>("MemoryGB")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("ID");

                    b.ToTable("GraphicsCards");
                });

            modelBuilder.Entity("Models.Parts.Motherboard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("ID");

                    b.ToTable("Motherboards");
                });

            modelBuilder.Entity("Models.Parts.Processor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Cores")
                        .HasColumnType("int");

                    b.Property<double?>("FrequencyGHz")
                        .HasColumnType("float");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("ID");

                    b.ToTable("Processors");
                });

            modelBuilder.Entity("Models.Parts.RAM", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FrequencyMHz")
                        .HasColumnType("int");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("MemoryGB")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("ID");

                    b.ToTable("RAMs");
                });

            modelBuilder.Entity("Models.Parts.Storage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("MemoryGB")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("ID");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("Models.Purchase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ConfigurationID")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int?>("StoreID")
                        .HasColumnType("int");

                    b.Property<int>("VendorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ConfigurationID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("StoreID");

                    b.HasIndex("VendorID");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Models.Store", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("ID");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Models.Vendor.Vendor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<int?>("StoreID")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("ID");

                    b.HasIndex("StoreID");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Models.Vendor.VendorContacts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VendorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("VendorID");

                    b.ToTable("VendorContacts");
                });

            modelBuilder.Entity("Models.Configuration", b =>
                {
                    b.HasOne("Models.Parts.Processor", "CPU")
                        .WithMany()
                        .HasForeignKey("CPUID");

                    b.HasOne("Models.Parts.GraphicsCard", "GPU")
                        .WithMany()
                        .HasForeignKey("GPUID");

                    b.HasOne("Models.Parts.Motherboard", "MB")
                        .WithMany()
                        .HasForeignKey("MBID");

                    b.HasOne("Models.Parts.RAM", "RAM")
                        .WithMany()
                        .HasForeignKey("RAMID");

                    b.HasOne("Models.Parts.Storage", "STORAGE")
                        .WithMany()
                        .HasForeignKey("STORAGEID");

                    b.Navigation("CPU");

                    b.Navigation("GPU");

                    b.Navigation("MB");

                    b.Navigation("RAM");

                    b.Navigation("STORAGE");
                });

            modelBuilder.Entity("Models.Customer.CustomerContacts", b =>
                {
                    b.HasOne("Models.Customer.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Models.Purchase", b =>
                {
                    b.HasOne("Models.Configuration", "Configuration")
                        .WithMany()
                        .HasForeignKey("ConfigurationID");

                    b.HasOne("Models.Customer.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.HasOne("Models.Store", null)
                        .WithMany("Purchases")
                        .HasForeignKey("StoreID");

                    b.HasOne("Models.Vendor.Vendor", "Vendor")
                        .WithMany("Purchases")
                        .HasForeignKey("VendorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");

                    b.Navigation("Customer");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Models.Vendor.Vendor", b =>
                {
                    b.HasOne("Models.Store", null)
                        .WithMany("Employees")
                        .HasForeignKey("StoreID");
                });

            modelBuilder.Entity("Models.Vendor.VendorContacts", b =>
                {
                    b.HasOne("Models.Vendor.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorID");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Models.Store", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("Models.Vendor.Vendor", b =>
                {
                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}