using Cell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cell.DAL.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(a => a.AnnouncementId).IsRequired();
            builder.Property(a => a.Path).HasMaxLength(1000);
            builder.HasOne(a => a.Announcement)
                .WithMany(at => at.Images)
                .HasForeignKey(at => at.AnnouncementId);
        }
    }
}