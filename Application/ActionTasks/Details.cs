using Domain;
using MediatR;
using Persistence;

namespace Application.ActionTasks
{
    public class Details
    {
        public class Query : IRequest<ActionTask>
        {
            public Guid Id { get; set; }

        public class Handler : IRequestHandler<Query, ActionTask>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<ActionTask> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.ActionTasks.FindAsync(request.Id);
            }
        }
        }
    }

    
}