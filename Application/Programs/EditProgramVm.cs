using Application.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Programs
{
    public class EditProgramVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Duration")]
        public int DefaultDuration { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        [MaxFileSize(2 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IFormFile Image { get; set; }
  
        public int PresenterId { get; set; }
    }
}
