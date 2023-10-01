/*
 * Bot.cs
 *
 *   Created: 2023-03-19-02:05:09
 *   Modified: 2023-05-29-07:03:56
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Models;

using System.Linq;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using global::Telegram.Bot.Types;
using Telegram.Abstractions;
using Telegram.Constants.DbConstants;
using Dgmjr.Identity.Models;
using static Telegram.Constants.DbConstants.SchemaNames;

[Table(Dgmjr.Identity.Constants.Tables.User, Schema = TeleSchema)]
[JSerializable(typeof(Bot))]
public class Bot : Dgmjr.Identity.Models.User { }

public class MyBot : Bot
{
    public virtual BotApiToken ApiToken { get; set; }
    public virtual UserClaim BotApiTokenClaim
    {
        get => this.Claims.FirstOrDefault(c->c.Type == BotClaimTypes.BotApiToken);
    }
}

public class SendPulseBot : MyBot
{
    public virtual ObjectId SendPulseId { get; set; }
    public virtual UserClaim SendPulseIdClaim
    {
        get =>
            this.Claims.FirstOrDefault(c => c.Type == SendPulse.Identity.ClaimTypeNames.ObjectId);
        set
        {
            if (this.SendPulseIdClaim != null)
            {
                this.Claims.Remove(this.SendPulseIdClaim);
            }
            this.Claims.Add(value);
        }
    }
}

public record struct BotDto : IBot
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    // public ObjectId SendPulseId { get; set; }
    // public BotApiToken ApiToken { get; set; }
}
