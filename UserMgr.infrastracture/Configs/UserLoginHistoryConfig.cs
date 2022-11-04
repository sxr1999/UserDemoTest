using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMgr.Domain;

namespace UserMgr.infrastracture.Configs;

public class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
{
    public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
    {
        builder.ToTable("T_UserAccessFailConfigs");
        builder.OwnsOne(x => x.PhoneNumber, y =>
        {
            y.Property(x => x.RegionNumber).HasMaxLength(5).IsUnicode(false);
            y.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
        });
    }
}