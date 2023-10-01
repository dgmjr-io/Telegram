/*
 * ClaimValueTypes.cs
 *
 *   Created: 2023-09-24-05:51:39
 *   Modified: 2023-09-24-05:51:39
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Identity;

using DgmjrId;

using Tgcb = Telegram.Identity.ClaimTypes.TelegramClaimBase;

public record class BotApiToken : ClaimValueType<Telegram.Bot.Types.BotApiToken>, IIdentityComponent
{
    /// <summary>The name "<inheritdoc cref="Name" path="/value" />"</summary>
    /// <value>bot_api_token</value>
    public const string Name = "bot_api_token";

    /// <summary>The URI string "<inheritdoc cref="UriString" path="/value" />"</summary>
    /// <value><inheritdoc cref="Tgcb.UriString" path="/value" /><inheritdoc cref="Tgcb.UriSeparator" path="/value" /><inheritdoc cref="Name" path="/value" /></value>
    public const string UriString = Tgcb.UriString + Tgcb.UriSeparator + Name;

    /// <summary>The URI string "<inheritdoc cref="ShortUriString" path="/value" />"</summary>
    /// <value><inheritdoc cref="Tgcb.ShortUriString" path="/value" /><inheritdoc cref="Tgcb.ShortUriSeparator" path="/value" /><inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = Tgcb.UriString + Tgcb.UriSeparator + Name;

    /// <summary>The URI string "<inheritdoc cref="UriString" path="/value" />"</summary>
    /// <value><inheritdoc cref="Tgcb.UriString" path="/value" /><inheritdoc cref="Tgcb.UriSeparator" path="/value" /><inheritdoc cref="Name" path="/value" /></value>
    public static readonly new uri Uri = new(UriString);

    /// <summary>The URI string "<inheritdoc cref="ShortUriString" path="/value" />"</summary>
    /// <value><inheritdoc cref="Tgcb.ShortUriString" path="/value" /><inheritdoc cref="Tgcb.ShortUriSeparator" path="/value" /><inheritdoc cref="Name" path="/value" /></value>
    public static readonly new uri ShortUri = new(ShortUriString);

    /// <summary>The URI string "<inheritdoc cref="UriString" path="/value" />"</summary>
    /// <value><inheritdoc cref="Tgcb.UriString" path="/value" /><inheritdoc cref="Tgcb.UriSeparator" path="/value" /><inheritdoc cref="Name" path="/value" /></value>
    uri IIdentityComponent.Uri => Uri;

    /// <summary>The URI string "<inheritdoc cref="ShortUriString" path="/value" />"</summary>
    /// <value><inheritdoc cref="Tgcb.ShortUriString" path="/value" /><inheritdoc cref="Tgcb.ShortUriSeparator" path="/value" /><inheritdoc cref="Name" path="/value" /></value>
    uri IIdentityComponent.ShortUri => ShortUri;
}
