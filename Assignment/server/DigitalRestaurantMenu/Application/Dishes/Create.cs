using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using FluentValidation;
using MediatR;

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

            public Handler(IDishRepository context, IMapper mapper)
            {
                this._mapper = mapper;
                this.context = context;
            }

            async Task<ResponseWrapper<Create>> IRequestHandler<Command, ResponseWrapper<Create>>.Handle(Command request, CancellationToken cancellationToken)
            {
                // TODO: Add custom exception handler
                
                var dish = _mapper.Map<Command, Dish>(request);
                dish.CreatedAt = DateTime.Now;
                dish.UpdatedAt = DateTime.Now;
                try
                {
                    await context.Create(dish);
                    var responseWrapper = ResponseWrapper<Create>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch (Exception)
                {
                    throw new Exception("Problem saving new dish");
                }
            }
        }
    }
}