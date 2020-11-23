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
        public class Query : IRequest<ResponseWrapper<List<DishDto>>> { }

        public class Handler : IRequestHandler<Query, ResponseWrapper<List<DishDto>>>
        {
            private readonly IDishRepository _context;
            private readonly IMapper _mapper;
            public Handler(IDishRepository context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResponseWrapper<List<DishDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dishes = await _context.GetAll();
                var contents = _mapper.Map<List<Dish>, List<DishDto>>(dishes);

                var responseWrapper = ResponseWrapper<List<DishDto>>.GetInstance((int)HttpStatusCode.OK, null, true, contents);
                return responseWrapper;
            }
        }
    }
}