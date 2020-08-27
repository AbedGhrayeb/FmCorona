using Application.Common.Errors;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Presenters.Commands
{
    public class EditPresenter
    {
        public class EditPresenterCommand : IRequest
        {
            public EditPresenterCommand(EditPresenterVm vm)
            {
                Vm = vm;
            }

            public EditPresenterVm Vm { get; }
        }
        public class Handel : IRequestHandler<EditPresenterCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(EditPresenterCommand request, CancellationToken cancellationToken)
            {

                var presenter = await _context.Presenters.FindAsync(request.Vm.Id);
                if (presenter == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                {
                    presenter.FirstName = request.Vm.FirstName ?? presenter.FirstName;
                    presenter.LastName = request.Vm.LastName ?? presenter.LastName;
                    presenter.Bio = request.Vm.Bio ?? presenter.Bio;
                    presenter.ImgUrl = _filesAccessor.ChangeFile(request.Vm.Image, presenter.ImgUrl,"presenters") ?? presenter.ImgUrl;

                };
                _context.Presenters.Update(presenter);
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
