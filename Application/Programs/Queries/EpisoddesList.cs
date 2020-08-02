using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Queries
{
    public class EpisodesList
    {
        public class EpisodesEnvelope
        {
            public List<EpisodeDto> EpisodesDtos { get; set; }
        }
        public class EpisodesListQuery : IRequest<EpisodesEnvelope>
        {
            public EpisodesListQuery(int? page,int? limit,string title)
            {
                Page = page;
                Limit = limit;
                Title = title;
            }
            public int ProgramId { get; set; }
            public int? Page { get; }
            public int? Limit { get; }
            public int? Num { get; }
            public string Title { get; }
        }
        public class Handler : IRequestHandler<EpisodesListQuery, EpisodesEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<EpisodesEnvelope> Handle(EpisodesListQuery request, CancellationToken cancellationToken)
            {
                var program = await _context.Programs.SingleOrDefaultAsync(x => x.Id == request.ProgramId);
                if (program == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "invalid program id" });
                }
                var queryable =  _context.Episodes.Where(x => x.Program.Id == request.ProgramId)
                    .OrderByDescending(x => x.ShowDate).AsQueryable();
                if (queryable == null || queryable.Count() == 0)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "no episodes added yet" });
                }
                if (!string.IsNullOrEmpty(request.Title))
                {
                    queryable = queryable.Where(x => x.Title.ToLower().Contains(request.Title.ToLower()));
                }
                var episodes = await queryable.Skip(request.Page ?? 0).Take(request.Limit ?? 10).ToListAsync();

                var episodesToReturn = new EpisodesEnvelope
                {
                    EpisodesDtos = _mapper.Map<List<EpisodeDto>>(episodes)
                };
                return episodesToReturn;
            }
        }
    }
}
