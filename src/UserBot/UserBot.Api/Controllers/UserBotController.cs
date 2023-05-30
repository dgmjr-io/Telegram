namespace Telegram.UserBot.Api;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TL;

[Route("api/users")]
public class UserBotUsersController : Dgmjr.AspNetCore.Controllers.ApiControllerBase
{
    private readonly UserBot _bot;

    public UserBotUsersController(ILogger<UserBotUsersController> logger, UserBot bot) : base(logger)
    {
        _bot = bot;
    }

    [HttpGet("{userId:long}")]
    public virtual ResponsePayload<User> GetById([FromRoute] long userId)
    {
        return _bot.Users.TryGetValue(userId, out var user) ? new ResponsePayload<User>(user) : ResponsePayload<User>.NotFound();
    }

    [HttpGet("@{username}")]
    public virtual ResponsePayload<User> GetUserByUsername([FromRoute] string username)
    {
        var user = _bot.Users.FirstOrDefault(u => u.Value.username?.Equals(username, OrdinalIgnoreCase) ?? false).Value;
        if(user != null)
        {
            return new ResponsePayload<User>(user);
        }
        return ResponsePayload<User>.NotFound();
    }
}
