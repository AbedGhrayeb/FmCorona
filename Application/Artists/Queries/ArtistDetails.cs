using Application.Common.Errors;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Artists.Queries
{
    public class ArtistDetails
    {
        public class ArtistDetailsQuery:IRequest<EditArtistVm>
        {
            public string Id { get; set; }
        }
        public class Handel : IRequestHandler<ArtistDetailsQuery, EditArtistVm>
        {
            private readonly DataContext _context;

            public Handel(DataContext context)
            {
                _context = context;
            }
            public async Task<EditArtistVm> Handle(ArtistDetailsQuery request, CancellationToken cancellationToken)
            {
                var artist = await _context.Artists.FindAsync(request.Id);
                if (artist==null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
                var vm = new EditArtistVm { Id = artist.Id, Name = artist.Name, ImgUrl = artist.ImgUrl };
                return vm;
            }
        }
    }
}
