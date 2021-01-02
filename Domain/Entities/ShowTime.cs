using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [DataType(DataType.Time)]
        public DateTime FirstShowTime { get; set; }
        public int EpisodeId { get; set; }
        public virtual Episode Episode { get; set; }
    }

}
