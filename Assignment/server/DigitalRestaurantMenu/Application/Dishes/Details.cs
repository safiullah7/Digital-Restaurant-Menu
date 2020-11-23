using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dishes.Dtos;
using Application.Errors;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

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
            private readonly ILogger<Details> _logger;

            public Handler(IDishRepository context, IMapper mapper, ILogger<Details> logger)
            {
                this._logger = logger;
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResponseWrapper<DishDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dish = await _context.Get(request.Id);

                    if (dish == null)
                        throw new RestException(HttpStatusCode.NotFound, "No dish found with this Id");

                    var contents = _mapper.Map<Dish, DishDto>(dish);
                    var responseWrapper = ResponseWrapper<DishDto>.GetInstance((int)HttpStatusCode.OK, null, true, contents);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem getting the dish");
                    throw new Exception("Problem getting the dish");
                }
            }
        }
    }
}