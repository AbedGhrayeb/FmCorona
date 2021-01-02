using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Programs
{
    public class AddScheduleVm
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name ="Set Time in UAE")]
        public DateTime ShowTime { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        [Display(Name = "Duaration in Minutes")]
        public int Duration { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
        [Required]
        public int  ProgramId { get; set; }
    }
}
