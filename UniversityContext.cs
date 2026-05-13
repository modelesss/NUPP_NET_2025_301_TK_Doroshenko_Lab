using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Models;

namespace University.Infrastructure.Data
{
    public class UniversityContext : DbContext
    {
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }

        // Конструктор для runtime (з DI)
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        // Конструктор для design-time (Add-Migration, Update-Database)
        public UniversityContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=university.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonEntity>()
                        .UseTptMappingStrategy();

            modelBuilder.Entity<StudentEntity>().ToTable("Students");
            modelBuilder.Entity<TeacherEntity>().ToTable("Teachers");

            modelBuilder.Entity<CourseEntity>()
                        .HasOne(c => c.Teacher)
                        .WithMany()
                        .HasForeignKey(c => c.TeacherId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TeacherEntity>()
                        .Property(t => t.Salary)
                        .HasColumnType("decimal(18,2)");
        }
    }
}
