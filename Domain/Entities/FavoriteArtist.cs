namespace Domain.Entities
{
    public class FavoriteArtist
    {
        public string ArtistId { get; set; }
        public string AppUserId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
