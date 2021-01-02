using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public DateTime ShowDate { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
    }
}
