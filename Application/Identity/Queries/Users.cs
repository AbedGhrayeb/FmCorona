using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries
{
    public class Users
    {
        public class GetProfile : IRequest<List<AppUser>> { }
        public class Handler : IRequestHandler<GetProfile, List<AppUser>>
        {
            private readonly DataContext _context;
            private readonly ICurrentUser _currentUser;
            private readonly IMapper _mapper;

            public Handler(DataContext context, ICurrentUser currentUser, IMapper mapper)
            {
                _context = context;
                _currentUser = currentUser;
                _mapper = mapper;
            }
            public async Task<List<AppUser>> Handle(GetProfile request, CancellationToken cancellationToken)
            {
                var users = await _context.Users.ToListAsync();
                //var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _currentUser.Username);
                //var UserToReturn = _mapper.Map<UserDto>(user);
                return users;

            }
        }
    }
}
