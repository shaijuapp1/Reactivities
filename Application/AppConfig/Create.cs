using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppConfigs
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AppConfig AppConfig { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.AppConfig).SetValidator(new AppConfigValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                //
                // var appConfigType = await _context.AppConfigTypes.FindAsync(request.AppConfig.ConfigType.Id);
                // request.AppConfig.ConfigType = appConfigType;

                request.AppConfig.ConfigType = await _context.AppConfigTypes
                    .Where(x => x.Id == request.AppConfig.ConfigType.Id ).FirstOrDefaultAsync();

                _context.AppConfigs.Add(request.AppConfig);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create AppConfig");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}