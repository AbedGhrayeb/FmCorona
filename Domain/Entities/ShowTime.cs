using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [DataType(DataType.Time)]
        public DateTime? FirstShowTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? RepatetShowTime { get; set; }


    }

}
