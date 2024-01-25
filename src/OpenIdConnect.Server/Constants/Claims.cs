/*
 * TelegramAuthenticationConstants.cs
 *
 *   Created: 2024-36-20T05:36:35-05:00
 *   Modified: 2024-36-20T05:36:36-05:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2024 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.OpenIdConnect.Constants;

/// <summary>
///     Telegram provided data.
/// </summary>
public static class Claims
{
    /// <summary>
    ///     Telegram user's first name.
    /// </summary>
    public const string FirstName = Telegram.Identity.ClaimTypes.FirstName.UriString;

    /// <summary>
    ///     Optional Telegram user's last name.
    /// </summary>
    public const string LastName = Telegram.Identity.ClaimTypes.LastName.UriString;

    /// <summary>
    ///     Optional Telegram user's user name.
    /// </summary>
    public const string Username = Telegram.Identity.ClaimTypes.Username.UriString;

    /// <summary>
    ///     Optional Telegram user's photo.
    /// </summary>
    public const string PhotoUrl = Telegram.Identity.ClaimTypes.PhotoUrl.UriString;

    /// <summary>
    ///     The Telegram user's URI string
    /// </summary>
    public const string Uri = Telegram.Identity.ClaimTypes.UserUri.UriString;

    /// <summary>
    ///     The Telegram user's unique ID.
    /// </summary>
    public const string UserId = Telegram.Identity.ClaimTypes.UserId.UriString;
}
