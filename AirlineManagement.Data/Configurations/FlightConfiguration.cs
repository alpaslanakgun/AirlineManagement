using AirlineManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineManagement.Data.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {

          
  
            builder.HasKey(x => x.FlightNumber);
            builder.Property(x => x.FlightNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.DepartureAirport).IsRequired().HasMaxLength(10);
            builder.Property(x => x.ArrivalAirport).IsRequired().HasMaxLength(10);
            builder.Property(x => x.DepartureTime).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.ArrivalTime).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.AircraftId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);

            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasOne(f => f.DepartureAirportNavigation)
                   .WithMany(a => a.DepartureFlights)
                   .HasForeignKey(f => f.DepartureAirport)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.ArrivalAirportNavigation)
                   .WithMany(a => a.ArrivalFlights)
                   .HasForeignKey(f => f.ArrivalAirport)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Aircraft)
                   .WithMany(a => a.Flights)
                   .HasForeignKey(f => f.AircraftId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Flights");
        }
    }
}
