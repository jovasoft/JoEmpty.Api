﻿// <auto-generated />
using System;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    [DbContext(typeof(PostgresContext))]
    partial class PostgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Entities.Area", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<Guid>("PersonalId");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CurrentCode");

                    b.Property<string>("District");

                    b.Property<string>("Note");

                    b.Property<string>("Province");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Entities.ClientContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClientId");

                    b.Property<string>("Department");

                    b.Property<string>("FirstName");

                    b.Property<string>("InternalNumber");

                    b.Property<string>("LastName");

                    b.Property<string>("MailAddress");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("ClientContacts");
                });

            modelBuilder.Entity("Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("ClientId");

                    b.Property<string>("Code");

                    b.Property<byte>("Currency");

                    b.Property<int>("FacilityCount");

                    b.Property<DateTime?>("FinishDate");

                    b.Property<DateTime?>("StartDate");

                    b.Property<byte>("Supply");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Entities.Facility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<Guid>("AreaId");

                    b.Property<string>("Brand");

                    b.Property<decimal>("BreakdownFee");

                    b.Property<int>("Capacity");

                    b.Property<Guid>("ClientId");

                    b.Property<string>("Code");

                    b.Property<Guid>("ContractId");

                    b.Property<decimal>("CurrentMaintenanceFee");

                    b.Property<string>("District");

                    b.Property<byte>("MaintenanceStatus");

                    b.Property<string>("Name");

                    b.Property<decimal>("OldMaintenanceFee");

                    b.Property<string>("Province");

                    b.Property<int>("Speed");

                    b.Property<int>("Station");

                    b.Property<byte>("Type");

                    b.Property<DateTime?>("WarrantyFinishDate");

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("Entities.Personal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Personals");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mail");

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
