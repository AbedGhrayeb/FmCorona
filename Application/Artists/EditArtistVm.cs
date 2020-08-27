using Application.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Artists
{
    public class EditArtistVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string ImgUrl { get; set; }
        [MaxFileSize(2 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
    }
}
