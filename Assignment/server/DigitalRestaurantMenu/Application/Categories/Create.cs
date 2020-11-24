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

namespace Application.Categories
{
    public class Create
    {
        public class Command : IRequest<ResponseWrapper<Create>>
        {
            public string Name { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, ResponseWrapper<Create>>
        {
            private readonly ILogger<Create> _logger;
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _context;

            public Handler(ICategoryRepository context, IMapper mapper, ILogger<Create> logger)
            {
                this._context = context;
                this._mapper = mapper;
                this._logger = logger;
            }

            public async Task<ResponseWrapper<Create>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<Command, Category>(request);
                category.CreatedAt = DateTime.Now;
                category.UpdatedAt = DateTime.Now;

                try
                {
                    await _context.Create(category);
                    _logger.LogInformation("Successfully created the category");
                    var responseWrapper = ResponseWrapper<Create>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem saving new category");
                    throw new Exception("Problem saving new category");
                }
            }
        }
    }
}