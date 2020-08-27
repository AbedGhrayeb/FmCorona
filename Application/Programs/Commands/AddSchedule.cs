using Application.Common.Errors;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class AddSchedule
    {
        public class EditScheduleCommand : IRequest
        {
            public EditScheduleCommand(AddScheduleVm vm)
            {
                Vm = vm;
            }

            public AddScheduleVm Vm { get; }
        }
        public class Handler : IRequestHandler<EditScheduleCommand>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(EditScheduleCommand request, CancellationToken cancellationToken)
            {
                var program = await _context.Programs.FindAsync(request.Vm.ProgramId);
                if (program==null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                var schedule = _mapper.Map<Schedule>(request.Vm);
                schedule.Program = program;
                _context.Schedules.Add(schedule);
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
