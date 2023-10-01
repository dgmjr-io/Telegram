// /*
//  * ClaimType.cs
//  *
//  *   Created: 2023-03-25-01:51:33
//  *   Modified: 2023-03-25-01:51:34
//  *
//  *   Author: David G. Moore, Jr. <david@dgmjr.io>
//  *
//  *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
//  *      License: MIT (https://opensource.org/licenses/MIT)
//  */

// namespace Telegram.Identity.Enums;
// using System.ComponentModel.DataAnnotations;
// using Telegram.Identity;

// [GenerateEnumerationClass("ClaimType", "Telegram.Identity")]
// public enum ClaimType
// {
//     /// <inheritdoc cref="StrClaimTypes.BaseUri" />
//     [Display(Name = "Base URI", Description = "The base URI for Telegram")]
//     [Uri(StrClaimTypes.BaseUri)]
//     BaseUri,

//     /// <inheritdoc cref="StrClaimTypes.Identity" />
//     [Display(Name = "Identity", Description = "The base URI for Telegram identity claims")]
//     [Uri(StrClaimTypes.Identity)]
//     Identity,

//     // /// <inheritdoc cref="ClaimTypeNames.UserIdClaim" />
//     // [Display(Name = "Base URI", Description = "The base URI for Telegram")]
//     // [Uri(ClaimTypes.UserIdClaim)]
//     // UserIdClaim,
//     /// <inheritdoc cref="ClaimTypes.Username" />
//     [Display(Name = "Base URI", Description = "The base URI for Telegram")]
//     [Uri(StrClaimTypes.Username)]
//     Username,

//     /// <inheritdoc cref="StrClaimTypes.UserId" />
//     [Display(Name = "Base URI", Description = "The base URI for Telegram")]
//     [Uri(StrClaimTypes.UserId)]
//     UserId,

//     /// <inheritdoc cref="StrClaimTypes.CanJoinGroups" />
//     [Display(Name = "Can join groups", Description = "Whether a bot can join groups")]
//     [Uri(StrClaimTypes.CanJoinGroups)]
//     CanJoinGroups,

//     /// <inheritdoc cref="StrClaimTypes.LanguageCode" />
//     [Display(
//         Name = "Language code",
//         Description = "The language code for the user's preferred language"
//     )]
//     [Uri(StrClaimTypes.LanguageCode)]
//     LanguageCode,

//     /// <inheritdoc cref="StrClaimTypes.BotApiToken" />
//     [Display(
//         Name = "Bot API Token",
//         Description = "The super secret \"key\" that allows this bot to talk to the Telegram API"
//     )]
//     [Uri(StrClaimTypes.BotApiToken)]
//     BotApiToken
// }
