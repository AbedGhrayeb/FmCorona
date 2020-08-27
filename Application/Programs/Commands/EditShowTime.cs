using Application.Common.Errors;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Commands
{
    public class EditShowTime
    {
        public class EditShowTimeCommand : IRequest
        {
            public EditShowTimeCommand(ShowTime vm)
            {
                Vm = vm;
            }

            public ShowTime Vm { get; }
        }
        public class Handel : IRequestHandler<EditShowTimeCommand>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditShowTimeCommand request, CancellationToken cancellationToken)
            {
                var showTime = await _context.ShowTimes.FindAsync(request.Vm.Id);
                if (showTime == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                showTime.DayOfWeek = request.Vm.DayOfWeek;
                showTime.FirstShowTime = request.Vm.FirstShowTime.Value;
                _context.ShowTimes.Update(showTime);
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
