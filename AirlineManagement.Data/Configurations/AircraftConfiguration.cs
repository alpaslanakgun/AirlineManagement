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
    public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
    {
        public void Configure(EntityTypeBuilder<Aircraft> builder)
        {
            builder.HasKey(x => x.AircraftId);
            builder.Property(x => x.AircraftId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Model).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Capacity).IsRequired();
            builder.Property(x => x.AirlineId).IsRequired().HasMaxLength(50);
            builder.ToTable("Aircrafts");
        }
    }
}
