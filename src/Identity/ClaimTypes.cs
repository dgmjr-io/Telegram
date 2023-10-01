/*
 * ClaimTypes.cs
 *
 *   Created: 2023-09-24-02:35:54
 *   Modified: 2023-09-24-02:35:54
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Identity.ClaimTypes;

using Dgmjr.Identity;
using Dgmjr.Identity.ClaimTypes;

using Tgcb = TelegramClaimBase;

public record class TelegramClaimBase : ClaimType, IIdentityComponent
{
    /// <value>https://telegram.org/identity</value>
    public const string UriString = "https://telegram.org/identity";

    /// <value>tg</value>
    public const string ShortUriString = "tg";

    /// <value>/</value>
    public const string UriSeparator = "/";

    /// <value>:</value>
    public const string ShortUriSeparator = ":";
}

public record class BotApiToken : ClaimType, IIdentityComponent
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

public record class Username : ClaimType, IIdentityComponent
{
    /// <summary>The name "<inheritdoc cref="Name" path="/value" />"</summary>
    /// <value>username</value>
    public const string Name = "username";

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

public record class UserId : ClaimType, IIdentityComponent
{
    /// <summary>The name "<inheritdoc cref="Name" path="/value" />"</summary>
    /// <value>userid</value>
    public const string Name = "userid";

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
