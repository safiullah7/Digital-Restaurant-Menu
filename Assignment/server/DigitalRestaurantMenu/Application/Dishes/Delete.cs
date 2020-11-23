using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.repository.repositories.interfaces;
using MediatR;
using Domain.models;
using System.Net;

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
            public Handler(IDishRepository context)
            {
                this._context = context;
            }

            public async Task<ResponseWrapper<Delete>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await _context.Delete(request.Id);
                    var responseWrapper = ResponseWrapper<Delete>.GetInstance((int)HttpStatusCode.OK, null, true, null);
                    return responseWrapper;
                }
                catch(Exception)
                {
                    throw new Exception("Problem deleting the dish");
                }
            }
        }
    }
}