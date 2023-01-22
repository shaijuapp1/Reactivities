using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TableNames
{
    public class List
    {
        public class Query : IRequest<Result<List<TableNameDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<TableNameDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<TableNameDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tableNames = await _context.TableNames
                    .ProjectTo<TableNameDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<List<TableNameDto>>.Success(tableNames);
            }
        }
    }
}