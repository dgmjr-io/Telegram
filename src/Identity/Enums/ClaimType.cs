/* 
 * ClaimType.cs
 * 
 *   Created: 2023-03-25-01:51:33
 *   Modified: 2023-03-25-01:51:34
 * 
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *   
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Identity.Enums;
using System.ComponentModel.DataAnnotations;

[GenerateEnumerationClass("ClaimType", "Telegram.Identity")]
public enum ClaimType
{
    /// <inheritdoc cref="ClaimTypeNames.BaseUri" />
    [Display(Name = "Base URI", Description = "The base URI for Telegram")]
    [Uri(ClaimTypeNames.BaseUri)]
    BaseUri,
    /// <inheritdoc cref="ClaimTypeNames.Identity" />
    [Display(Name = "Identity", Description = "The base URI for Telegram identity claims")]
    [Uri(ClaimTypeNames.Identity)]
    Identity,
    // /// <inheritdoc cref="ClaimTypeNames.UserIdClaim" />
    // [Display(Name = "Base URI", Description = "The base URI for Telegram")]
    // [Uri(ClaimTypes.UserIdClaim)]
    // UserIdClaim,
    /// <inheritdoc cref="ClaimTypeNames.Username" />
    [Display(Name = "Base URI", Description = "The base URI for Telegram")]
    [Uri(ClaimTypeNames.Username)]
    Username,
    /// <inheritdoc cref="ClaimTypeNames.UserId" />
    [Display(Name = "Base URI", Description = "The base URI for Telegram")]
    [Uri(ClaimTypeNames.UserId)]
    UserId,
    /// <inheritdoc cref="ClaimTypeNames.CanJoinGroups" />
    [Display(Name = "Can join groups", Description = "Whether a bot can join groups")]
    [Uri(ClaimTypeNames.CanJoinGroups)]
    CanJoinGroups,
    /// <inheritdoc cref="ClaimTypeNames.LanguageCode" />
    [Display(Name = "Language code", Description = "The language code for the user's preferred language")]
    [Uri(ClaimTypeNames.LanguageCode)]
    LanguageCode,

    /// <inheritdoc cref="ClaimTypeNames.BotApiToken" />
    [Display(Name = "Bot API Token", Description = "The super secret \"key\" that allows this bot to talk to the Telegram API")]
    [Uri(ClaimTypeNames.BotApiToken)]
    BotApiToken
}
