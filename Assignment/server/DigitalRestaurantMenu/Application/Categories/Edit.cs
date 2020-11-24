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

namespace Application.Categories
{
    public class Edit
    {
        public class Command : IRequest<ResponseWrapper<Edit>>
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResponseWrapper<Edit>>
        {
            private readonly ILogger<Edit> _logger;
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _context;
            public Handler(ICategoryRepository context, IMapper mapper, ILogger<Edit> logger)
            {
                this._context = context;
                this._mapper = mapper;
                this._logger = logger;
            }

            public async Task<ResponseWrapper<Edit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var categoryFromDb = await _context.Get(request.Id);
                if (categoryFromDb == null)
                    throw new RestException(HttpStatusCode.NotFound, "No category found with this Id");

                var category = _mapper.Map<Command, Category>(request);
                category.UpdatedAt = DateTime.Now;
                category.CreatedAt = categoryFromDb.CreatedAt;

                try
                {
                    await _context.Update(request.Id, category);
                    _logger.LogInformation("Successfully updated the category");
                    var responseWrapper = ResponseWrapper<Edit>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem updating the category");
                    throw new Exception("Problem updating the category");
                }
            }
        }
    }
}