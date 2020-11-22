using System.Threading;
using System.Threading.Tasks;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;

namespace Application.Dishes
{
    public class Details
    {
        public class Query: IRequest<Dish>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Dish>
        {
            private readonly IDishRepository context;

            public Handler(IDishRepository context)
            {
                this.context = context;
            }

            public async Task<Dish> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Get(request.Id);
            }
        }
    }
}