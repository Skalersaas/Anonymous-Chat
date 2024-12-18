using Anonymous_Chat.Handlers;
using Anonymous_Chat.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anonymous_Chat.Controllers
{
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        [Route("data")]
        [HttpGet]
        public ObjectResult GetData()
        {
            List<User> users = [];
            List<(string, string)> pairs = UserManager.activeChats.Select(kvp => (kvp.Key, kvp.Value)).ToList();
            foreach (var userId in UserManager.GetUsers())
            {
                var prof = UserManager.GetProfile(userId);
                var user = new User
                {
                    id = userId,
                    pseudonym = prof.Pseudonym,
                    gender = prof.Gender
                };

                if (UserManager.GetChat(userId).Key != null)
                    user.status = (int)UserStatus.Talking;
                else
                {
                    UserManager.GetWaitingUser(out var waitingId);
                    if (waitingId == userId)
                        user.status = (int)UserStatus.Waiting;
                    else
                        user.status = (int)UserStatus.Idle;
                }
                users.Add(user);
            }
            return  Ok(new
            {
                users = users.Select((user) => new {user.id, user.gender, user.pseudonym, user.status}),
                pairs = pairs.Select((pair) => new {pair.Item1, pair.Item2}),

            });
        }
        enum UserStatus
        {
            Idle,
            Waiting,
            Talking
        }
    }
}
