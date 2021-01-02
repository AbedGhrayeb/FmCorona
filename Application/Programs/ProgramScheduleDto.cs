using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace Application.Programs
{
    public class ProgramScheduleDto
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
        [Display(Name = "Day Of Week")]
        public string DayOfWeek { get; set; }
        [Display(Name = "Presenter")]
        public string Presenter { get; set; }
        [Display(Name = "Guest Name")]
        public string GuestName { get; set; }
        [Display(Name = "UAE Time")]
        public string UAE { get; set; }
        [Display(Name = "KSA Time")]
        public string KSA { get; set; }
        public int Duration { get; set; }
    }
}
