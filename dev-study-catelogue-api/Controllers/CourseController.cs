using Application.Courses.Commands.CreateCourseCommand;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dev_study_catelogue_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ApiControllerBase
    {
/*
        // GET: api/<CourseController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
*/
/*
        // GET api/<CourseController>/5

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
*/
        // POST api/<CourseController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCourseCommand command)
        {
            var response = await Mediator.Send(command);    
            return Ok(response);
        }
/*
        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
*/
/*
        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
*/
    }
}
