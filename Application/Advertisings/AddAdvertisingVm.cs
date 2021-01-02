using Application.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Advertisings
{
    public class AddAdvertisingVm
    {
        [Required]
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [MaxFileSize(2 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
        public string Sponsor { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartFrom { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndAt { get; set; }
    }
}
