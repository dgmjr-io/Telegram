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
    public new const string UriString = "https://telegram.org/identity";

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
    public new const string UriString = TelegramIdentityBaseUri;

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

/// <summary>The URI for a bot token claim.</summary>
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

/// <summary>The URI for a username claim.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class Username : ClaimType, IClaimType<DgmjrCvt.String>
{
    public static readonly IClaimType Instance = new Username();

    public override uri? ClaimValueTypeUri => DgmjrCvt.String.UriString;

    private Username() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public new const string UriString = TelegramIdentityBaseUri + "/" + Name;

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

/// <summary>The URI for a user ID claim.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class UserId : ClaimType, IClaimType<DgmjrCvt.Integer64>
{
    public static readonly IClaimType Instance = new UserId();

    public override uri? ClaimValueTypeUri => DgmjrCvt.Integer64.UriString;

    private UserId() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public new const string UriString = TelegramIdentityBaseUri + "/" + Name;

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

/// <summary>The URI for a first name claim.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class FirstName : ClaimType, IClaimType<DgmjrCvt.String>
{
    public static readonly IClaimType Instance = new FirstName();

    public override uri? ClaimValueTypeUri => DgmjrCvt.Integer64.UriString;

    private FirstName() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public new const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = "tg:" + Name;

    /// <value>given_name</value>
    public const string Name = "given_name";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a last name claim.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class LastName : ClaimType, IClaimType<DgmjrCvt.String>
{
    public static readonly IClaimType Instance = new LastName();

    public override uri? ClaimValueTypeUri => DgmjrCvt.Integer64.UriString;

    private LastName() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public new const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = "tg:" + Name;

    /// <value>surname</value>
    public const string Name = "surname";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a user's photo claim.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class PhotoUrl : ClaimType, IClaimType<DgmjrCvt.AnyUri>
{
    public static readonly IClaimType Instance = new PhotoUrl();

    public override uri? ClaimValueTypeUri => DgmjrCvt.Integer64.UriString;

    private PhotoUrl() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public new const string UriString = TelegramIdentityBaseUri + "/" + Name;

    /// <value>tg:<inheritdoc cref="Name" path="/value" /></value>
    public const string ShortUriString = "tg:" + Name;

    /// <value>photo_url</value>
    public const string Name = "photo_url";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a user's photo claim.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class UserUri : ClaimType, IClaimType<DgmjrCvt.AnyUri>
{
    /// <value>tg://user?id={0}</value>
    public const string FormatString = "tg://user?id={0}";

    public static uri Create(long id) => Format(FormatString, id);

    public static readonly IClaimType Instance = new UserUri();

    public override uri? ClaimValueTypeUri => DgmjrCvt.Integer64.UriString;

    private UserUri() { }

    /// <value>tg://<inheritdoc cref="Name" path="/value" /></value>
    public new const string UriString = "tg://" + Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    public const string ShortUriString = UriString;

    /// <value>user</value>
    public const string Name = "user";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}

/// <summary>The URI for a user's preferred language.</summary>
/// <value><inheritdoc cref="UriString" path="/value" /></value>
public record class Language : ClaimType, IClaimType<DgmjrCvt.AnyUri>
{
    public static readonly IClaimType Instance = new Language();

    public override uri? ClaimValueTypeUri => DgmjrCvt.Integer64.UriString;

    private Language() { }

    /// <value><inheritdoc cref="TelegramIdentityBaseUri" path="/value" />/<inheritdoc cref="Name" path="/value" /></value>
    public new const string UriString =  TelegramIdentityBaseUri + "/" + Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    public const string ShortUriString = UriString;

    /// <value>language</value>
    public const string Name = "language";

    /// <value><inheritdoc cref="Name" path="/value" /></value>
    string IHaveAName.Name => Name;

    /// <value><inheritdoc cref="UriString" path="/value" /></value>
    string IHaveAUriString.UriString => UriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    string IIdentityComponent.ShortUriString => ShortUriString;

    /// <value><inheritdoc cref="ShortUriString" /></value>
    public override uri ShortUri => ShortUriString;
}
