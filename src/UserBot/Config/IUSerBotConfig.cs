using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.UserBot.Store.Abstractions;

namespace Telegram.UserBot.Config
{
    public partial interface IUserBotConfig
    {
        // GetSessionStore GetSessionStore { get; set; }
        // IUserBotStore? GetSessionStore();
        string? GetConfigVariable(string variable);
        string? Prompt(string variable);
    }

    public delegate IUserBotStore GetSessionStore(IServiceProvider services);
}
