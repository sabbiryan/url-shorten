﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrlShorten.EntityFrameworkCore;

namespace UrlShorten.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190920101048_Initital_Create")]
    partial class Initital_Create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UrlShorten.EntityFrameworkCore.HitLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("DeletedBy");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DeviceInfo");

                    b.Property<string>("IpAddress");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModificationTime");

                    b.Property<string>("ModifiedBy");

                    b.Property<string>("UrlMapId");

                    b.HasKey("Id");

                    b.HasIndex("UrlMapId");

                    b.ToTable("HitLogs");
                });

            modelBuilder.Entity("UrlShorten.EntityFrameworkCore.UrlMap", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("DeletedBy");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DeviceInfo");

                    b.Property<int>("HitCount");

                    b.Property<int>("Identity")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IpAddress");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModificationTime");

                    b.Property<string>("ModifiedBy");

                    b.Property<string>("RawUrl");

                    b.Property<string>("ShortenUrl");

                    b.HasKey("Id");

                    b.ToTable("UrlMaps");
                });

            modelBuilder.Entity("UrlShorten.EntityFrameworkCore.HitLog", b =>
                {
                    b.HasOne("UrlShorten.EntityFrameworkCore.UrlMap", "UrlMap")
                        .WithMany("Collection")
                        .HasForeignKey("UrlMapId");
                });
#pragma warning restore 612, 618
        }
    }
}