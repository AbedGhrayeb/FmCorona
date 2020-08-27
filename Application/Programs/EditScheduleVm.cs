using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Programs
{
   public class EditScheduleVm
    {
        public int Id { get; set; }
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
        [Required]
        public int ProgramId { get; set; }
    }
}
