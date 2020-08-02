using Application.Common.Errors;
using Application.Presenters;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Topics.Queries
{
    public class SocialMediaList
    {
        public class SocialMediaEnvelope
        {
            public List<SocialMediaDto> SocialMediaDtos { get; set; }
        }
        public class SocialMediaListQuery : IRequest<SocialMediaEnvelope> { }
        public class Hndler : IRequestHandler<SocialMediaListQuery, SocialMediaEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Hndler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<SocialMediaEnvelope> Handle(SocialMediaListQuery request, CancellationToken cancellationToken)
            {
                var socialMedia = await _context.SocialMedias.ToListAsync();
                if (socialMedia.Count == 0)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, new { msg = "not added topics yet" });
                }
                var socialMediaToReturen = new SocialMediaEnvelope { SocialMediaDtos = _mapper.Map<List<SocialMediaDto>>(socialMedia) };
                return socialMediaToReturen;
            }
        }
    }
}
