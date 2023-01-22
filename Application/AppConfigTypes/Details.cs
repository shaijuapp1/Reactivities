using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppConfigTypes
{
    public class Details
    {
        public class Query : IRequest<Result<AppConfigTypeDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AppConfigTypeDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<AppConfigTypeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appConfigType = await _context.AppConfigTypes
                    .ProjectTo<AppConfigTypeDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<AppConfigTypeDto>.Success(appConfigType);
            }
        }
    }
}