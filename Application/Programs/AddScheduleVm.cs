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
        public bool Guest { get; set; }
        public string GuestName { get; set; }
        [Required]
        public int  ProgramId { get; set; }
    }
}
