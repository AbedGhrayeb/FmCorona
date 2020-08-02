using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Presenters.Queries
{
    public class PresenterDetails
    {
        public class PresenterEnvelope
        {
            public PresenterDto PresenterDto { get; set; }
        }
        public class PresenterDetailsQuery : IRequest<PresenterEnvelope>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<PresenterDetailsQuery, PresenterEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PresenterEnvelope> Handle(PresenterDetailsQuery request, CancellationToken cancellationToken)
            {
                var presenter = await _context.Presenters.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (presenter == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { msg = "no presenters added yet" });
                }
                var presenterToReturn = new PresenterEnvelope { PresenterDto = _mapper.Map<PresenterDto>(presenter) };
                return presenterToReturn;
            }
        }
    }
}
