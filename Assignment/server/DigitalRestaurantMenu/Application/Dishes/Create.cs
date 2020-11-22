using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;

namespace Application.Dishes
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
            public string Availability { get; set; }
            public bool Active { get; set; }
            public int PreparationTime { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDishRepository context;
            private readonly IMapper _mapper;

            public Handler(IDishRepository context, IMapper mapper)
            {
                this._mapper = mapper;
                this.context = context;
            }

            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request, CancellationToken cancellationToken)
            {
                // TODO: Add AutoMapper
                // TODO: Add custom exception handler
                
                var dish = _mapper.Map<Command, Dish>(request);
                dish.CreatedAt = DateTime.Now;
                dish.UpdatedAt = DateTime.Now;
                try
                {
                    await context.Create(dish);
                    return Unit.Value;
                }
                catch (Exception)
                {
                    throw new Exception("Problem saving new dish");
                }
            }
        }
    }
}