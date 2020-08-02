using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Queries
{
    public class EpisodeDetails
    {
        public class EpisodeDetailsQuery : IRequest<EpisodeDto>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<EpisodeDetailsQuery, EpisodeDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<EpisodeDto> Handle(EpisodeDetailsQuery request, CancellationToken cancellationToken)
            {
                var episode = await _context.Episodes.FindAsync(request.Id);
                if (episode == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "invalid episode id" });
                }

                var episodeToReturn = _mapper.Map<EpisodeDto>(episode);
                return episodeToReturn;
            }
        }
    }
}
