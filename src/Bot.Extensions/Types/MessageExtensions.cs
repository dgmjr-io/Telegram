/*
 * MessageExtensions.cs
 *
 *   Created: 2022-12-10-04:07:57
 *   Modified: 2022-12-10-04:07:57
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Bot;

using global::Telegram.Bot.Types;
using global::Telegram.Bot.Types.Enums;
using Humanizer;

public static class MessageExtensions
{
    public static string? Truncate(this Message message, int maxLength = 100)
    {
        return message.Type switch
        {
            MessageType.Text => message.Text?.Truncate(maxLength),
            MessageType.Sticker => $"🀄: {message.Sticker.Emoji}",
            MessageType.Video => $"🎥 ({message.Video.Duration}s): {message.Caption?.Truncate(maxLength)}",
            MessageType.Photo => $"📸 ({Convert.ToInt32(message.Photo[0].FileSize).Bytes().Kilobytes:0.00}kB): {message.Caption?.Truncate(maxLength)} ",
            MessageType.Animation => $"🎉: {message.Animation.FileName.Truncate(maxLength)}",
            MessageType.Audio => $"🔊 ({message.Audio.Duration}s): {message.Caption.Truncate(maxLength)}",
            MessageType.Contact
                => $"👤: {message.Contact.PhoneNumber ?? message.Contact.FirstName ?? message.Contact.LastName ?? message.Contact.UserId?.ToString()}",
            MessageType.Document => $"📄 ({Convert.ToInt32(message.Document.FileSize).Bytes().Kilobytes:0.00}kB): {message.Document.FileName.Truncate(maxLength)}",
            MessageType.Location
                => $"📍: {message.Location.Latitude:#.###}, {message.Location.Longitude:#.###}",
            MessageType.Dice => $"🎲: {message.Dice.Emoji}",
            MessageType.Voice => $"🗣 ({message.Audio.Duration}s): {message.Caption.Truncate(maxLength)}",
            _ => message.Caption.Truncate(maxLength)
        };
    }
}
