using System.Security.Claims;

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

using Tgcb = TelegramClaimType;
using static TelegramClaimType;

/// <summary>The base claim type for Telegram claims.</summary>
/// <value><inheritdoc cref="Tgcb.UriString" path="/value" /></value>
public abstract record class TelegramClaimType : DgmjrId.ClaimType, IClaimType
{
    /// <value>https://telegram.org/identity</value>
    public const string TelegramIdentityBaseUri = "https://telegram.org/identity";

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" /></value>
    public const string UriString = "https://telegram.org/identity";

    /// <value>/</value>
    public const string UriSeparator = "/";

    /// <value>:</value>
    public const string ShortUriSeparator = ":";

    /// <value>tg:identity</value>
    public const string ShortUriString = "tg:identity";
}

/// <summary>The base claim type for Telegram claims.</summary>
/// <value><inheritdoc cref="TelegramClaimType{T}.UriString" path="/value" /></value>
public abstract record class TelegramClaimType<TValueType>
    : TelegramClaimType,
        IClaimType<TValueType>
    where TValueType : IClaimValueType, IEquatable<TValueType>
{
    protected TelegramClaimType() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" /></value>
    public const string UriString = TelegramIdentityBaseUri;

    /// <value>tg:identity</value>
    public const string ShortUriString = "tg:identity";

    /// <value>identity</value>
    public const string Name = "identity";

    /// <value>/</value>
    public const string UriSeparator = "/";

    /// <value>:</value>
    public const string ShortUriSeparator = ":";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a claim that specifies the anonymous user.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class BotApiToken : TelegramClaimType, IClaimType<DgmjrCvt.String>
{
    public static readonly IClaimType Instance = new BotApiToken();

    public override uri? ClaimValueTypeUri => Tgcb.UriString + UriSeparator + Name;

    private BotApiToken() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public new const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public new const string ShortUriString = "tg:" + Name;

    /// <value>bot_api_token</value>
    public new const string Name = "bot_api_token";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a claim that specifies the anonymous user.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class Username : ClaimType, IClaimType<DgmjrCvt.String>
{
    public static readonly IClaimType Instance = new Username();

    public override uri? ClaimValueTypeUri => DgmjrCvt.String.UriString;

    private Username() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = "tg:" + Name;

    /// <value>username</value>
    public const string Name = "username";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a claim that specifies the anonymous user.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class UserId : ClaimType, IClaimType<DgmjrCvt.Integer64>
{
    public static readonly IClaimType Instance = new UserId();

    public override uri? ClaimValueTypeUri => DgmjrCvt.Integer64.UriString;

    private UserId() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = "tg:" + Name;

    /// <value>userid</value>
    public const string Name = "userid";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}
