using System.Collections.Generic;

namespace VidAppCodeFirst.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Video> Videos { get; }
        public Tag()
        {
            Videos = new HashSet<Video>();
        }
    }
}
