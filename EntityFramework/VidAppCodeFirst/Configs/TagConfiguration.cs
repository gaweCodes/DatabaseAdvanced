using System.Data.Entity.ModelConfiguration;
using VidAppCodeFirst.Models;

namespace VidAppCodeFirst.Configs
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
