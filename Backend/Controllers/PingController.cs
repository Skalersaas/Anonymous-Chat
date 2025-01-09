using Microsoft.AspNetCore.Mvc;

namespace Anonymous_Chat.Controllers
{
    [ApiController]
    [Route("")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ObjectResult Ping()
        {
            return Ok("Working");
        }
    }
}
