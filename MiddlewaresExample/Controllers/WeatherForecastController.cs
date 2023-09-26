using Microsoft.AspNetCore.Mvc;

namespace MiddlewaresExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            //throw new Exception("Test hata");

            return "OK";           
        }

        [HttpGet("User")]
        public User GetUser()
        {
            return new User()
            {
                Id = 1,
                UserName = "Test",
            };                
        }


        [HttpPost("User")]
        public string CreateUser([FromBody] User user)
        {

            return "User created";
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}