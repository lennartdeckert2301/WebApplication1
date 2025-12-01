using LernmoduleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LernmoduleApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<QuizRun> QuizRuns => Set<QuizRun>();
        public DbSet<QuizAnswer> QuizAnswers => Set<QuizAnswer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizRun>()
                .HasMany(r => r.Answers)
                .WithOne(a => a.Run)
                .HasForeignKey(a => a.RunId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
