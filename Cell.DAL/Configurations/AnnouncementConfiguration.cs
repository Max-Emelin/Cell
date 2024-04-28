using Cell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cell.DAL.Configurations
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(a => a.Title).HasMaxLength(128).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(1000);
            builder.Property(a => a.Price).HasMaxLength(15);
            builder.Property(a => a.Address).HasMaxLength(100);
            builder.Property(a => a.Created).IsRequired();
            builder.HasOne(a => a.User)
                .WithMany(at => at.Announcements)
                .HasForeignKey(at => at.UserId);
            builder.HasMany(a => a.Images)
                .WithOne(at => at.Announcement)
                .HasForeignKey(at => at.AnnouncementId);
        }
    }
}
