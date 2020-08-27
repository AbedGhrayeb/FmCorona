using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Artists
{
    public class AddArtistVm
    {
        public string Name { get; set; }
        [MaxFileSize(2 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
    }
}
