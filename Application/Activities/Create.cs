using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Activity>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext datacontext;

            public Handler(DataContext datacontext)
            {
                this.datacontext = datacontext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                datacontext.Activities.Add(request.Activity);
                await datacontext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}