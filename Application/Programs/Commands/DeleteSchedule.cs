using Application.Common.Errors;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class DeleteSchedule
    {
        public class DeleteScheduleCommand : IRequest
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<DeleteScheduleCommand>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
            {
                var schedule = await _context.Schedules.FindAsync(request.Id);
                if (schedule == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                _context.Schedules.Remove(schedule);
                var result = await _context.SaveChangesAsync() > 0;
                if (result)
                {
                    return Unit.Value;
                }
                throw new Exception("Proplem Saving Changes");
            }
        }
    }
}
