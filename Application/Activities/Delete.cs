using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var activityToDelete = await datacontext.Activities.FindAsync(request.Id);
                datacontext.Remove(activityToDelete);
                await datacontext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}