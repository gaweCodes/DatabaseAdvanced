using System.Data.Entity.ModelConfiguration;
using VidAppCodeFirst.Models;

namespace VidAppCodeFirst.Configs
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
