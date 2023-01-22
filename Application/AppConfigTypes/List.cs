using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppConfigTypes
{
    public class List
    {
        public class Query : IRequest<Result<List<AppConfigTypeDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<AppConfigTypeDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<AppConfigTypeDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appConfigTypes = await _context.AppConfigTypes
                    .ProjectTo<AppConfigTypeDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<List<AppConfigTypeDto>>.Success(appConfigTypes);
            }
        }
    }
}