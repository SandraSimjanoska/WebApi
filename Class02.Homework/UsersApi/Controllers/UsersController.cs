using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllUsers()
        {
            return Ok(StaticDb.Users);
        }

        [HttpGet("{index}")]
        public ActionResult<string> GetUserByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index cannot be negative.");
                }

                if (index >= StaticDb.Users.Count)
                {
                    return NotFound($"There is no resource at index {index}");
                }

                return Ok(StaticDb.Users[index]);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the admin.");
            }
        }
    }
}
