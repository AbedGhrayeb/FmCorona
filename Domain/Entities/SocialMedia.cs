namespace Domain.Entities
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public string Link { get; set; }
        public string ImgUrl { get; set; }
        public int PresenterId { get; set; }
        public int ProgramId { get; set; }
    }
}
