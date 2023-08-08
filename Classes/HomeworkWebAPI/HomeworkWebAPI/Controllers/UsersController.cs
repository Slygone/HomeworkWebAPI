using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/users
        public ActionResult<List<string>> GetAllUsers()
        {
            return Ok(StaticDb.UserNames);
        }
        [HttpGet("{index}")]//http://localhost:[port]/api/users/1
        public ActionResult<string> GetUserById(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index cannot be negative!");
                }
                if (index >= StaticDb.UserNames.Count)
                {
                    return NotFound($"There is no user on index {index}");
                }
                return Ok(StaticDb.UserNames[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error!");
            }
        }
    }
}
