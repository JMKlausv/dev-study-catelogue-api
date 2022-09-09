using Application.Courses.Commands.CreateCourse;
using Application.Courses.Commands.DeleteCourse;
using Application.Courses.Commands.UpdateCourse;
using Application.Courses.Queries.GetAllCourses;
using Application.Courses.Queries.GetCourseByDifficulty;
using Application.Courses.Queries.GetCourseByFrameworkId;
using Application.Courses.Queries.GetCourseById;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dev_study_catelogue_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ApiControllerBase
    {

        // GET: api/<CourseController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? frameworkId , [FromQuery] string? difficulty)
        {
            if(frameworkId != null)
            {
                var query = new GetCourseByFrameworkIdQuery()
                {
                    FrameworkId = int.Parse(frameworkId)
                };
                var result1 = await Mediator.Send(query);
                return Ok(result1); 

            }
            if (difficulty != null)
            {
                var query = new GetCourseByDifficultyQuery()
                {
                    Difficulty = difficulty
                };
                var result1 = await Mediator.Send(query);
                return Ok(result1);

            }

            var result =  await Mediator.Send(new GetAllCoursesQuery());
            return Ok(result);  

        }


        // GET api/<CourseController>/5

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetCourseByIdQuery()
            {
                Id = id,
            };
            var result = await Mediator.Send(query);    
            return Ok(result);
        }

        // POST api/<CourseController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCourseCommand command)
        {
            var response = await Mediator.Send(command);    
            return Ok(response);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCourseCommand command)
        {
            command.Id = id;
            var response = await Mediator.Send(command);
            return Ok(response);
        }


        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult>  Delete(int id)
        {
            var command = new DeleteCourseCommand()
            {
                Id = id
            };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}
