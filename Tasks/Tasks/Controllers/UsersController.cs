using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase

    {
        IUsersService _UsersService;

        public UsersController(IUsersService UsersService)
        {
            _UsersService = UsersService;

        }
        // GET: api/<UsersController>
        /*     [HttpGet]
             public IEnumerable<string> Get()
             {
                 return new string[] { "value1", "value2" };
             }*/

        // GET api/<UsersController>/5
        [HttpGet]
        public async Task<User> GetById([FromQuery] string userName, [FromQuery] string password)
       {
            return await _UsersService.GetById(userName, password);
        }

       /* // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
