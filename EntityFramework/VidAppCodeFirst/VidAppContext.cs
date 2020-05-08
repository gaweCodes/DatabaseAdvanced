using System.Data.Entity;
using VidAppCodeFirst.Configs;
using VidAppCodeFirst.Models;

namespace VidAppCodeFirst
{
    public class VidAppContext : DbContext
    {
        public VidAppContext() : base("name=VidAppCodeFirst") {}
        public DbSet<Video> Videos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VideoConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
