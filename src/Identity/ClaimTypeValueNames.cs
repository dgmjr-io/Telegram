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

public static partial class ClaimTypeValueUris
{
    /// <summary>The base URI for Telegram values- <inheritdoc cref="BaseUri" path="/value" /></summary>
    /// <value><inheritdoc cref="ClaimTypeUris.Identity" path="/value" />value/</value>
    public const string BaseUri = ClaimTypeUris.Identity + ClaimTypeUris.Namespaces.Values;

    /// <summary>The URI for the Telegram <inheritdoc cref="ClaimTypeUris.UriFragments.LanguageCode" path="/value" /> claim value type.</summary>
    /// <value><inheritdoc cref="ClaimTypeUris.LanguageCode"  path="/value" /></value>
    public const string LanguageCode = BaseUri + ClaimTypeUris.UriFragments.LanguageCode;
    /// <summary>The URI for the Telegram <inheritdoc cref="ClaimTypeUris.UriFragments.BotApiToken" path="/value" /> claim value type.</summary>
    /// <value><inheritdoc cref="BaseUri" path="/value" /><inheritdoc cref="ClaimTypeUris.UriFragments.BotApiToken" path="/value" /></value>
    public const string BotApiToken = BaseUri + ClaimTypeUris.UriFragments.BotApiToken;
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
