using Discussion.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discussion.DAL.Configuration;

/// <summary>
/// Configure User Entity.
/// </summary>
/// <param name="builder">Provides a simple API for configuring an Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType.</param>
internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username).IsRequired();

        builder.Property(u => u.Email).IsRequired();

        builder.Property(u => u.Role).IsRequired();

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.Property(u => u.PasswordSalt).IsRequired();

        // One to Many configuration for User/Questions.
        builder.HasMany(u => u.Questions)
           .WithOne(q => q.User)
           .HasForeignKey(q => q.UserId)
           .OnDelete(DeleteBehavior.NoAction); // If you delete Author, his Questions will remain.

        // One to Many configuration for User/Answers.
        builder.HasMany(u => u.Answers)
           .WithOne(a => a.User)
           .HasForeignKey(a => a.UserId)
           .OnDelete(DeleteBehavior.NoAction); // If you delete Author, his Answers will remain.

        // One to Many configuration for User/Ratings.
        builder.HasMany(u => u.Ratings)
           .WithOne(r => r.User)
           .HasForeignKey(r => r.UserId)
           .OnDelete(DeleteBehavior.NoAction); // If you delete Author, his Ratings will remain.
    }
}
