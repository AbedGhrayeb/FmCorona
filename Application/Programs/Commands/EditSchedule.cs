using Application.Common.Errors;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class EditSchedule
    {
        public class EditScheduleCommand : IRequest
        {
            public EditScheduleCommand(EditScheduleVm vm)
            {
                Vm = vm;
            }

            public EditScheduleVm Vm { get; }
        }
        public class Handel : IRequestHandler<EditScheduleCommand>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditScheduleCommand request, CancellationToken cancellationToken)
            {
                var schedule = await _context.Schedules.FindAsync(request.Vm.Id);
                if (schedule == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                var program = await _context.Programs.FindAsync(request.Vm.ProgramId);
                if (program == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                schedule.DayOfWeek = request.Vm.DayOfWeek;
                schedule.ShowTime = request.Vm.ShowTime;
                schedule.Duration = request.Vm.Duration;
                schedule.Guest = request.Vm.Guest;
                schedule.GuestName = request.Vm.GuestName;
                schedule.Program = program;

                _context.Schedules.Update(schedule);
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
