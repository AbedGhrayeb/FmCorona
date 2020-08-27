using System.ComponentModel.DataAnnotations;

namespace Application.Artists
{
    public class ArtistDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Display(Name ="Image")]
        public string ImgUrl { get; set; }
        [Display(Name ="Fan Count")]
        public int FansCount { get; set; }
    }
}
