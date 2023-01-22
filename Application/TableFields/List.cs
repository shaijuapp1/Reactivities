using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TableFields
{
    public class List
    {
        public class Query : IRequest<Result<List<TableFieldDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<TableFieldDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<TableFieldDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tableField = await _context.TableFields
                    .ProjectTo<TableFieldDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<List<TableFieldDto>>.Success(tableField);
            }
        }
    }
}