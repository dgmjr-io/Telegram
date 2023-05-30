/*
 * ClaimType.cs
 *
 *   Created: 2023-03-30-12:25:07
 *   Modified: 2023-03-30-12:25:08
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Identity;

public partial class StrClaimType : Dgmjr.Identity.ClaimType
{
    public virtual bool Equals(StrClaimTypes? other)
    {
        return base.Equals(other);
    }
}
