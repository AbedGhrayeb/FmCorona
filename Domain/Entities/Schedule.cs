using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool Guest { get; set; }
        public string GuestName { get; set; }
        [DataType(DataType.Time)]
        public DateTime ShowTime { get; set; }
        public int Duration { get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
    }
}
