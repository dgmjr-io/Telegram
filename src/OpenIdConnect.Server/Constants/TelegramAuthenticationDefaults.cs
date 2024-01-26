/*
 * TelegramAuthenticationDefaults.cs
 *
 *   Created: 2024-42-20T05:42:10-05:00
 *   Modified: 2024-42-20T05:42:10-05:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2024 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.OpenIdConnect.Constants;

/// <summary>
///     Options used when application is not configured by the developer
/// </summary>
public static class TelegramAuthenticationDefaults
{
    /// <summary>
    ///     Default value for the identity provider.
    /// </summary>
    public const string IdentityProvider = "Telegram OIDC Connect";

    /// <summary>
    ///     Default value for <see cref="Microsoft.AspNetCore.Authentication.AuthenticationScheme.Name" />.
    /// </summary>
    public const string Name = "Telegram";

    /// <summary>
    ///     Default value for <see cref="Microsoft.AspNetCore.Authentication.AuthenticationScheme.DisplayName" />.
    /// </summary>
    public const string DisplayName = "Telegram";

    /// <summary>
    ///     Default value for <see cref="Microsoft.AspNetCore.Authentication.RemoteAuthenticationOptions.ClaimsIssuer" />.
    /// </summary>
    public const string ClaimsIssuer = "https://oidc.telegram.technology";

    /// <summary>
    ///     Default value for <see cref="TelegramAuthenticationOptions.AuthorizationEndpoint" />.
    /// </summary>
    public const string AuthorizationEndpoint = "/Identity/Account/TelegramLogin";

    /// <summary>
    ///     Default value for <see cref="TelegramAuthenticationOptions.CallbackPath" />.
    /// </summary>
    public const string CallbackPath = "/signin-telegram";

    /// <summary>
    ///     Default Origin form field name
    /// </summary>
    public const string OriginFieldName = "__tgOrigin";

    /// <summary>
    ///     Default User Data form field name
    /// </summary>
    public const string UserDataFieldName = "__tgUserData";
}
