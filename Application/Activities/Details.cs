using MediatR;
using Domain;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Activity>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly DataContext dataContext;
            public Handler(DataContext context)
            {
                this.dataContext = context;
            }

            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                return await dataContext.Activities.FindAsync(request.Id);
            }
        }
    }
}