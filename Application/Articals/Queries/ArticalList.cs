using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articals.Queries
{

    public class ArticalList
    {
        public class HighlightsEnvelope
        {
            public List<ArticalDto> Highlights { get; set; }
        }

        public class ArticalListQuery : IRequest<HighlightsEnvelope> { }
        public class Handler : IRequestHandler<ArticalListQuery, HighlightsEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<HighlightsEnvelope> Handle(ArticalListQuery request, CancellationToken cancellationToken)
            {
                var articals = _context.Articals;

                var orderedArtical = await articals.OrderByDescending(x => x.CreateAt).ToListAsync();
                var highlight = new HighlightsEnvelope
                {
                    Highlights = _mapper.Map<List<ArticalDto>>(orderedArtical)
                };
                return highlight;

            }
        }
    }
}
