using Application.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Articals
{
    public class EditArticalVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        public string Details { get; set; }
        [Display(Name = "Image")]
        public string ImgUrl { get; set; }
        [MaxFileSize(2 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
    }
}
