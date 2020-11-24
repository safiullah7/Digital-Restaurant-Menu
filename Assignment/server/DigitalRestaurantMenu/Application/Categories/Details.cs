using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Dtos;
using Application.Errors;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Categories
{
    public class Details
    {
        public class Query : IRequest<ResponseWrapper<CategoryDto>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ResponseWrapper<CategoryDto>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger<Details> _logger;
            private readonly ICategoryRepository _context;

            public Handler(ICategoryRepository context, IMapper mapper, ILogger<Details> logger)
            {
                this._logger = logger;
                this._mapper = mapper;
                this._context = context;
            }

            public async Task<ResponseWrapper<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _context.Get(request.Id);

                    if (category == null)
                        throw new RestException(HttpStatusCode.NotFound, "No category found with this Id");

                    var contents = _mapper.Map<Category, CategoryDto>(category);
                    var responseWrapper = ResponseWrapper<CategoryDto>.GetInstance((int)HttpStatusCode.OK, null, true, contents);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem getting the category");
                    throw new Exception("Problem getting the category");
                }
            }
        }
    }
}