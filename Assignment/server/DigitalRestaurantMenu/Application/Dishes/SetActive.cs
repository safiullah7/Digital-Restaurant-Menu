using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;

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
            public Handler(IDishRepository context)
            {
                this._context = context;
            }

            public async Task<ResponseWrapper<SetActive>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await _context.ChangeActiveState(request.Id, request.Active);
                    var responseWrapper = ResponseWrapper<SetActive>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch(Exception)
                {
                    throw new Exception("Problem saving new dish");
                }
            }
        }
    }
}