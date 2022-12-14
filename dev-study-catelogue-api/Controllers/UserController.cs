using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.CreateUser;
using Application.User.Commands.UpdateUserLikes;
using Application.User.Queries.GetUserPreferences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dev_study_catelogue_api.Controllers
{
   public record userResource
    {
        public string Id { get; set; }  
    }
    public class UserController : ApiControllerBase
    {
    /*
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    */
    
        // GET api/<UserController>/userPreference/
        [HttpGet()]
        [Route("userPreference/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var command = new GetUserPreferencesQuery
            {
                UserId = id,   
            };
            var response = await Mediator.Send(command);
            return Ok(response);

        }
    

        // POST api/<UserController>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> authenticate([FromBody] AuthenticateUserCommand command)
        {
            var result = await  Mediator.Send(command);
            Console.WriteLine(result);  
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> create([FromBody] CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            var resObject = new { Id = result };
            return Ok(resObject);
        }

          // PUT api/<UserController>/5
          [HttpPut]
          [Route("updateLikes")]
          public async Task<IActionResult> Put( [FromBody] UpdateUserLikesCommand command)
          {
            var result = await Mediator.Send(command);
            var resObject = new {UserId = result};
            return Ok(resObject);
          }
   
        /*

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
