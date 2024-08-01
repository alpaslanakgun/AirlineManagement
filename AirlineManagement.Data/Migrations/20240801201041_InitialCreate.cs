using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirlineManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportCode);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                });

            migrationBuilder.CreateTable(
                name: "Aircrafts",
                columns: table => new
                {
                    AircraftId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    AirlineId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircrafts", x => x.AircraftId);
                    table.ForeignKey(
                        name: "FK_Aircrafts_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrewMembers",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AirlineId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewMembers", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_CrewMembers_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartureAirport = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ArrivalAirport = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    AircraftId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightNumber);
                    table.ForeignKey(
                        name: "FK_Flights_Aircrafts_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircrafts",
                        principalColumn: "AircraftId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_ArrivalAirport",
                        column: x => x.ArrivalAirport,
                        principalTable: "Airports",
                        principalColumn: "AirportCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_DepartureAirport",
                        column: x => x.DepartureAirport,
                        principalTable: "Airports",
                        principalColumn: "AirportCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassengerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Seat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Flights_FlightNumber",
                        column: x => x.FlightNumber,
                        principalTable: "Flights",
                        principalColumn: "FlightNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckIns",
                columns: table => new
                {
                    CheckInId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReservationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    BaggageCount = table.Column<int>(type: "int", nullable: false),
                    BoardingTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIns", x => x.CheckInId);
                    table.ForeignKey(
                        name: "FK_CheckIns_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airlines",
                columns: new[] { "AirlineId", "Country", "CreatedDate", "IsActive", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { "AL001", "USA", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), true, false, "Global Air", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) },
                    { "AL002", "UK", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), true, false, "Skyways", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) }
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportCode", "City", "Country", "Name" },
                values: new object[,]
                {
                    { "JFK", "New York", "USA", "John F. Kennedy International Airport" },
                    { "LHR", "London", "UK", "London Heathrow Airport" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "PassengerId", "CreatedDate", "IsActive", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { "P001", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), true, false, "Alice Smith", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) },
                    { "P002", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), true, false, "Bob Johnson", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) }
                });

            migrationBuilder.InsertData(
                table: "Aircrafts",
                columns: new[] { "AircraftId", "AirlineId", "Capacity", "CreatedDate", "IsActive", "IsDeleted", "Model", "UpdatedDate" },
                values: new object[,]
                {
                    { "AC001", "AL001", 160, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Boeing 737", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "AC002", "AL002", 156, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Airbus A320", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "CrewMembers",
                columns: new[] { "MemberId", "AirlineId", "CreatedDate", "IsActive", "IsDeleted", "Name", "Role", "UpdatedDate" },
                values: new object[,]
                {
                    { "CM001", "AL001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "John Doe", "Pilot", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CM002", "AL001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "Jane Doe", "Co-Pilot", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightNumber", "AircraftId", "ArrivalAirport", "ArrivalTime", "CreatedDate", "DepartureAirport", "DepartureTime", "IsActive", "IsDeleted", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { "GA100", "AC001", "LHR", new DateTime(2024, 4, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), "JFK", new DateTime(2024, 4, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Scheduled", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) },
                    { "SW200", "AC002", "JFK", new DateTime(2024, 4, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), "LHR", new DateTime(2024, 4, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Scheduled", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CreatedDate", "FlightNumber", "IsActive", "IsDeleted", "PassengerId", "Seat", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { "R001", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), "GA100", true, false, "P001", "12A", 1, new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) },
                    { "R002", new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), "SW200", true, false, "P002", "6B", 1, new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) }
                });

            migrationBuilder.InsertData(
                table: "CheckIns",
                columns: new[] { "CheckInId", "BaggageCount", "BoardingTime", "CreatedDate", "IsActive", "IsDeleted", "ReservationId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { "CI001", 2, new DateTime(2024, 4, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), true, false, "R001", 1, new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) },
                    { "CI002", 1, new DateTime(2024, 4, 2, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278), true, false, "R002", 1, new DateTime(2024, 8, 1, 23, 10, 41, 136, DateTimeKind.Local).AddTicks(3278) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aircrafts_AirlineId",
                table: "Aircrafts",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_ReservationId",
                table: "CheckIns",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewMembers_AirlineId",
                table: "CrewMembers",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AircraftId",
                table: "Flights",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ArrivalAirport",
                table: "Flights",
                column: "ArrivalAirport");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureAirport",
                table: "Flights",
                column: "DepartureAirport");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightNumber",
                table: "Reservations",
                column: "FlightNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PassengerId",
                table: "Reservations",
                column: "PassengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckIns");

            migrationBuilder.DropTable(
                name: "CrewMembers");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Aircrafts");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Airlines");
        }
    }
}
