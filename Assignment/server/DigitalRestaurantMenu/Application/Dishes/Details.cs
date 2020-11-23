using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dishes.Dtos;
using Application.Errors;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;

namespace Application.Dishes
{
    public class Details
    {
        public class Query : IRequest<ResponseWrapper<DishDto>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ResponseWrapper<DishDto>>
        {
            private readonly IDishRepository _context;
            private readonly IMapper _mapper;

            public Handler(IDishRepository context, IMapper mapper)
            {
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResponseWrapper<DishDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dish = await _context.Get(request.Id);

                if (dish == null)
                    throw new RestException(HttpStatusCode.NotFound, "No dish found with this Id");
                
                var contents = _mapper.Map<Dish, DishDto>(dish);
                var responseWrapper = ResponseWrapper<DishDto>.GetInstance((int)HttpStatusCode.OK, null, true, contents);
                return responseWrapper;
            }
        }
    }
}