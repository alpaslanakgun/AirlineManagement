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
    public class CrewMemberConfiguration : IEntityTypeConfiguration<CrewMember>
    {
        public void Configure(EntityTypeBuilder<CrewMember> builder)
        {
         
            builder.HasKey(x => x.MemberId);
            builder.Property(x => x.MemberId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Role).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AirlineId).IsRequired().HasMaxLength(50);

            builder.HasOne(c => c.Airline)
                  .WithMany(a => a.CrewMembers)
                  .HasForeignKey(c => c.AirlineId);

            builder.ToTable("CrewMembers");
        }
    }
}
