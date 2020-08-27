namespace Application.Programs
{
    public class WeeklyScheduleDto
    {
        public int Id { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
        public string ProgramName { get; set; }
        public string ImgUrl { get; set; }
        public string Presenter { get; set; }
        public string UAE { get; set; }
        public string KSA { get; set; }
        public bool OnAir { get; set; }
    }
}
