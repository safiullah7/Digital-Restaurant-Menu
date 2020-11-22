using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;

namespace Application.Dishes
{
    public class Edit
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
            public string Availability { get; set; }
            public bool Active { get; set; }
            public int PreparationTime { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDishRepository _context;
            private readonly IMapper _mapper;

            public Handler(IDishRepository context, IMapper mapper)
            {
                this._mapper = mapper;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var dishFromDb = await _context.Get(request.Id);
                if (dishFromDb == null)
                {
                    //TODO REST exception
                }
                var dish = _mapper.Map<Command, Dish>(request);
                dish.UpdatedAt = DateTime.Now;
                dish.CreatedAt = dishFromDb.CreatedAt;

                try
                {
                    await _context.Update(request.Id, dish);
                    return Unit.Value;
                    // TODO generic response
                }
                catch (Exception)
                {
                    throw new Exception("Problem updating the dish");
                }
            }
        }
    }
}