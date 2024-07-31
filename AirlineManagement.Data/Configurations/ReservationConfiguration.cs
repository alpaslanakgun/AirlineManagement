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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.ReservationId);
            builder.Property(x => x.ReservationId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PassengerId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FlightNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Seat).IsRequired().HasMaxLength(10);
            builder.ToTable("Reservations");
        }
    }
}
