using Application.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Articals
{
    public class AddArticalVm
    {
        [Required]
        public string Title { get; set; }
        [Display( Name ="Short Description")]
        public string ShortDescription { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }
        [MaxFileSize(2 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
    }
}
