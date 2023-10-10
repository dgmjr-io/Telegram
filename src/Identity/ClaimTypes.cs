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

/// <summary>The URI for a claim that specifies the anonymous user.</summary>
/// <value><inheritdoc cref="Tgcb.UriString" path="/value" /></value>
public record class TelegramClaimBase : ClaimType, IClaimType, IIdentityComponent
{
    public static readonly IClaimType Instance = new Tgcb();

    private TelegramClaimBase() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" /></value>
    public const string UriString = "https://telegram.org/identity";

    /// <value>tg:identity</value>
    public const string ShortUriString = "tg:identity";

    /// <value>identity</value>
    public const string Name = "identity";

    /// <value>/</value>
    public const string UriSeparator = "/";

    /// <value>:</value>
    public const string ShortUriSeparator = ":";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IIdentityComponent.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a claim that specifies the anonymous user.</summary>
/// <value><inheritdoc cref="BotApiToken.UriString" path="/value" /></value>
public record class BotApiToken : ClaimType, IClaimType, IIdentityComponent
{
    public static readonly IClaimType Instance = new BotApiToken();

    private BotApiToken() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = "tg:" + Name;

    /// <value>bot_api_token</value>
    public const string Name = "bot_api_token";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IIdentityComponent.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a claim that specifies the anonymous user.</summary>
/// <value><inheritdoc cref="Username.UriString" path="/value" /></value>
public record class Username : ClaimType, IClaimType, IIdentityComponent
{
    public static readonly IClaimType Instance = new Username();

    private Username() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = "tg:" + Name;

    /// <value>username</value>
    public const string Name = "username";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IIdentityComponent.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a claim that specifies the anonymous user.</summary>
/// <value><inheritdoc cref="UserId.UriString" path="/value" /></value>
public record class UserId : ClaimType, IClaimType, IIdentityComponent
{
    public static readonly IClaimType Instance = new UserId();

    private UserId() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = "tg:" + Name;

    /// <value>userid</value>
    public const string Name = "userid";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IIdentityComponent.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}
