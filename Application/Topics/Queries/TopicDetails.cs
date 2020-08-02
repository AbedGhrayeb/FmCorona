using Application.Common.Errors;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Topics.Queries
{
    public class TopicDetails
    {
        public class TopicEnvelope
        {
            public Topic TopicDto { get; set; }
        }
        public class TopicDeatailsQuery : IRequest<TopicEnvelope> 
        {
            public int Id { get; set; }
        }
        public class Hndler : IRequestHandler<TopicDeatailsQuery, TopicEnvelope>
        {
            private readonly DataContext _context;

            public Hndler(DataContext context)
            {
                _context = context;
            }
            public async Task<TopicEnvelope> Handle(TopicDeatailsQuery request, CancellationToken cancellationToken)
            {
                var topic = await _context.Topics.FindAsync(request.Id);
                if (topic==null)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, new { msg = "not found topic" });
                }
                var topicToReturen = new TopicEnvelope { TopicDto = topic };
                return topicToReturen;
            }
        }
    }
}
