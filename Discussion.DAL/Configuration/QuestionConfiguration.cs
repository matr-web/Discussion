using Discussion.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discussion.DAL.Configuration;

/// <summary>
/// Configure Question Entity.
/// </summary>
/// <param name="builder">Provides a simple API for configuring an Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType.</param>
internal class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
{
    public void Configure(EntityTypeBuilder<QuestionEntity> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(q => q.CategoryId).IsRequired();

        builder.Property(q => q.Topic).IsRequired();

        builder.Property(q => q.Content).IsRequired();

        builder.Property(q => q.Date).HasDefaultValueSql("getdate()");

        // One to many configuration for Question/Answers.
        builder.HasMany(q => q.Answers)
          .WithOne(a => a.Question)
          .HasForeignKey(a => a.QuestionId)
          .OnDelete(DeleteBehavior.Cascade); // If you delete Question, Answers for the Question also will be deleted.

        // One to many configuration for Question/Ratings.
        builder.HasMany(q => q.Ratings)
           .WithOne(r => r.Question)
           .HasForeignKey(r => r.QuestionId)
           .OnDelete(DeleteBehavior.Cascade); // If you delete Question, Ratings for the Question also will be deleted.
    }
}
