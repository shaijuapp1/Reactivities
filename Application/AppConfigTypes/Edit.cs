using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppConfigTypes
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AppConfigType AppConfigType { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AppConfigType).SetValidator(new AppConfigTypeValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var appConfigType = await _context.AppConfigTypes.FindAsync(request.AppConfigType.Id);
               
                if (appConfigType == null) return null;

                _mapper.Map(request.AppConfigType, appConfigType);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update AppConfigType = ");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}