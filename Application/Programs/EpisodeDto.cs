using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Programs
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        [Display(Name ="Program Name")]
        public string ProgramName { get; set; }
        [Display(Name ="Image")]
        public string ImgUrl { get; set; }
        [Display(Name = "Is there a guest")]
        public bool Guest { get; set; }
        [Display(Name ="Guest Name")]
        public string GuestName { get; set; }
        public int? Number { get; set; }
        public string Title { get; set; }
        [Display(Name ="Show Date")]
        public string ShowDate { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }
    
    }
}
