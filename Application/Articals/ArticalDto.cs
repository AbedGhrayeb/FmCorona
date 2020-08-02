using System;

namespace Application.Articals
{
    public class ArticalDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Details { get; set; }
        public string ImgUrl { get; set; }
        public string CreateAt { get; set; }
    }
}
