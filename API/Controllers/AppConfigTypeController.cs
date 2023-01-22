using Application.AppConfigTypes;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [AllowAnonymous]
    public class AppConfigTypeController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAppConfigTypes()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppConfigType(AppConfigType AppConfigType)
        {
            return HandleResult(await Mediator.Send(new Create.Command { AppConfigType = AppConfigType }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, AppConfigType appConfigType)
        {
            appConfigType.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { AppConfigType = appConfigType }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        // [HttpPost("{id}/attend")]
        // public async Task<IActionResult> Attend(Guid id)
        // {
        //     return HandleResult(await Mediator.Send(new UpdateAttendance.Command{Id = id}));
        // }
    }
}