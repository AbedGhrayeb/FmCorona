using System.Collections.Generic;

namespace Domain.Entities
{
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public virtual ICollection<FavoriteArtist> FavoriteArtists { get; set; }
    }
}
