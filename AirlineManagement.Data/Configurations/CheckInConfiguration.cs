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
    public class CheckInConfiguration : IEntityTypeConfiguration<CheckIn>
    {
        public void Configure(EntityTypeBuilder<CheckIn> builder)
        {
            builder.HasKey(x => x.CheckInId);
            builder.Property(x => x.CheckInId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ReservationId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BaggageCount).IsRequired();
            builder.Property(x => x.BoardingTime).IsRequired().HasColumnType("datetime");
            builder.ToTable("CheckIns");
        }
    }
}
