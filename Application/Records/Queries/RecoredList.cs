using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Records.Queries
{
    public class RecoredList
    {
        public class RecoredsQuery : IRequest<List<Record>> { }
        public class Handler : IRequestHandler<RecoredsQuery, List<Record>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Record>> Handle(RecoredsQuery request, CancellationToken cancellationToken)
            {
                var recors = await _context.Records.OrderByDescending(x=>x.Id)
                    .ToListAsync();
                return recors;
            }
        }
    }
}
