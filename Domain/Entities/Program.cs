using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Program
    {
        public Program()
        {
            Episodes = new List<Episode>();
            ShowTimes = new HashSet<ShowTime>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int DefaultDuration { get; set; }
        //nav
        public virtual Presenter Presenter { get; set; }
        public virtual ICollection<Episode> Episodes { get; set; }
        public virtual ICollection<ShowTime> ShowTimes { get; set; }


    }
}
