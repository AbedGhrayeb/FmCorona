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
    public class AddShowTime
    {
        public class AddShowTimeCommand : IRequest
        {
            public AddShowTimeCommand(AddShowTimeVm vm)
            {
                Vm = vm;
            }

            public AddShowTimeVm Vm { get; }
        }
        public class Handel : IRequestHandler<AddShowTimeCommand>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handel(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(AddShowTimeCommand request, CancellationToken cancellationToken)
            {
                var program = await _context.Programs.FindAsync(request.Vm.ProgramId);
                if (program == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                var showTime = _mapper.Map<ShowTime>(request.Vm);
                _context.ShowTimes.Add(showTime);
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
