using Application.Common.Errors;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articals.Commands
{
    public class EditArtical
    {
        public class EditArticalCommand : IRequest
        {
            public EditArticalCommand(EditArticalVm vm)
            {
                Vm = vm;
            }

            public EditArticalVm Vm { get; }
        }
        public class Handel : IRequestHandler<EditArticalCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(EditArticalCommand request, CancellationToken cancellationToken)
            {

                var artical = await _context.Articals.FindAsync(request.Vm.Id);
                if (artical == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                {
                    artical.Details = request.Vm.Details ?? artical.Details;
                    artical.ImgUrl = _filesAccessor.ChangeFile(request.Vm.Image, artical.ImgUrl,"articls") ?? artical.ImgUrl;
                    artical.ShortDescription = request.Vm.ShortDescription ?? artical.ShortDescription;
                    artical.Title = request.Vm.Title ?? artical.Title;
                };
                _context.Articals.Update(artical);
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
