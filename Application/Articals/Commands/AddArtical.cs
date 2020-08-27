using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articals.Commands
{
    public class AddArtical
    {
        public class AddArticalCommand : IRequest
        {
            public AddArticalCommand(AddArticalVm vm)
            {
                Vm = vm;
            }

            public AddArticalVm Vm { get; }
        }
        public class Handel : IRequestHandler<AddArticalCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(AddArticalCommand request, CancellationToken cancellationToken)
            {
                var artical = new Artical
                {
                    CreateAt = DateTime.UtcNow,
                    Details = request.Vm.Details,
                    ImgUrl = _filesAccessor.UploadFile(request.Vm.Image,"articls"),
                    ShortDescription = request.Vm.ShortDescription,
                    Title = request.Vm.Title
                };
                _context.Articals.Add(artical);
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
