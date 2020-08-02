using Application.Common.Errors;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Artists
{
    public class AddFavoriteArtist
    {
        public class AddCommand : IRequest
        {
            public AddCommand()
            {
                ArtistsIds = new List<string>();
            }
            public List<string> ArtistsIds { get; set; }
        }
        public class handler : IRequestHandler<AddCommand>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Unit> Handle(AddCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.Username);
                if (request.ArtistsIds.Count == 0)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, new { msg = "You dont choose any artist" });
                }
                else
                {
                    var artists = _context.Artists;
                    foreach (var artist in artists)
                    {
                        if (request.ArtistsIds.Contains(artist.Id))
                        {
                            var favoriteArtist = new FavoriteArtist
                            {
                                AppUser = user,
                                Artist = artist
                            };
                            _context.FavoriteArtists.Add(favoriteArtist);
                            if (await _context.SaveChangesAsync() == 0)
                            {
                                throw new Exception("Proplem Saving Changes");
                            }

                        }
                    }
                    return Unit.Value;
                }
            }
        }
    }
}
