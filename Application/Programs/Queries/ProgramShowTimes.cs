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
    public class ProgramShowTimes
    {
        public class ProgramShowTimesQuery : IRequest<List<ProgramShowTimesVm>>
        {
            public int ProgramId { get; set; }
        }
        public class Handel : IRequestHandler<ProgramShowTimesQuery, List<ProgramShowTimesVm>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handel(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<ProgramShowTimesVm>> Handle(ProgramShowTimesQuery request, CancellationToken cancellationToken)
            {
                var program = await _context.Programs.FindAsync(request.ProgramId);
                if (program == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                var showTimes = await _context.ShowTimes.Where(x => x.ProgramId == request.ProgramId).ToListAsync();
                var programShowTimes = _mapper.Map<List<ProgramShowTimesVm>>(showTimes);
                return programShowTimes;
            }
        }
    }
}
