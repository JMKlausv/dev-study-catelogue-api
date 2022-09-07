using Application.Frameworks.Commands.CreateFramework;
using Application.Frameworks.Commands.DeleteFramework;
using Application.Frameworks.Commands.UpdateFramework;
using Application.Frameworks.Queries.GetAllFrameworks;
using Application.Frameworks.Queries.GetFrameworksByLanguageId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dev_study_catelogue_api.Controllers
{
    //[Authorize]
    public class FrameworkController : ApiControllerBase
    {
        // GET: api/<FrameworkController>

        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] string? languageId)
        {
            if (languageId != null)
            {
                var command = new GetFrameworksByLanguageIdQuery()
                {
                    LanguageId = int.Parse(languageId)
                };
                return Ok(await Mediator.Send(command));
            }
            return Ok(await Mediator.Send(new GetAllFrameworksQuery()));
        }

/*
        // GET api/<FrameworkController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
*/
        // POST api/<FrameworkController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateFrameworkCommand command)
        {

            return Ok(await Mediator.Send(command));

        }

        // PUT api/<FrameworkController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateFrameworkCommand command)
        {
            command.Id = id;
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        // DELETE api/<FrameworkController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteFrameworkCommand()
            {
                Id = id,
            };
            return Ok(await Mediator.Send(command));
        }
    }
}
