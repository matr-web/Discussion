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

        // Many to One configuration for Ratings/Question.
        builder.HasOne(r => r.Question)
          .WithMany(q => q.Ratings)
          .HasForeignKey(r => r.QuestionId)
          .OnDelete(DeleteBehavior.NoAction); // If you delete Rating, Question will remain.

        // Many to One configuration for Ratings/Answer.
        builder.HasOne(r => r.Answer)
          .WithMany(q => q.Ratings)
          .HasForeignKey(r => r.AnswerId)
          .OnDelete(DeleteBehavior.NoAction); // If you delete Rating, Answer will remain.
    }
}
