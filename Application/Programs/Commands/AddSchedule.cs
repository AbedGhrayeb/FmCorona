using Application.Common.Errors;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
                if (program == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                if (program.Schedules.Any(x=>x.DayOfWeek==request.Vm.DayOfWeek))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "You already add this day, choose another day or edit it!" });
                }
                var schedule = new Schedule
                {
                    DayOfWeek = request.Vm.DayOfWeek,
                    ProgramId = request.Vm.ProgramId,
                    ShowTime = request.Vm.ShowTime,
                    Duration = request.Vm.Duration,
                    Guest = request.Vm.Guest,
                    GuestName = request.Vm.GuestName
                };
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
