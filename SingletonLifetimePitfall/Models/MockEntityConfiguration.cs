using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SingletonLifetimePitfall.Models;

public class MockEntityConfiguration : IEntityTypeConfiguration<MockEntity>
{
    public void Configure(EntityTypeBuilder<MockEntity> builder)
    {
        builder.Property(x => x.Name).IsRequired();
    }
}
