using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.repository.repositories.interfaces;
using MediatR;
using Domain.models;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Application.Dishes
{
    public class Delete
    {
        public class Command : IRequest<ResponseWrapper<Delete>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResponseWrapper<Delete>>
        {
            private readonly IDishRepository _context;
            private readonly ILogger<Delete> _logger;
            public Handler(IDishRepository context, ILogger<Delete> logger)
            {
                this._logger = logger;
                this._context = context;
            }

            public async Task<ResponseWrapper<Delete>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await _context.Delete(request.Id);
                    _logger.LogInformation("Successfully deleted the dish");
                    var responseWrapper = ResponseWrapper<Delete>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem deleting the dish");
                    throw new Exception("Problem deleting the dish");
                }
            }
        }
    }
}