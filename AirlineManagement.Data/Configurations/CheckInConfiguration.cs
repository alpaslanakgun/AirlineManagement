using AirlineManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.UpdatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasOne(c => c.Reservation)
                   .WithMany(r => r.CheckIns)
                   .HasForeignKey(c => c.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("CheckIns");
        }
    }
}
