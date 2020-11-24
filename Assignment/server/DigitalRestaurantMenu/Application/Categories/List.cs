using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Dtos;
using AutoMapper;
using Domain.models;
using Domain.repository.repositories.interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Categories
{
    public class List
    {
        public class Query : IRequest<ResponseWrapper<List<CategoryDto>>> { }

        public class Handler : IRequestHandler<Query, ResponseWrapper<List<CategoryDto>>>
        {
            private readonly ILogger<List> _logger;
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _context;
            public Handler(ICategoryRepository context, IMapper mapper, ILogger<List> logger)
            {
                this._context = context;
                this._mapper = mapper;
                this._logger = logger;
            }

            public async Task<ResponseWrapper<List<CategoryDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var categories = await _context.GetAll();
                    _logger.LogInformation("Successfully retreived the categories");
                    var contents = _mapper.Map<List<Category>, List<CategoryDto>>(categories);

                    var responseWrapper = ResponseWrapper<List<CategoryDto>>.GetInstance((int)HttpStatusCode.OK, null, true, contents);
                    return responseWrapper;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Problem getting the categories");
                    throw new Exception("Problem getting the categories");
                }
            }
        }
    }
}