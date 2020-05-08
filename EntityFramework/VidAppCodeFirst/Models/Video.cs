using System;
using System.Collections.Generic;

namespace VidAppCodeFirst.Models
{
    public class Video
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public Classification Classification { get; set; }
        public ICollection<Tag> Tags { get; }
        public Video()
        {
            Tags = new HashSet<Tag>();
        }
    }
}
