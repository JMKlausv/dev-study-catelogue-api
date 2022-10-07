using Application.Courses.Commands.CreateCourse;
using Application.Courses.Commands.DeleteCourse;
using Application.Courses.Commands.UpdateCourse;
using Application.Courses.Queries.GetAllCourses;
using Application.Courses.Queries.GetAllCoursesCount;
using Application.Courses.Queries.GetCourseByDifficulty;
using Application.Courses.Queries.GetCourseByFrameworkId;
using Application.Courses.Queries.GetCourseById;
using Application.Courses.Queries.GetUserCourseCount;
using MediatR;
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
        public async Task<IActionResult> Get( [FromQuery] string? orderBy,[FromQuery] string? frameworkId , [FromQuery] string? difficulty , [FromQuery] string? maxCount , [FromQuery] string? UploadedByUser , [FromQuery] string? userDivision)
        {
            System.Diagnostics.Debug.WriteLine(UploadedByUser);
            if (frameworkId != null)
            {
                var query = new GetCourseByFrameworkIdQuery()
                {
                    FrameworkId = int.Parse(frameworkId),
                    MaxCount = maxCount != null ? int.Parse(maxCount) : null,
                    UploadedByUser = UploadedByUser,
                    userDivision = userDivision

                };
                var result1 = await Mediator.Send(query);
                return Ok(result1); 

            }
            if (difficulty != null)
            {
                var query = new GetCourseByDifficultyQuery()
                {
                    Difficulty = difficulty,
                    MaxCount = maxCount != null ? int.Parse(maxCount) : null,
                    UploadedByUser = UploadedByUser ,
                    UserDivision = userDivision 

                };
                var result1 = await Mediator.Send(query);
                return Ok(result1);

            }
            var queryDefault = new GetAllCoursesQuery()
            {
              
                MaxCount = maxCount != null ? int.Parse(maxCount) : null,
                UploadedByUser =UploadedByUser,
                UserDivision = userDivision ,
                OrderBy = orderBy

            };
            var result =  await Mediator.Send(queryDefault);
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

        //GET api/CourseController>/Count
        [HttpGet()]
        [Route("count")]
        public async Task<IActionResult> GetCourseCount([FromQuery] string? userDivision)
        {
            int result;
            if(userDivision == null)
            {
               var query = new GetAllCourseCountQuery();

                result = await Mediator.Send(query);
            }
            else
            {
                var query = new GetUserCourseCountQuery();

                result = await Mediator.Send(query);
            }
            


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
