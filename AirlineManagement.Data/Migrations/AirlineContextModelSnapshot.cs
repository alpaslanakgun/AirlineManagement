﻿// <auto-generated />
using System;
using AirlineManagement.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AirlineManagement.Data.Migrations
{
    [DbContext(typeof(AirlineContext))]
    partial class AirlineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Aircraft", b =>
                {
                    b.Property<string>("AircraftId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AirlineId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AircraftId");

                    b.HasIndex("AirlineId");

                    b.ToTable("Aircrafts", (string)null);

                    b.HasData(
                        new
                        {
                            AircraftId = "AC001",
                            AirlineId = "AL001",
                            Capacity = 160,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            IsDeleted = false,
                            Model = "Boeing 737",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AircraftId = "AC002",
                            AirlineId = "AL002",
                            Capacity = 156,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            IsDeleted = false,
                            Model = "Airbus A320",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Airline", b =>
                {
                    b.Property<string>("AirlineId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("AirlineId");

                    b.ToTable("Airlines", (string)null);

                    b.HasData(
                        new
                        {
                            AirlineId = "AL001",
                            Country = "USA",
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Global Air",
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        },
                        new
                        {
                            AirlineId = "AL002",
                            Country = "UK",
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Skyways",
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        });
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Airport", b =>
                {
                    b.Property<string>("AirportCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AirportCode");

                    b.ToTable("Airports", (string)null);

                    b.HasData(
                        new
                        {
                            AirportCode = "JFK",
                            City = "New York",
                            Country = "USA",
                            Name = "John F. Kennedy International Airport"
                        },
                        new
                        {
                            AirportCode = "LHR",
                            City = "London",
                            Country = "UK",
                            Name = "London Heathrow Airport"
                        });
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.CheckIn", b =>
                {
                    b.Property<string>("CheckInId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("BaggageCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BoardingTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ReservationId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("CheckInId");

                    b.HasIndex("ReservationId");

                    b.ToTable("CheckIns", (string)null);

                    b.HasData(
                        new
                        {
                            CheckInId = "CI001",
                            BaggageCount = 2,
                            BoardingTime = new DateTime(2024, 4, 1, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            IsActive = true,
                            IsDeleted = false,
                            ReservationId = "R001",
                            Status = 1,
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        },
                        new
                        {
                            CheckInId = "CI002",
                            BaggageCount = 1,
                            BoardingTime = new DateTime(2024, 4, 2, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            IsActive = true,
                            IsDeleted = false,
                            ReservationId = "R002",
                            Status = 1,
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        });
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.CrewMember", b =>
                {
                    b.Property<string>("MemberId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AirlineId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MemberId");

                    b.HasIndex("AirlineId");

                    b.ToTable("CrewMembers", (string)null);

                    b.HasData(
                        new
                        {
                            MemberId = "CM001",
                            AirlineId = "AL001",
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            IsDeleted = false,
                            Name = "John Doe",
                            Role = "Pilot",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            MemberId = "CM002",
                            AirlineId = "AL001",
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            IsDeleted = false,
                            Name = "Jane Doe",
                            Role = "Co-Pilot",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Flight", b =>
                {
                    b.Property<string>("FlightNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AircraftId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ArrivalAirport")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("DepartureAirport")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("FlightNumber");

                    b.HasIndex("AircraftId");

                    b.HasIndex("ArrivalAirport");

                    b.HasIndex("DepartureAirport");

                    b.ToTable("Flights", (string)null);

                    b.HasData(
                        new
                        {
                            FlightNumber = "GA100",
                            AircraftId = "AC001",
                            ArrivalAirport = "LHR",
                            ArrivalTime = new DateTime(2024, 4, 1, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            DepartureAirport = "JFK",
                            DepartureTime = new DateTime(2024, 4, 1, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            Status = "Scheduled",
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        },
                        new
                        {
                            FlightNumber = "SW200",
                            AircraftId = "AC002",
                            ArrivalAirport = "JFK",
                            ArrivalTime = new DateTime(2024, 4, 2, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            DepartureAirport = "LHR",
                            DepartureTime = new DateTime(2024, 4, 2, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            Status = "Scheduled",
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        });
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Passenger", b =>
                {
                    b.Property<string>("PassengerId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PassengerId");

                    b.ToTable("Passengers", (string)null);

                    b.HasData(
                        new
                        {
                            PassengerId = "P001",
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Alice Smith",
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        },
                        new
                        {
                            PassengerId = "P002",
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Bob Johnson",
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        });
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Reservation", b =>
                {
                    b.Property<string>("ReservationId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PassengerId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Seat")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("ReservationId");

                    b.HasIndex("FlightNumber");

                    b.HasIndex("PassengerId");

                    b.ToTable("Reservations", (string)null);

                    b.HasData(
                        new
                        {
                            ReservationId = "R001",
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            FlightNumber = "GA100",
                            IsActive = true,
                            IsDeleted = false,
                            PassengerId = "P001",
                            Seat = "12A",
                            Status = 1,
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        },
                        new
                        {
                            ReservationId = "R002",
                            CreatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278),
                            FlightNumber = "SW200",
                            IsActive = true,
                            IsDeleted = false,
                            PassengerId = "P002",
                            Seat = "6B",
                            Status = 1,
                            UpdatedDate = new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278)
                        });
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Aircraft", b =>
                {
                    b.HasOne("AirlineManagement.Domain.Entities.Airline", "Airline")
                        .WithMany("Aircrafts")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.CheckIn", b =>
                {
                    b.HasOne("AirlineManagement.Domain.Entities.Reservation", "Reservation")
                        .WithMany("CheckIns")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.CrewMember", b =>
                {
                    b.HasOne("AirlineManagement.Domain.Entities.Airline", "Airline")
                        .WithMany("CrewMembers")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Flight", b =>
                {
                    b.HasOne("AirlineManagement.Domain.Entities.Aircraft", "Aircraft")
                        .WithMany("Flights")
                        .HasForeignKey("AircraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirlineManagement.Domain.Entities.Airport", "ArrivalAirportNavigation")
                        .WithMany("ArrivalFlights")
                        .HasForeignKey("ArrivalAirport")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AirlineManagement.Domain.Entities.Airport", "DepartureAirportNavigation")
                        .WithMany("DepartureFlights")
                        .HasForeignKey("DepartureAirport")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Aircraft");

                    b.Navigation("ArrivalAirportNavigation");

                    b.Navigation("DepartureAirportNavigation");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("AirlineManagement.Domain.Entities.Flight", "Flight")
                        .WithMany("Reservations")
                        .HasForeignKey("FlightNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirlineManagement.Domain.Entities.Passenger", "Passenger")
                        .WithMany("Reservations")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Aircraft", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Airline", b =>
                {
                    b.Navigation("Aircrafts");

                    b.Navigation("CrewMembers");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Airport", b =>
                {
                    b.Navigation("ArrivalFlights");

                    b.Navigation("DepartureFlights");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Flight", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Passenger", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("AirlineManagement.Domain.Entities.Reservation", b =>
                {
                    b.Navigation("CheckIns");
                });
#pragma warning restore 612, 618
        }
    }
}
