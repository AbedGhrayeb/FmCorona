using Application.Common.Errors;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Advertisings.Commands
{
    public class EditAdvertising
    {
        public class EditAdvertisingCommand : IRequest
        {
            public EditAdvertisingCommand(EditAdvertisingVm vm)
            {
                Vm = vm;
            }

            public EditAdvertisingVm Vm { get; }
        }
        public class Handler : IRequestHandler<EditAdvertisingCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handler(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(EditAdvertisingCommand request, CancellationToken cancellationToken)
            {
                var advertising = await _context.Advertisings.FindAsync(request.Vm.Id);
                if (advertising == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                advertising.Sponsor = request.Vm.Sponsor;
                advertising.Url = request.Vm.Url;
                advertising.StartFrom = request.Vm.StartFrom;
                advertising.EndAt = request.Vm.EndAt;
                advertising.ImgUrl = _filesAccessor.ChangeFile(request.Vm.Image, advertising.ImgUrl, "advertisings");

                _context.Advertisings.Update(advertising);
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
