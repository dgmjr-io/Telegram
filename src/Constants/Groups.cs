/*
 * Groups.cs
 *
 *   Created: 2022-12-19-07:51:02
 *   Modified: 2022-12-19-07:51:03
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Constants;

public static class GroupsAndChannels
{
    /// <summary>The minimum length of a group title.</summary>
    /// <value>5</value>
    public const int TitleMinLength = 5;

    /// <summary>The maximum length of a group's title.</summary>
    /// <value>128</value>
    public const int TitleMaxLength = 128;

    /// <summary>The minimum length of a group description.</summary>
    /// <value>0</value>
    public const int DescriptionMinLength = 0;

    /// <summary>The maximum length of a group description.</summary>
    /// <value>255</value>
    public const int DescriptionMaxLength = 255;

    /// <summary>The minimum length of a group username.</summary>
    /// <value><inheritdoc cref="Account.UsernameMinLength" /></value>
    public const int UsernameMinLength = Account.UsernameMinLength;

    /// <summary>The maximum length of a group username.</summary>
    /// <value><inheritdoc cref="Account.UsernameMaxLength" /></value>
    public const int UsernameMaxLength = Account.UsernameMaxLength;
}
