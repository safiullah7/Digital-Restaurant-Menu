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
    public class Delete
    {
        public class Command : IRequest<ResponseWrapper<Delete>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResponseWrapper<Delete>>
        {
            private readonly ILogger<Delete> _logger;
            private readonly ICategoryRepository _context;
            public Handler(ICategoryRepository context, ILogger<Delete> logger)
            {
                this._context = context;
                this._logger = logger;
            }

            public async Task<ResponseWrapper<Delete>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _context.Get(request.Id);
                    if (category == null)
                        throw new RestException(HttpStatusCode.NotFound, "No category found with this Id");
                        
                    await _context.Delete(request.Id);
                    _logger.LogInformation("Successfully deleted the category");
                    var responseWrapper = ResponseWrapper<Delete>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem deleting the category");
                    throw new Exception("Problem deleting the category");
                }
            }
        }
    }
}