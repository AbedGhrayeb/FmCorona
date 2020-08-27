using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Artists.Queries
{
    public class ArtistsList
    {
        public class ArtistsEnvelope
        {
            public List<ArtistDto> ArtistsDto { get; set; }
        }
        public class ArtistsListQuery : IRequest<ArtistsEnvelope> { }
        public class Handler : IRequestHandler<ArtistsListQuery, ArtistsEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ArtistsEnvelope> Handle(ArtistsListQuery request, CancellationToken cancellationToken)
            {
                var artists = await _context.Artists.ToListAsync();

                var artistsToReturen = new ArtistsEnvelope
                {
                    ArtistsDto = _mapper.Map<List<ArtistDto>>(artists)
                };

                return artistsToReturen;

            }
        }
    }
}
