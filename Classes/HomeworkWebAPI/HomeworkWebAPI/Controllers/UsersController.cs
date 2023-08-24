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
        [HttpGet("{user}")]//http://localhost:[port]/api/users/1
        public ActionResult<string> GetUserByUsername(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Invalid User!");
                }
                
                var user = StaticDb.UserNames.FirstOrDefault(x => x.Equals(username));
                
                if(user == null)
                {
                    return NotFound("There is no such User!");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error!");
            }
        }
    }
}
