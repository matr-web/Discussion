using Discussion.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discussion.DAL.Configuration;

/// <summary>
/// Configure Rating Entity.
/// </summary>
/// <param name="builder">Provides a simple API for configuring an Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType.</param>
internal class RatingConfiguration : IEntityTypeConfiguration<RatingEntity>
{
    public void Configure(EntityTypeBuilder<RatingEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Value).IsRequired();
    }
}
