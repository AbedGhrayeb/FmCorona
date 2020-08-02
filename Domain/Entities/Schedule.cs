using System;

namespace Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
        public virtual Program Program { get; set; }
    }
}
