using System.ComponentModel.DataAnnotations;

namespace Application.Programs
{
    public class ProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name ="Image")]
        public string ImgUrl { get; set; }
        [Display(Name = "Duration")]
        public int? DefaultDuration { get; set; }
        public string Description { get; set; }
        public string Presenter { get; set; }
        [Display(Name ="Day")]
        public string DayOfWeek { get; set; }
        [Display(Name = "Showtime From")]
        public string ShowTimeFrom { get; set; }
        [Display(Name = "Showtime To")]
        public string ShowTimeTo { get; set; }
    }
}