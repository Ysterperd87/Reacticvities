using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext datacontext;
            private readonly IMapper mapper;

            public Handler(DataContext datacontext, IMapper mapper)
            {
                this.mapper = mapper;
                this.datacontext = datacontext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await datacontext.Activities.FindAsync(request.Activity.Id);
                mapper.Map(request.Activity, activity);

                await datacontext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}