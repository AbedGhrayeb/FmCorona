using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Queries
{
    public class ScheduleDetails
    {
        public class ScheduleDetailsQuery : IRequest<EditScheduleVm>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<ScheduleDetailsQuery, EditScheduleVm>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<EditScheduleVm> Handle(ScheduleDetailsQuery request, CancellationToken cancellationToken)
            {
                var schedule = await _context.Schedules.FindAsync(request.Id);
                if (schedule == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }

                var scheduleToReturn = _mapper.Map<EditScheduleVm>(schedule);
                return scheduleToReturn;
            }
        }
    }
}
