using System;
using System.Collections.Generic;
using System.Linq;

namespace VidApp
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public Classification Classification { get; set; }
        public virtual ICollection<Tag> Tags { get; private set; }

        public Video()
        {
            Tags = new HashSet<Tag>();
        }

        public void AddTag(Tag tag)
        {
            Tags.Add(tag);  
        }

        public void RemoveTag(string tagName)
        {

            var tag = Tags.SingleOrDefault(t => t.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase));

            if (tag != null)
                Tags.Remove(tag);
        }
    }
}