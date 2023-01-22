using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TableNames
{
    public class Details
    {
        public class Query : IRequest<Result<TableNameDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TableNameDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<TableNameDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tableName = await _context.TableNames
                    .ProjectTo<TableNameDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<TableNameDto>.Success(tableName);
            }
        }
    }
}