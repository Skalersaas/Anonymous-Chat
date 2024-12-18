using Microsoft.AspNetCore.Mvc;
using Anonymous_Chat.Handlers;

namespace Anonymous_Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChattingController : ControllerBase
    {
        [Route("connect")]
        public async Task Connect()
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await ConnectionHandler.HandleConnectionAsync(webSocket);
        }
    }
}