using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.UserBot.Store.Abstractions;

namespace Telegram.UserBot.Config
{
    public partial interface IUserBotConfig
    {
        IUserBotStore? GetSessionStore();
        string? GetConfigVariable(string variable);
        string Prompt(string variable);
    }
}
