using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Queries
{
    public class CurrentUser
    {
        public class CurrentUserQuery : IRequest<UserDto> { }
        public class Handler : IRequestHandler<CurrentUserQuery, UserDto>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _currentUser;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor currentUser, IMapper mapper)
            {
                _context = context;
                _currentUser = currentUser;
                _mapper = mapper;
            }
            public async Task<UserDto> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _currentUser.Username);
                var userDto = _mapper.Map<UserDto>(user);
                return userDto;

            }
        }
    }
}
