using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using CodeFirstDemo.Migrations;

namespace CodeFirstDemo 
{
    public class Course 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public float FullPrice { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public IList<Tag> Tags { get; set; }
    }

    public class Author 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }

    public class Tag 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }

    public enum CourseLevel 
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3
    }

    public class PlutoContext : DbContext 
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public PlutoContext()
            : base("name=DefaultConnection") {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId);
            
            modelBuilder.Entity<Course>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Courses)
                .Map(m => m.ToTable("TagCourses").MapLeftKey("Course_Id"));
        }
    }

    internal static class Program 
    {
        private static void Main() 
        {
            var config = new Configuration();
            config.AutomaticMigrationDataLossAllowed = true;
            config.AutomaticMigrationsEnabled = true;
            var dbMigrator = new DbMigrator(config);
            

            var pendingMigrations = dbMigrator.GetPendingMigrations().ToList();

            dbMigrator.Update();
        }
    }
}