using AirlineManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.ToTable("Flights");
        }
    }
}
