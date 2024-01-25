/*
 * TelegramAuthenticationBuilder.cs
 *
 *   Created: 2024-35-20T05:35:55-05:00
 *   Modified: 2024-35-20T05:35:55-05:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2024 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

using Telegram.AspNetCore.Authentication.Constants;

namespace Telegram.AspNetCore.Authentication;

public static class TelegramAuthenticationBuilder
{
    /// <summary>
    ///     Adds Telegram Authentication to .NET Identity.
    /// /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="configureOptions">Delegate to configure <see cref="TelegramAuthenticationOptions" />.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddTelegram(
        this AuthenticationBuilder builder,
        Action<TelegramAuthenticationOptions> configureOptions
    )
    {
        builder.Services.AddSingleton<
            IPostConfigureOptions<TelegramAuthenticationOptions>,
            TelegramAuthenticationPostConfigureOptions
        >();

        builder.AddScheme<TelegramAuthenticationOptions, TelegramAuthenticationHandler>(
            TelegramAuthenticationDefaults.Name,
            TelegramAuthenticationDefaults.DisplayName,
            configureOptions
        );

        return builder;
    }
}
