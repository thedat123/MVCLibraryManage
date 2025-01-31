﻿// <auto-generated />
using System;
using MVCLibraryManage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCLibraryManage.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20241106034614_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MVCLibraryManage.Models.Entity.Borrower", b =>
                {
                    b.Property<int>("LibraryCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibraryCardId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LibraryCardId");

                    b.ToTable("Borrowers");
                });

            modelBuilder.Entity("MVCLibraryManage.Models.Entity.LibraryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<DateTime?>("PublicationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("LibraryItems");

                    b.HasDiscriminator<string>("ItemType").HasValue("LibraryItem");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MVCLibraryManage.Models.Entity.Book", b =>
                {
                    b.HasBaseType("MVCLibraryManage.Models.Entity.LibraryItem");

                    b.Property<int?>("NumberOfPage")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Book");
                });

            modelBuilder.Entity("MVCLibraryManage.Models.Entity.DVD", b =>
                {
                    b.HasBaseType("MVCLibraryManage.Models.Entity.LibraryItem");

                    b.Property<double>("Runtime")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("DVD");
                });

            modelBuilder.Entity("MVCLibraryManage.Models.Entity.Magazine", b =>
                {
                    b.HasBaseType("MVCLibraryManage.Models.Entity.LibraryItem");

                    b.HasDiscriminator().HasValue("Magazine");
                });
#pragma warning restore 612, 618
        }
    }
}
