/*
 * Usernames.cs
 *
 *   Created: 2022-12-19-07:47:28
 *   Modified: 2022-12-19-07:47:29
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

using System.Reflection;

namespace Telegram.Constants;

public static class Account
{
    /// <summary>A regular expression to validate Telegram usernames</summary>
    /// <value>^@[a-zA-Z0-9]{<inheritdoc cref="UsernameMinLength" path="/value" />,<inheritdoc cref="UsernameMaxLength" path="/value" />}$</value>
    public const string UsernameRegex = "^@[a-zA-Z0-9]{5,32}$";

    /// <summary>A regular expression to validate Telegram usernames (without the leading "@")</summary>
    /// <value>^[a-zA-Z0-9]{<inheritdoc cref="UsernameMinLength" path="/value" />,<inheritdoc cref="UsernameMaxLength" path="/value" />}$</value>
    public const string UsernameRegexNoLeadingAt = "^[a-zA-Z0-9]{5,32}$";

    /// <summary>The minimum length of a username.</summary>
    /// <value>5</value>
    public const int UsernameMinLength = 5;

    /// <summary>The maximum length of a username.</summary>
    /// <value>32</value>
    public const int UsernameMaxLength = 32;

    /// <summary>The minimum length of a given name.</summary>
    /// <value>0</value>
    public const int GivenNameMinLength = 0;

    /// <summary>The maximum length of a given name.</summary>
    /// <value>64</value>
    public const int GivenNameMaxLength = 64;

    /// <summary>The minimum length of a surname.</summary>
    /// <value><inheritdoc cref="GivenNameMinLength" /></value>
    public const int SurnameMinLength = 0;

    /// <summary>The maximum length of a surname.</summary>
    /// <value><inheritdoc cref="GivenNameMaxLength" /></value>
    public const int SurnameMaxLength = GivenNameMaxLength;
}
