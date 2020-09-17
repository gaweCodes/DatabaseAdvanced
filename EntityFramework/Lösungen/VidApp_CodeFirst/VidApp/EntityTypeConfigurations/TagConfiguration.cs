using System.Data.Entity.ModelConfiguration;

namespace VidApp.EntityTypeConfigurations {
    public class TagConfiguration : EntityTypeConfiguration<Tag> {
        public TagConfiguration() {
            Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
