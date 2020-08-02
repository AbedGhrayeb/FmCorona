namespace Application.Programs
{
    public class ProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public int? DefaultDuration { get; set; }
        public string Presenter { get; set; }
        public string DayOfWeek { get; set; }
        public string ShowTimeFrom { get; set; }
        public string ShowTimeTo { get; set; }
    }
}