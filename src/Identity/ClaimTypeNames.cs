/*
 * Claims.cs
 *
 *   Created: 2023-03-19-02:53:23
 *   Modified: 2023-03-23-11:30:12
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Identity;

public static partial class StrClaimTypes
{
    /// <summary>The base URI for Telegram - <inheritdoc cref="BaseUri" path="/value" /></summary>
    /// <value>https://telegram.org/</value>
    public const string BaseUri = "https://telegram.org/";

    /// <summary>The base URI for Telegram identity - <inheritdoc cref="global::Telegram.Identity.StrClaimTypes.Identity" path="/value" /></summary>
    /// <value><inheritdoc cref="BaseUri" path="/value" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.Namespaces.Identity" path="/value" /></value>
    public const string Identity =
        BaseUri + global::Telegram.Identity.StrClaimTypes.Namespaces.Identity;

    // /// <summary>The URI for the Telegram user ID claim type.</summary>
    // /// <value><inheritdoc cref="Identity" /><inheritdoc cref="UriFragments.UserId" /></value>
    // public const string UserIdClaim = Identity + UriFragments.UserId;
    /// <summary>The URI for the Telegram <inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.Username" path="/value" /> claim type.</summary>
    /// <value><inheritdoc cref="Identity" path="/value" /><inheritdoc cref="StrClaimTypes.UriFragments.Username" path="/value" /></value>
    public const string Username =
        Identity + global::Telegram.Identity.StrClaimTypes.UriFragments.Username;

    /// <summary>The URI for the Telegram <inheritdoc cref="Identity" path="/value" /><inheritdoc cref=" UriFragments.UserId" path="/value" /> claim type.</summary>
    /// <value><inheritdoc cref="Identity" /><inheritdoc cref=" UriFragments.UserId" path="/value" /></value>
    public const string UserId =
        Identity + global::Telegram.Identity.StrClaimTypes.UriFragments.UserId;

    /// <summary>The URI for the Telegram <inheritdoc cref="Identity" path="/value" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.CanJoinGroups" path="/value" /> claim type.</summary>
    /// <value><inheritdoc cref="Identity" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.CanJoinGroups" path="/value" /></value>
    public const string CanJoinGroups =
        Identity + global::Telegram.Identity.StrClaimTypes.UriFragments.CanJoinGroups;

    /// <summary>The URI for the Telegram <inheritdoc cref="Identity" path="/value" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.IsBot" path="/value" /> claim type.</summary>
    /// <value><inheritdoc cref="Identity" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.IsBot" path="/value" /></value>
    public const string IsBot =
        Identity + global::Telegram.Identity.StrClaimTypes.StrClaimTypes.UriFragments.IsBot;

    /// <summary>The URI for the Telegram <inheritdoc cref="Identity" path="/value" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.LanguageCode" path="/value" /> claim type.</summary>
    /// <value><inheritdoc cref="Identity" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.LanguageCode"  path="/value" /></value>
    public const string LanguageCode =
        Identity + global::Telegram.Identity.trClaimTypes.UriFragments.LanguageCode;

    /// <summary>The URI for the Telegram <inheritdoc cref="Identity" path="/value" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.BotApiToken" path="/value" /> claim type.</summary>
    /// <value><inheritdoc cref="Identity" /><inheritdoc cref="global::Telegram.Identity.StrClaimTypes.UriFragments.BotApiToken" path="/value" /></value>
    public const string BotApiToken =
        Identity + global::Telegram.Identity.StrClaimTypes.UriFragments.BotApiToken;
}

// [GenerateEnumerationRecordStruct("ClaimType")]
// public enum ClaimTypeEnum
// {
//     /// <inheritdoc cref="ClaimTypeNames.BaseUri" />
//     [Display(Name = "Base URI", Description = "The base URI for Telegram", ShortName = ClaimTypeNames.BaseUri)]
//     [Uri(ClaimTypeNames.BaseUri)]
//     BaseUri,
//     /// <inheritdoc cref="ClaimTypeNames.Identity" />
//     [Display(Name = "Identity", Description = "The base URI for Telegram identity claims", ShortName = ClaimTypeNames.Identity)]
//     [Uri(ClaimTypeNames.Identity)]
//     Identity,
//     // /// <inheritdoc cref="ClaimTypeNames.UserIdClaim" />
//     // [Display(Name = "Base URI", Description = "The base URI for Telegram")]
//     // [Uri(ClaimTypes.UserIdClaim)]
//     // UserIdClaim,
//     /// <inheritdoc cref="ClaimTypeNames.Username" />
//     [Display(Name = "Base URI", Description = "The base URI for Telegram", ShortName = ClaimTypeNames.Username)]
//     [Uri(ClaimTypeNames.Username)]
//     Username,
//     /// <inheritdoc cref="ClaimTypeNames.UserId" />
//     [Display(Name = "Base URI", Description = "The base URI for Telegram", ShortName = ClaimTypeNames.UserId)]
//     [Uri(ClaimTypeNames.UserId)]
//     UserId,
//     /// <inheritdoc cref="ClaimTypeNames.CanJoinGroups" />
//     [Display(Name = "Can join groups", Description = "Whether a bot can join groups", ShortName = ClaimTypeNames.CanJoinGroups)]
//     [Uri(ClaimTypeNames.CanJoinGroups)]
//     CanJoinGroups,
//     /// <inheritdoc cref="ClaimTypeNames.LanguageCode" />
//     [Display(Name = "Language code", Description = "The language code for the user's preferred language", ShortName = ClaimTypeNames.LanguageCode)]
//     [Uri(ClaimTypeNames.LanguageCode)]
//     LanguageCode,
//     /// <inheritdoc cref="ClaimTypeNames.BotApiToken" />
//     [Display(Name = "Bot API Token", Description = "The unique and secret key for controlling this bot via the Telegram bot API", ShortName = ClaimTypeNames.BotApiToken)]
//     [Uri(ClaimTypeNames.BotApiToken)]
//     BotApiToken
// }
