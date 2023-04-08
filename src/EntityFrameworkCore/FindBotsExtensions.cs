/*
 * FindBotsExtensions.cs
 *
 *   Created: 2022-12-13-07:58:43
 *   Modified: 2022-12-13-07:58:43
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

using System.Linq;
using Dgmjr.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace Telegram.Bots;

public static class FindBotsExtensions
{
    public static async Task<Bot?> FindBotAsync(this IQueryable<Bot> bots, object identifier)
    {
        return identifier is null
            ? throw new ArgumentNullException(nameof(identifier))
            : identifier is int id
            ? await bots.FirstOrDefaultAsync(b => b.Id == id)
            : identifier is string username
            ? await bots.FirstOrDefaultAsync(b => b.TelegramUsername == username)
            : identifier is ObjectId sendPulseId
            ? await bots.FirstOrDefaultAsync(b => b.SendPulseId == sendPulseId)
            : identifier is BotApiToken apiToken
            ? await bots.FirstOrDefaultAsync(b => b.ApiToken == apiToken)
            : identifier is string @string && ObjectId.TryParse(@string, out sendPulseId)
            ? await bots.FirstOrDefaultAsync(b => b.SendPulseId == sendPulseId)
            : identifier is string @string2 && BotApiToken.TryParse(@string2, out var apiToken2)
            ? await bots.FirstOrDefaultAsync(b => b.ApiToken == apiToken2)
            : throw new ArgumentException($"Cannot find bot with identifier of type {identifier.GetType().Name}", nameof(identifier));
    }
}
