using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Artical
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Details { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public string ImgUrl { get; set; }
    }
}
