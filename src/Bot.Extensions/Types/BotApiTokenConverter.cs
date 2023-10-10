/*
 * BotApiTokenConverter.cs
 *
 *   Created: 2022-12-05-06:14:50
 *   Modified: 2022-12-05-06:15:18
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Bot.Types;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class BotApiTokenConverter : ValueConverter<BotApiToken, string>
{
    public BotApiTokenConverter()
        : base(v => v.Value, v => BotApiToken.From(v)) { }
}
