using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TableFields
{
    public class Details
    {
        public class Query : IRequest<Result<TableFieldDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TableFieldDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<TableFieldDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tableField = await _context.TableFields
                    .ProjectTo<TableFieldDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<TableFieldDto>.Success(tableField);
            }
        }
    }
}