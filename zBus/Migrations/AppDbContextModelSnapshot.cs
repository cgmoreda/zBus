﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zBus.Data;

#nullable disable

namespace zBus.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeatId"), 1L, 1);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("SeatId");

                    b.HasIndex("TripId");

                    b.ToTable("Seat");
                });

            modelBuilder.Entity("Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TripId"), 1L, 1);

                    b.Property<int>("ArrivalCityID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("BusId")
                        .HasColumnType("int");

                    b.Property<int>("DepartureCityID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("TripPrice")
                        .HasColumnType("float");

                    b.Property<double>("TripTime")
                        .HasColumnType("float");

                    b.HasKey("TripId");

                    b.HasIndex("ArrivalCityID");

                    b.HasIndex("BusId");

                    b.HasIndex("DepartureCityID");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("zBus.Models.Bus", b =>
                {
                    b.Property<int>("BusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusId"), 1L, 1);

                    b.Property<bool>("AirConditioningAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("BusModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusPicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<bool>("RestroomAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("WifiAvailable")
                        .HasColumnType("bit");

                    b.HasKey("BusId");

                    b.HasIndex("DriverId");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("zBus.Models.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DriverId"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProfilePicturePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("int");

                    b.HasKey("DriverId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("zBus.Models.Station", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StationId"), 1L, 1);

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StationId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("Seat", b =>
                {
                    b.HasOne("Trip", "Trip")
                        .WithMany("Seats")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("Trip", b =>
                {
                    b.HasOne("zBus.Models.Station", "ArrivalStation")
                        .WithMany()
                        .HasForeignKey("ArrivalCityID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("zBus.Models.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("zBus.Models.Station", "DepartureStation")
                        .WithMany()
                        .HasForeignKey("DepartureCityID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ArrivalStation");

                    b.Navigation("Bus");

                    b.Navigation("DepartureStation");
                });

            modelBuilder.Entity("zBus.Models.Bus", b =>
                {
                    b.HasOne("zBus.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("Trip", b =>
                {
                    b.Navigation("Seats");
                });
#pragma warning restore 612, 618
        }
    }
}
