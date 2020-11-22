using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Dishes.Dtos;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;

namespace Application.Dishes
{
    public class List
    {
        public class Query : IRequest<List<Dish>> { }

        public class Handler : IRequestHandler<Query, List<Dish>>
        {
            private readonly IDishRepository _context;
            private readonly IMapper _mapper;
            public Handler(IDishRepository context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<List<Dish>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dishes = await _context.GetAll();
                var dishesDto = _mapper.Map<List<Dish>, List<DishDto>>(dishes);
                return dishes; 
            }
        }
    }
}