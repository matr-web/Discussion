using Discussion.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discussion.DAL.Configuration;

internal class AnswerConfiguration : IEntityTypeConfiguration<AnswerEntity>
{
    /// <summary>
    /// Configure Answer Entity.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring an Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType.</param>
    public void Configure(EntityTypeBuilder<AnswerEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Content).IsRequired();

        builder.Property(a => a.Date).HasDefaultValueSql("getdate()");

        builder.Property(a => a.QuestionId).IsRequired();

        // One to many configuration for Answer/Ratings.
        builder.HasMany(a => a.Ratings)
            .WithOne(r => r.Answer)
            .HasForeignKey(r => r.AnswerId)
            .OnDelete(DeleteBehavior.ClientCascade); // If you delete Answer, Ratings for the Answer also will be deleted.
    }
}
