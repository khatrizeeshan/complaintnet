using ComplaintNet.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComplaintNet.WebApi.Persistance
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Email).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Password).HasMaxLength(100).IsRequired();
        }
    }
}
