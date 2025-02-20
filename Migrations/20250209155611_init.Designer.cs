﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoshopAPIV3.Data;

#nullable disable

namespace VideoshopAPIV3.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250209155611_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VideoshopAPIV3.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ReleaseYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RentalHeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RentalHeaderId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.RentalDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("RentalHeaderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("RentalHeaderId");

                    b.ToTable("RentalDetails");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.RentalHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("RentalHeaders");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.Movie", b =>
                {
                    b.HasOne("VideoshopAPIV3.Model.RentalHeader", null)
                        .WithMany("Movies")
                        .HasForeignKey("RentalHeaderId");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.RentalDetail", b =>
                {
                    b.HasOne("VideoshopAPIV3.Model.Movie", "Movie")
                        .WithMany("RentalDetails")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VideoshopAPIV3.Model.RentalHeader", "RentalHeader")
                        .WithMany("RentalDetails")
                        .HasForeignKey("RentalHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("RentalHeader");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.RentalHeader", b =>
                {
                    b.HasOne("VideoshopAPIV3.Model.Customer", "Customers")
                        .WithMany("RentalHeaders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.Customer", b =>
                {
                    b.Navigation("RentalHeaders");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.Movie", b =>
                {
                    b.Navigation("RentalDetails");
                });

            modelBuilder.Entity("VideoshopAPIV3.Model.RentalHeader", b =>
                {
                    b.Navigation("Movies");

                    b.Navigation("RentalDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
