using System;

namespace Domain.Entities
{
    public class Advertising
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public string Sponsor { get; set; }
        public DateTime StartFrom { get; set; }
        public DateTime EndAt { get; set; }
    }
}
