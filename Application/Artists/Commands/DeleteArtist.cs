using Application.Common.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articals.Commands
{
    public class DeleteArtist
    {
        public class DeleteArtistCommand : IRequest
        {
            public string Id { get; set; }
        }
        public class Handel : IRequestHandler<DeleteArtistCommand>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
            {

                var artist = await _context.Artists
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                if (artist == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }

                _context.Artists.Remove(artist);
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
