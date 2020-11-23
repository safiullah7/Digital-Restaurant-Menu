using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Dishes
{
    public class SetActive
    {
        public class Command : IRequest<ResponseWrapper<SetActive>>
        {
            public string Id { get; set; }
            public bool Active { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResponseWrapper<SetActive>>
        {
            private readonly IDishRepository _context;
            private readonly ILogger<SetActive> _logger;
            public Handler(IDishRepository context, ILogger<SetActive> logger)
            {
                this._logger = logger;
                this._context = context;
            }

            public async Task<ResponseWrapper<SetActive>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var dish = await _context.Get(request.Id);
                    if (dish == null)
                        throw new RestException(HttpStatusCode.NotFound, "No dish found with this Id");

                    await _context.ChangeActiveState(request.Id, request.Active);
                    var responseWrapper = ResponseWrapper<SetActive>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem setting the active status of the dish");
                    throw new Exception("Problem setting the active status of the dish");
                }
            }
        }
    }
}