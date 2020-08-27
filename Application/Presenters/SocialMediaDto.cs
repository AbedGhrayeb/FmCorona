using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Presenters
{
    public class SocialMediaDto
    {
        public string Provider { get; set; }
        public string Link { get; set; }
        [Display(Name = "Icon")]
        public string ImgUrl { get; set; }
    }
}