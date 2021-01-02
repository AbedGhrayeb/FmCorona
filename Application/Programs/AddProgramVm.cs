using Application.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Programs
{
    public class AddProgramVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [MaxFileSize(2 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
        [Required]
        public int PresinterId { get; set; }

    }
}
