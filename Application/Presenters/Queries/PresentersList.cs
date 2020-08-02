using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Presenters.Queries
{
    public class PresentersList
    {
        public class PresentersEnvelope
        {
            public List<PresenterDto> PresentersDtos{ get; set; }
        }
        public class PresentersListQuery : IRequest<PresentersEnvelope>
        { }
        public class Handler : IRequestHandler<PresentersListQuery, PresentersEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PresentersEnvelope> Handle(PresentersListQuery request, CancellationToken cancellationToken)
            {
                var presenters = await _context.Presenters.ToListAsync();

                if (presenters == null || presenters.Count == 0)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "no presenters added yet" });
                }
                var presentersToReturn = new PresentersEnvelope
                {
                    PresentersDtos = _mapper.Map<List<PresenterDto>>(presenters)
                };
                return presentersToReturn;
            }
        }
    }
}
