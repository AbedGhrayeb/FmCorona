using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Topics.Queries
{
    public class Lives
    {
        public class LivesQuery : IRequest<IReadOnlyList<Live>> { }
        public class Handler : IRequestHandler<LivesQuery, IReadOnlyList<Live>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<IReadOnlyList<Live>> Handle(LivesQuery request, CancellationToken cancellationToken)
            {
                return await _context.Lives.ToListAsync();

            }
        }
    }
}
