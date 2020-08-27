using Application.Artists;
using Application.Common.Errors;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articals.Commands
{
    public class EditArtist
    {
        public class EditArtistCommand : IRequest
        {
            public EditArtistCommand(EditArtistVm vm)
            {
                Vm = vm;
            }

            public EditArtistVm Vm { get; }
        }
        public class Handel : IRequestHandler<EditArtistCommand>
        {
            private readonly DataContext _context;
            private readonly IFilesAccessor _filesAccessor;

            public Handel(DataContext context, IFilesAccessor filesAccessor)
            {
                _context = context;
                _filesAccessor = filesAccessor;
            }
            public async Task<Unit> Handle(EditArtistCommand request, CancellationToken cancellationToken)
            {

                var artist = await _context.Artists.FindAsync(request.Vm.Id);
                if (artist == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                {
                    artist.Name = request.Vm.Name ?? artist.Name;
                    artist.ImgUrl = _filesAccessor.ChangeFile(request.Vm.Image,artist.ImgUrl,"artists") ?? artist.ImgUrl;

                };
                _context.Artists.Update(artist);
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
