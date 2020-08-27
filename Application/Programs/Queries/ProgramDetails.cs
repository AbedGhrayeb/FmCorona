using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Queries
{
    public class ProgramDetails
    {
        public class ProgramDetailsQuery : IRequest<ProgramDto>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<ProgramDetailsQuery, ProgramDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ProgramDto> Handle(ProgramDetailsQuery request, CancellationToken cancellationToken)
            {
                var program = await _context.Programs.FindAsync(request.Id);
                if (program == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }

                var programToReturn = _mapper.Map<ProgramDto>(program);
                return programToReturn;
            }
        }
    }
}
