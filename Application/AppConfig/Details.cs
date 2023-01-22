using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppConfigs
{
    public class Details
    {
        public class Query : IRequest<Result<AppConfigDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AppConfigDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<AppConfigDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appConfig = await _context.AppConfigs
                    .ProjectTo<AppConfigDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<AppConfigDto>.Success(appConfig);
            }
        }
    }
}