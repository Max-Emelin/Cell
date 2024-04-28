using Cell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cell.DAL.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(a => a.Text).HasMaxLength(1000);
            builder.Property(a => a.Created).IsRequired();
            builder.HasOne(a => a.UserTo)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.UserToId);
        }
    }
}
