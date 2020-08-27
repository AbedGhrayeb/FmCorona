using Application.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Presenters
{
    public class AddSocialMediaVm
    {
        [Required]
        public int PresenterId { get; set; }
        [Required]
        public string Provider { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string Link { get; set; }
        [Required]
        [Display(Name = "Icon")]
        [MaxFileSize(1 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile ImgUrl { get; set; }

    }
}
