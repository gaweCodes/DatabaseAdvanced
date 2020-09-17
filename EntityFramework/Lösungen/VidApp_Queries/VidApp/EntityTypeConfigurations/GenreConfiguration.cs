using System.Data.Entity.ModelConfiguration;

namespace VidApp.EntityTypeConfigurations {
    public class GenreConfiguration : EntityTypeConfiguration<Genre> {
        public GenreConfiguration() {
            Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
