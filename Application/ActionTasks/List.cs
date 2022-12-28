using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ActionTasks
{
    public class List
    {
        public class Query : IRequest<List<ActionTask>> { }

        public class Handler : IRequestHandler<Query, List<ActionTask>>   
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<ActionTask>> Handle(Query request, CancellationToken token)
            {
                return await _context.ActionTasks.ToListAsync();
            }
        }

    }
}