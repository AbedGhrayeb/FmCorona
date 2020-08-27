using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Presenters.Commands
{
    public class AddPresenters
    {
        public class AddPresentersCommand : IRequest
        {
            public AddPresentersCommand(AddPresenterVm vm)
            {
                Vm = vm;
            }

            public AddPresenterVm Vm { get; }
        }
        public class Handel : IRequestHandler<AddPresentersCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(AddPresentersCommand request, CancellationToken cancellationToken)
            {
                var presenter = new Presenter
                {
                    FirstName = request.Vm.FirstName,
                    LastName = request.Vm.LastName,
                    Bio = request.Vm.Bio,
                    ImgUrl = _filesAccessor.UploadFile(request.Vm.Image,"presenters"),
                };
                _context.Presenters.Add(presenter);
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
