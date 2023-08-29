using Discussion.DAL.Configuration;
using Discussion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discussion.DAL;

/// <summary>
/// Discuss DataBase Context Class.
/// </summary>
public class DiscussDbContext : DbContext
{
    public DiscussDbContext(DbContextOptions<DiscussDbContext> options) : base(options)
    {
    }

    public DbSet<AnswerEntity> Answers { get; set; }

    public DbSet<AnswerEntity> Categories { get; set; }

    public DbSet<QuestionEntity> Questions { get; set; }

    public DbSet<RatingEntity> Ratings { get; set; }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations for all created entities.
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new RatingConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
