using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMgr.Domain;

namespace UserMgr.infrastracture.Configs;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("T_Users");
        builder.OwnsOne(x => x.phoneNumber, y =>
        {
            y.Property(x => x.RegionNumber).HasMaxLength(5).IsUnicode(false);
            y.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
        });
        builder.HasOne(x => x._userAccessFail)
            .WithOne(y => y.User)
            .HasForeignKey<UserAccessFail>(z => z.UserId);
        builder.Property("passwordHash").HasMaxLength(100).IsUnicode(false);
    }
}