/*
 * BotClaim.cs
 *
 *   Created: 2023-03-25-04:18:46
 *   Modified: 2023-03-25-04:18:46
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Models;

using System.ComponentModel.DataAnnotations;
using Dgmjr.Identity.Abstractions;
using Telegram.Abstractions;

public class BotClaim : IEntityClaim<BotClaim>
{
    public int Id { get; set; }
    public int EntityId { get; set; }
    public string? ClaimValue { get; set; }
    public string? ClaimType { get; set; }
    public uri? Type { get; set; }
    public uri? ValueType { get; set; }
    public uri? Issuer { get; set; }
    public uri? OriginalIssuer { get; set; }
    public IStringDictionary Properties { get; set; } = new StringDictionary();

    public void InitializeFromClaim(C claim);

    public C ToClaim() =>
        new Claim(ClaimType, ClaimValue, ValueType, Issuer, OriginalIssuer, Properties);
}
