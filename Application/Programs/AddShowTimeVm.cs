using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Programs
{
    public class AddShowTimeVm
    {
        public int ProgramId { get; set; }
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime? FirstShowTime { get; set; }
    }
}
