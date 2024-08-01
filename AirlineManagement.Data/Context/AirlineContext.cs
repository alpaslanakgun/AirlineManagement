using AirlineManagement.Domain.Common;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AirlineManagement.Data.Context
{
    public class AirlineContext : DbContext
    {
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }

        public AirlineContext(DbContextOptions<AirlineContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            var currentDate = DateTime.Now;

            modelBuilder.Entity<Airline>().HasData(
                new Airline { AirlineId = "AL001", Name = "Global Air", Country = "USA", CreatedDate = currentDate, UpdatedDate = currentDate, IsDeleted = false, IsActive = true },
                new Airline { AirlineId = "AL002", Name = "Skyways", Country = "UK", CreatedDate = currentDate, UpdatedDate = currentDate, IsDeleted = false, IsActive = true }
            );

            modelBuilder.Entity<Airport>().HasData(
                new Airport { AirportCode = "JFK", Name = "John F. Kennedy International Airport", City = "New York", Country = "USA" },
                new Airport { AirportCode = "LHR", Name = "London Heathrow Airport", City = "London", Country = "UK" }
            );

            modelBuilder.Entity<Aircraft>().HasData(
                new Aircraft { AircraftId = "AC001", Model = "Boeing 737", Capacity = 160, AirlineId = "AL001" },
                new Aircraft { AircraftId = "AC002", Model = "Airbus A320", Capacity = 156, AirlineId = "AL002" }
            );

            modelBuilder.Entity<CrewMember>().HasData(
                new CrewMember { MemberId = "CM001", Name = "John Doe", Role = "Pilot", AirlineId = "AL001" },
                new CrewMember { MemberId = "CM002", Name = "Jane Doe", Role = "Co-Pilot", AirlineId = "AL001" }
            );

            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    FlightNumber = "GA100",
                    DepartureAirport = "JFK",
                    ArrivalAirport = "LHR",
                    DepartureTime = new DateTime(2024, 4, 1, 14, 0, 0),
                    ArrivalTime = new DateTime(2024, 4, 1, 20, 0, 0),
                    AircraftId = "AC001",
                    Status = "Scheduled",
                    CreatedDate = currentDate,
                    UpdatedDate = currentDate,
                    IsDeleted = false,
                    IsActive = true
                },
                new Flight
                {
                    FlightNumber = "SW200",
                    DepartureAirport = "LHR",
                    ArrivalAirport = "JFK",
                    DepartureTime = new DateTime(2024, 4, 2, 9, 0, 0),
                    ArrivalTime = new DateTime(2024, 4, 2, 15, 0, 0),
                    AircraftId = "AC002",
                    Status = "Scheduled",
                    CreatedDate = currentDate,
                    UpdatedDate = currentDate,
                    IsDeleted = false,
                    IsActive = true
                }
            );

            modelBuilder.Entity<Passenger>().HasData(
                new Passenger { PassengerId = "P001", Name = "Alice Smith", CreatedDate = currentDate, UpdatedDate = currentDate, IsDeleted = false, IsActive = true },
                new Passenger { PassengerId = "P002", Name = "Bob Johnson", CreatedDate = currentDate, UpdatedDate = currentDate, IsDeleted = false, IsActive = true }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    ReservationId = "R001",
                    PassengerId = "P001",
                    FlightNumber = "GA100",
                    Status = ReservationStatus.Confirmed,
                    Seat = "12A",
                    CreatedDate = currentDate,
                    UpdatedDate = currentDate,
                    IsDeleted = false,
                    IsActive = true
                },
                new Reservation
                {
                    ReservationId = "R002",
                    PassengerId = "P002",
                    FlightNumber = "SW200",
                    Status = ReservationStatus.Confirmed, 
                    Seat = "6B",
                    CreatedDate = currentDate,
                    UpdatedDate = currentDate,
                    IsDeleted = false,
                    IsActive = true
                }
            );

            modelBuilder.Entity<CheckIn>().HasData(
                new CheckIn
                {
                    CheckInId = "CI001",
                    ReservationId = "R001",
                    Status = CheckInStatus.Completed, 
                    BaggageCount = 2,
                    BoardingTime = new DateTime(2024, 4, 1, 13, 0, 0),
                    CreatedDate = currentDate,
                    UpdatedDate = currentDate,
                    IsDeleted = false,
                    IsActive = true
                },
                new CheckIn
                {
                    CheckInId = "CI002",
                    ReservationId = "R002",
                    Status = CheckInStatus.Completed,
                    BaggageCount = 1,
                    BoardingTime = new DateTime(2024, 4, 2, 8, 0, 0),
                    CreatedDate = currentDate,
                    UpdatedDate = currentDate,
                    IsDeleted = false,
                    IsActive = true
                }
            );
        }

        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries()
                         .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is BaseEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = now;
                        entity.UpdatedDate = now;
                    }

                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified)
                .ToList();

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    entity.UpdatedDate = now;
                }
            }

            var addedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();

            foreach (var entry in addedEntries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    entity.CreatedDate = now;
                    entity.UpdatedDate = now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
