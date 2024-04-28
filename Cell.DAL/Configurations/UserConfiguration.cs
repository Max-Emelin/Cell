using Cell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Cell.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(a => a.Name).HasMaxLength(100);
            builder.Property(a => a.Lastname).HasMaxLength(100);
            builder.Property(a => a.Email).HasMaxLength(100);
            builder.Property(a => a.PhoneNumber).HasMaxLength(11).IsRequired();
            builder.Property(a => a.Address).HasMaxLength(100);
            builder.Property(a => a.Login).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(100).IsRequired();
            builder.HasMany(a => a.Announcements)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
            builder.HasMany(a => a.Comments)
               .WithOne(e => e.UserTo)
               .HasForeignKey(e => e.UserToId);
        }
    }
}