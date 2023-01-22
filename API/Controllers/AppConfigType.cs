using Application.AppConfigs;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [AllowAnonymous]
    public class AppConfigController : BaseApiController
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
        public async Task<IActionResult> CreateAppConfigType(AppConfig AppConfig)
        {
            return HandleResult(await Mediator.Send(new Create.Command { AppConfig = AppConfig }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, AppConfig appConfig)
        {
            appConfig.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { AppConfig = appConfig }));
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