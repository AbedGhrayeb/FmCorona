using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Advertisings.Queries
{
    public class AdvertisingsList
    {
        public class AdvertisingsQuery : IRequest<IReadOnlyList<AdvertisingVm>> { }
        public class Handler : IRequestHandler<AdvertisingsQuery, IReadOnlyList<AdvertisingVm>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IReadOnlyList<AdvertisingVm>> Handle(AdvertisingsQuery request, CancellationToken cancellationToken)
            {
                var query = await _context.Advertisings.Where(x
                    => DateTime.Now.CompareTo(x.StartFrom) >= 0
                    && DateTime.Now.CompareTo(x.EndAt) <= 0)
                    .ToListAsync();
                var advertisingsToReturen = _mapper.Map<IReadOnlyList<AdvertisingVm>>(query);
                return advertisingsToReturen;
            }
        }
    }
}
