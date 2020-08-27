using Application.Common.Errors;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Programs.Queries
{
    public class ProgramsList
    {
        public class ProgramsEnvelope
        {
            public List<ProgramDto> ProgramsDtos { get; set; }
        }
        public class ProgramsListQuery : IRequest<ProgramsEnvelope> { }
        public class Handler : IRequestHandler<ProgramsListQuery, ProgramsEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ProgramsEnvelope> Handle(ProgramsListQuery request, CancellationToken cancellationToken)
            {
                var programs = await _context.Programs.ToListAsync();

                var programsToReturen = new ProgramsEnvelope
                {
                    ProgramsDtos = _mapper.Map<List<ProgramDto>>(programs)
                };
                return programsToReturen;
            }
        }
    }
}
