using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Queries
{
    public class ProgramSchedule
    {

        public class ProgramScheduleQuery : IRequest<List<ProgramScheduleDto>>
        {
            public int ProgramId { get; set; }
        }
        public class Handler : IRequestHandler<ProgramScheduleQuery, List<ProgramScheduleDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<ProgramScheduleDto>> Handle(ProgramScheduleQuery request, CancellationToken cancellationToken)
            {
                var queryable = _context.Schedules.Where(x => x.ProgramId == request.ProgramId).AsQueryable();

                IReadOnlyCollection<Schedule> schedules = await queryable.OrderBy(x => x.DayOfWeek).ToListAsync(); ;
                var schedulesToReuren = _mapper.Map<List<ProgramScheduleDto>>(schedules);
                return schedulesToReuren;

            }
        }
    }
}
