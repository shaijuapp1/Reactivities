using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.ActionTasks
{
    public class Edit
    {
        public class Command : IRequest
        {
            public ActionTask Item { get; set; }
        }

         public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.ActionTasks.FindAsync(request.Item.Id);

                _mapper.Map(request.Item, item);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
              
        }
    }
}