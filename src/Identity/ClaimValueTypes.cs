// /*
//  * ClaimValueTypes.cs
//  *
//  *   Created: 2023-09-24-05:51:39
//  *   Modified: 2023-09-24-05:51:39
//  *
//  *   Author: David G. Moore, Jr. <david@dgmjr.io>
//  *
//  *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
//  *      License: MIT (https://opensource.org/licenses/MIT)
//  */

// namespace Telegram.Identity.ClaimValueTypes;

// using Dgmjr.Abstractions;

// using DgmjrId;

// using Telegram.Bot.Types;

// using Tgcb = TelegramClaimType;

// public record class BotApiToken
//     : DgmjrId.ClaimValueType<Telegram.Bot.Types.BotApiToken>,
//         IClaimValueType<Telegram.Bot.Types.BotApiToken>
// {
//     /// <summary>The name "<inheritdoc cref="Name" path="/value" />"</summary>
//     /// <value>bot_api_token</value>
//     public const string Name = "bot_api_token";

//     /// <summary>The URI string "<inheritdoc cref="UriString" path="/value" />"</summary>
//     /// <value><inheritdoc cref="Tgcb.UriString" path="/value" /><inheritdoc cref="UriSeparator" path="/value" /><inheritdoc cref="Name" path="/value" /></value>
//     public new const string UriString = Tgcb.UriString + UriSeparator + Name;

//     /// <summary>The URI string "<inheritdoc cref="ShortUriString" path="/value" />"</summary>
//     /// <value><inheritdoc cref="Tgcb.ShortUriString" path="/value" /><inheritdoc cref="ShortUriSeparator" path="/value" /><inheritdoc cref="Name" path="/value" /></value>
//     public const string ShortUriString = Tgcb.UriString + UriSeparator + Name;

//     /// <summary>The URI string "<inheritdoc cref="UriString" path="/value" />"</summary>
//     /// <value><inheritdoc cref="UriString" path="/value" /></value>
//     public static readonly new uri Uri = new(UriString);

//     /// <summary>The URI string "<inheritdoc cref="ShortUriString" path="/value" />"</summary>
//     /// <value><inheritdoc cref="ShortUriString" path="/value" /></value>
//     public static new readonly uri ShortUri = new(ShortUriString);

//     /// <summary>The URI string "<inheritdoc cref="UriString" path="/value" />"</summary>
//     /// <value><inheritdoc cref="UriString" path="/value" /></value>
//     uri IHaveAuri.Uri => Uri;

//     /// <summary>The URI string "<inheritdoc cref="ShortUriString" path="/value" />"</summary>
//     /// <value><inheritdoc cref="ShortUriString" path="/value" /></value>
//     uri IIdentityComponent.ShortUri => ShortUri;

//     Bot.Types.BotApiToken IClaimValueType<Bot.Types.BotApiToken>.Value =>
//         (Bot.Types.BotApiToken)Value;

//     Bot.Types.BotApiToken IHaveAValue<Bot.Types.BotApiToken>.Value => (Bot.Types.BotApiToken)Value;
// }
