using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;

namespace Application.Dishes
{
    public class SetActive
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
            public bool Active { get; set; }
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
                    await _context.ChangeActiveState(request.Id, request.Active);
                    return Unit.Value;
                }
                catch(Exception)
                {
                    throw new Exception("Problem saving new dish");
                }
            }
        }
    }
}