using AutoMapper;
using Domain.Entities;
using System;

namespace Application.Programs
{
    public class OnAirResolver : IValueResolver<Schedule, WeeklyScheduleDto, bool>
    {
        public bool Resolve(Schedule source, WeeklyScheduleDto destination, bool onAir, ResolutionContext context)
        {

            if (source.DayOfWeek == DateTime.UtcNow.DayOfWeek)
            {
                if (source.ShowTime >= DateTime.UtcNow &&
                     source.ShowTime.AddMinutes(source.Duration) <= DateTime.UtcNow)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
