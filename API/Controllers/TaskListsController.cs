using Application.ActionTasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActionTaskController : BaseApiController
    {
        [HttpGet] 
        public async Task<ActionResult<List<ActionTask>>> GetActivities()
        {
            return await  Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<ActionTask>> GetItem(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id} );
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ActionTask actiontask)
        {
            return Ok(await Mediator.Send(new Create.Command { item = actiontask }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, ActionTask item)
        {
            item.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Item = item }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command { Id = id }));
        }

    }
}