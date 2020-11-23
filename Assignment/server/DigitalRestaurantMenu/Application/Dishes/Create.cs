using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Dishes
{
    public class Create
    {
        public class Command : IRequest<ResponseWrapper<Create>>
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
            public string Availability { get; set; }
            public bool Active { get; set; }
            public int PreparationTime { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().NotNull();
                RuleFor(x => x.Category).NotEmpty().NotNull();
                RuleFor(x => x.Availability).NotEmpty().NotNull();
                RuleFor(x => x.PreparationTime).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, ResponseWrapper<Create>>
        {
            private readonly IDishRepository context;
            private readonly IMapper _mapper;
            private readonly ILogger<Create> _logger;

            public Handler(IDishRepository context, IMapper mapper, ILogger<Create> logger)
            {
                this._logger = logger;
                this._mapper = mapper;
                this.context = context;
            }

            async Task<ResponseWrapper<Create>> IRequestHandler<Command, ResponseWrapper<Create>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var dish = _mapper.Map<Command, Dish>(request);
                dish.CreatedAt = DateTime.Now;
                dish.UpdatedAt = DateTime.Now;
                try
                {
                    await context.Create(dish);
                    _logger.LogInformation("Successfully created the dish");
                    var responseWrapper = ResponseWrapper<Create>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem saving new dish");
                    throw new Exception("Problem saving new dish");
                }
            }
        }
    }
}