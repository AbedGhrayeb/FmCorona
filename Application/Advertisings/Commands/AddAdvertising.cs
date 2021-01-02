using Application.Common.Errors;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Advertisings.Commands
{
    public class AddAdvertising
    {
        public class AddAdvertisingCommand : IRequest
        {
            public AddAdvertisingCommand(AddAdvertisingVm vm)
            {
                Vm = vm;
            }

            public AddAdvertisingVm Vm { get; }
        }
        public class Handler : IRequestHandler<AddAdvertisingCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handler(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(AddAdvertisingCommand request, CancellationToken cancellationToken)
            {
                if (request.Vm.EndAt < request.Vm.StartFrom)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "invalid date" });
                }
                var advertising = new Advertising
                {
                    Sponsor = request.Vm.Sponsor,
                    Url = request.Vm.Url,
                    StartFrom = request.Vm.StartFrom,
                    EndAt = request.Vm.EndAt,
                    ImgUrl = _filesAccessor.UploadFile(request.Vm.Image, "advertisings")
                };
                _context.Advertisings.Add(advertising);
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
