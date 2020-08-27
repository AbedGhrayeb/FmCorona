using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Artists.Commands
{
    public class AddArtist
    {
        public class AddArtistCommand : IRequest
        {
            public AddArtistCommand(AddArtistVm vm)
            {
                Vm = vm;
            }

            public AddArtistVm Vm { get; }
        }
        public class Handel : IRequestHandler<AddArtistCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(AddArtistCommand request, CancellationToken cancellationToken)
            {
                var artist = new Artist
                {
                    Id=Guid.NewGuid().ToString(),
                    Name=request.Vm.Name,
                    ImgUrl = _filesAccessor.UploadFile(request.Vm.Image,"artists"),
                };
                _context.Artists.Add(artist);
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
