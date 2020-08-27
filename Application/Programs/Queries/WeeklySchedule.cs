using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Queries
{
    public class WeeklySchedule
    {
        public class ScheduleEnvelope
        {
            public List<WeeklyScheduleDto> ScheduleDtos { get; set; }
        }
        public class ScheduleQuery : IRequest<ScheduleEnvelope>
        {
            public ScheduleQuery(int? dayOfWeek)
            {
                DayOfWeek = dayOfWeek;
            }

            public int? DayOfWeek { get; }
        }
        public class Handler : IRequestHandler<ScheduleQuery, ScheduleEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ScheduleEnvelope> Handle(ScheduleQuery request, CancellationToken cancellationToken)
            {
                var queryable = _context.Schedules.AsQueryable();
                if (request.DayOfWeek.HasValue)
                {
                    queryable = queryable.Where(x => (int)x.DayOfWeek == request.DayOfWeek.Value);
                }
                var schedules = await queryable.OrderBy(x => x.Program.ShowTimes.FirstOrDefault().FirstShowTime.Value).ToListAsync();
                var schedulesToReuren = new ScheduleEnvelope
                {
                    ScheduleDtos = _mapper.Map<List<WeeklyScheduleDto>>(schedules)
                };
                return schedulesToReuren;
            }
        }
    }
}
