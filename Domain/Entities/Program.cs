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
            Schedules = new List<Schedule>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int? PresenterId  { get; set; }
        //nav
        public virtual Presenter Presenter { get; set; }
        public virtual ICollection<Episode> Episodes { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }


    }
}
