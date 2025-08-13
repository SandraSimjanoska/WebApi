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
        public ActionResult<string> GetUserById(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,"The index has negative value");
                }

                if (index >= StaticDb.Users.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound,$"There is no resourse on index {index}");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDb.Users[index -1]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"An error occurred. Contact The admin");
            }
        }
    }
}
