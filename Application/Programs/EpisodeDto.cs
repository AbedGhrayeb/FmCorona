using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Programs
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public string ImgUrl { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
        public int? Number { get; set; }
        public string Title { get; set; }
        public string ShowDate { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }
    
    }
}
