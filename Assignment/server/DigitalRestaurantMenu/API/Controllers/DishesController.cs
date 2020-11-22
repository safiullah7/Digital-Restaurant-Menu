using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Application.Dishes;
using System.Threading.Tasks;
using Domain.models;

namespace API.Controllers
{
    public class DishesController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Dish>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<MediatR.Unit> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> Details(string id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPut("{id}")]
        public async Task<MediatR.Unit> Edit(Edit.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}/{active}")]
        public async Task<MediatR.Unit> SetActive(string id, bool active)
        {
            return await Mediator.Send(new SetActive.Command{Id = id, Active = active});
        }

        [HttpDelete("{id}")]
        public async Task<MediatR.Unit> Delete(string id)
        {
            return await Mediator.Send(new Delete.Command{Id = id});
        }
    }
}