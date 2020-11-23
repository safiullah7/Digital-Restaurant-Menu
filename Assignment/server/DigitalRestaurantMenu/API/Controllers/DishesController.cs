using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Application.Dishes;
using System.Threading.Tasks;
using Domain.models;
using Application.Dishes.Dtos;

namespace API.Controllers
{
    public class DishesController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ResponseWrapper<List<DishDto>>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<Create>>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<DishDto>>> Details(string id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseWrapper<Edit>>> Edit(string id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpPut("{id}/{active}")]
        public async Task<ActionResult<ResponseWrapper<SetActive>>> SetActive(string id, bool active)
        {
            return await Mediator.Send(new SetActive.Command{Id = id, Active = active});
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<Delete>>> Delete(string id)
        {
            return await Mediator.Send(new Delete.Command{Id = id});
        }
    }
}