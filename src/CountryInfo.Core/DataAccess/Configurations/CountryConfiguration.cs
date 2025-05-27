using CountryInfo.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountryInfo.Core.DataAccess.Configurations;

internal class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .UseCollation("NOCASE")
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(c => c.Name).IsUnique();

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(2)
            .IsFixedLength();

        builder.Property(c => c.Region)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Subregion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Capital)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Population)
            .IsRequired();

        builder.Property(c => c.Currency)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(c => c.Statistic);
    }
}