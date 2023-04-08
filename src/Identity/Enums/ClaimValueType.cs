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

[GenerateEnumerationClass("ClaimValueType", "Telegram.Identity")]
public enum ClaimValueType
{
    /// <summmary>The base type for the Telegram claim value type - <inheritdoc cref="ClaimTypeNames.Identity" path="/value" /></summary>
    /// <value><inheritdoc cref="ClaimTypeNames.Identity" path="/value" /></value>
    [Display(Name = "Base URI", Description = "The base URI for Telegram claim value types")]
    [Uri(ClaimTypeNames.Identity)]
    BaseUri,


    /// <inheritdoc cref="ClaimTypeNames.BotApiToken" />
    [Display(Name = "Bot API Token", ShortName = nameof(BotApiToken), Description = "The super secret \"key\" that allows this bot to talk to the Telegram API")]
    [Uri(ClaimTypeNames.BotApiToken)]
    BotApiToken
}
