using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppConfigs
{
    public class List
    {
        public class Query : IRequest<Result<List<AppConfigDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<AppConfigDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<AppConfigDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appConfig = await _context.AppConfigs
                    .ProjectTo<AppConfigDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<List<AppConfigDto>>.Success(appConfig);
            }
        }
    }
}