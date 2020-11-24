using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Categories;
using Application.Categories.Dtos;
using Domain.models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ResponseWrapper<List<CategoryDto>>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
        
        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<Create>>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<Delete>>> Delete(string id)
        {
            return await Mediator.Send(new Delete.Command{Id = id});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<CategoryDto>>> Details(string id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }
    }
}