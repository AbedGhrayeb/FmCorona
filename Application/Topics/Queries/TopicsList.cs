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
    public class TopicsList
    {
        public class TopicsEnvelope
        {
            public List<Topic> TopicsDtos { get; set; }
        }
        public class TopicsListQuery : IRequest<TopicsEnvelope> { }
        public class Hndler : IRequestHandler<TopicsListQuery, TopicsEnvelope>
        {
            private readonly DataContext _context;

            public Hndler(DataContext context)
            {
                _context = context;
            }
            public async Task<TopicsEnvelope> Handle(TopicsListQuery request, CancellationToken cancellationToken)
            {
                var topics = await _context.Topics.ToListAsync();
                if (topics.Count==0)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, new { msg = "not added topics yet" });
                }
                var topicsToReturen = new TopicsEnvelope { TopicsDtos = topics };
                return topicsToReturen;
            }
        }
    }
}
