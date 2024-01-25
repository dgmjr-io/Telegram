/*
 * TelegramPostConfigureOptions.cs
 *
 *   Created: 2024-53-20T05:53:02-05:00
 *   Modified: 2024-53-20T05:53:02-05:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2024 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

/// <summary>
///     Configure defaults for <see cref="TelegramAuthenticationOptions" />
/// </summary>
/// <remarks>
///     Initializes <see cref="TelegramAuthenticationPostConfigureOptions" />.
/// </remarks>
/// <param name="dataProtection">The <see cref="IDataProtectionProvider" />.</param>
public class TelegramAuthenticationPostConfigureOptions(IDataProtectionProvider dataProtection)
    : IPostConfigureOptions<TelegramAuthenticationOptions>
{
    private readonly IDataProtectionProvider _dataProtection = dataProtection;

    /// <inheritdoc />
    public void PostConfigure(string? name, TelegramAuthenticationOptions options)
    {
        options.DataProtectionProvider ??= _dataProtection;

        if (options.DataProtectionProvider == null)
        {
            throw new ArgumentNullException(
                $"{nameof(options)}.{nameof(options.DataProtectionProvider)}",
                "Data Protection Provider not configured"
            );
        }

        if (options.StateDataFormat != null)
            return;

        var dataProtector = options.DataProtectionProvider.CreateProtector(
            typeof(TelegramAuthenticationHandler).FullName!,
            name
        );
        options.StateDataFormat = new PropertiesDataFormat(dataProtector);
    }
}
