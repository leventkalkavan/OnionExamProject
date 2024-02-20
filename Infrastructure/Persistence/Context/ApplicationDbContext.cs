using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class ApplicationDbContext:IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions options):base(options)
    {}
    
    public DbSet<ExamScenarioCategory> ExamScenarioCategories { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionCategory> QuestionCategories { get; set; }
    public DbSet<ExamAnswer> ExamAnswers { get; set; }
    public DbSet<ExamAssignment> ExamAssignments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Exam>()
            .HasOne(e => e.ExamScenarioCategory)
            .WithMany(c => c.Exams)
            .HasForeignKey(e => e.ScenarioCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<QuestionCategory>()
            .HasOne(qc => qc.Exam)
            .WithMany(e => e.QuestionCategories)
            .HasForeignKey(qc => qc.ExamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Question>()
            .HasOne(q => q.QuestionCategory)
            .WithMany(qc => qc.Questions)
            .HasForeignKey(q => q.QuestionCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Choice>()
            .HasOne(c => c.Question)
            .WithMany(q => q.Choices)
            .HasForeignKey(c => c.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}