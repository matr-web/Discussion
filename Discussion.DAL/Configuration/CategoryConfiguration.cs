using Discussion.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discussion.DAL.Configuration;

/// <summary>
/// Configure Category Entity.
/// </summary>
/// <param name="builder">Provides a simple API for configuring an Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType.</param>
internal class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired();

        // One to many configuration for Category/Questions.
        builder.HasMany(c => c.Questions)
          .WithOne(q => q.Category)
          .HasForeignKey(q => q.CategoryId)
          .OnDelete(DeleteBehavior.ClientCascade); // If you delete Category, Questions for the Category also will be deleted.
    }
}
