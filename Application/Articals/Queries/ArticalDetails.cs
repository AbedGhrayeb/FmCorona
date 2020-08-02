using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articals.Queries
{

    public class ArticalDetails
    {
        public class HighlightEnvelope
        {
            public ArticalDto Highlight { get; set; }
        }
        public class ArticalQuery : IRequest<HighlightEnvelope>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<ArticalQuery, HighlightEnvelope>
        {

            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<HighlightEnvelope> Handle(ArticalQuery request, CancellationToken cancellationToken)
            {
                var artical = await _context.Articals.FindAsync(request.Id);
                if (artical == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "artical not found" });
                }
                var articalDto = new HighlightEnvelope { Highlight = _mapper.Map<ArticalDto>(artical) };
                return articalDto;
            }
        }

    }
}

