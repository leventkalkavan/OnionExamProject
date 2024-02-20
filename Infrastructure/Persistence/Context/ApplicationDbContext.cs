using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ExamScenarioCategory> ExamScenarioCategories { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ExamAssignment> ExamAssignments { get; set; }
        public DbSet<ExamAnswer> ExamAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.QuestionCategory)
                .WithMany(qc => qc.Questions)
                .HasForeignKey(q => q.QuestionCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamAnswer>()
                .HasOne(ea => ea.Question)
                .WithMany(q => q.ExamAnswers)
                .HasForeignKey(ea => ea.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}