using AutoMapper;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Programs
{
    public class OnAirResolver : IValueResolver<Schedule, WeeklyScheduleDto,bool>
    {
        public bool Resolve(Schedule source, WeeklyScheduleDto destination, bool onAir, ResolutionContext context)
        {

            if (source.Program.ShowTimes.Count==0)
            {
                return false;
            }
            if (source.DayOfWeek == DateTime.UtcNow.DayOfWeek)
            {
                if (source.Program.ShowTimes.FirstOrDefault().FirstShowTime.Value >= DateTime.UtcNow &&
                     source.Program.ShowTimes.FirstOrDefault().FirstShowTime.Value.AddMinutes(source.Program.DefaultDuration) <= DateTime.UtcNow)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
