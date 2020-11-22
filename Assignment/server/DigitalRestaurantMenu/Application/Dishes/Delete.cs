using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.repository.repositories.interfaces;
using MediatR;

namespace Application.Dishes
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDishRepository _context;
            public Handler(IDishRepository context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await _context.Delete(request.Id);
                    return Unit.Value;
                }
                catch(Exception)
                {
                    throw new Exception("Problem deletinga the dish");
                }
            }
        }
    }
}