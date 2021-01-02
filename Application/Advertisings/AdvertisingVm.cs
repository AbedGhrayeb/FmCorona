using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Advertisings
{
    public class AdvertisingVm
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public string Sponsor { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndAt { get; set; }
    }
}
