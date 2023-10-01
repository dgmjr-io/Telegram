/*
 * IBot.cs
 *
 *   Created: 2023-03-19-03:04:18
 *   Modified: 2023-03-23-09:46:53
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Abstractions;

using global::System;
using global::Telegram.Bot.Types;

public interface IBot
{
    long Id { get; set; }
    string Username { get; set; }
    string Name { get; set; }
    // ObjectId SendPulseId { get; set; }
    // BotApiToken ApiToken { get; set; }
}
