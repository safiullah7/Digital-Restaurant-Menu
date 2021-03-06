using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Dishes
{
    public class Edit
    {
        public class Command : IRequest<ResponseWrapper<Edit>>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
            public string Availability { get; set; }
            public bool Active { get; set; }
            public int PreparationTime { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResponseWrapper<Edit>>
        {
            private readonly IDishRepository _context;
            private readonly IMapper _mapper;
            private readonly ILogger<Edit> _logger;

            public Handler(IDishRepository context, IMapper mapper, ILogger<Edit> logger)
            {
                this._logger = logger;
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResponseWrapper<Edit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dishFromDb = await _context.Get(request.Id);
                if (dishFromDb == null)
                    throw new RestException(HttpStatusCode.NotFound, "No dish found with this Id");

                var dish = _mapper.Map<Command, Dish>(request);
                dish.UpdatedAt = DateTime.Now;
                dish.CreatedAt = dishFromDb.CreatedAt;

                try
                {
                    await _context.Update(request.Id, dish);
                    _logger.LogInformation("Successfully updated the dish");
                    var responseWrapper = ResponseWrapper<Edit>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem updating the dish");
                    throw new Exception("Problem updating the dish");
                }
            }
        }
    }
}