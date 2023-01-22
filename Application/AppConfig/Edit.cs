using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppConfigs
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AppConfig AppConfig { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AppConfig).SetValidator(new AppConfigValidator());
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
                var appConfig = await _context.AppConfigs.FindAsync(request.AppConfig.Id);
               
                if (appConfig == null) return null;

                _mapper.Map(request.AppConfig, appConfig);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update AppConfigType = ");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}