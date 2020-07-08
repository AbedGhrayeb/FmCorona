using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries
{
    public class Profile
    {
        public class GetProfile : IRequest<UserDto> { }
        public class Handler : IRequestHandler<GetProfile, UserDto>
        {
            private readonly DataContext _context;
            private readonly ICurrentUser _currentUser;
            private readonly IMapper _mapper;

            public Handler(DataContext context,ICurrentUser currentUser,IMapper mapper)
            {
                _context = context;
                _currentUser = currentUser;
                _mapper = mapper;
            }
            public async Task<UserDto> Handle(GetProfile request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _currentUser.Username);
                var UserToReturn = _mapper.Map<UserDto>(user);
                return UserToReturn;
             
            }
        }
    }
}
