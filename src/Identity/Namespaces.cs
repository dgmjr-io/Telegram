/*
 * Claims.cs
 *
 *   Created: 2022-12-15-11:25:21
 *   Modified: 2022-12-15-11:25:21
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Identity;

public static partial class StrClaimTypes
{
    public static class Namespaces
    {
        /// <summary>The namespace for Telegram identity.</summary>
        /// <value>identity/</value>
        public const string Identity = "identity/";

        /// <summary>The namespace for Telegram claim value types.</summary>
        /// <value>values/</value>
        public const string Values = "values/";
    }
}
