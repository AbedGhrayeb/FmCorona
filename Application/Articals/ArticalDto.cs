using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Articals
{
    public class ArticalDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        public string Details { get; set; }
        [Display(Name = "Image")]
        public string ImgUrl { get; set; }
        [Display(Name = "Create Date")]
        public string CreateAt { get; set; }
    }
}
