/*
 * IUserBot.cs
 *
 *   Created: 2023-04-30-07:39:48
 *   Modified: 2023-04-30-07:39:48
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.UserBot;

public partial interface IUserBot : ILog, WT.IClient
{
    IDictionary<long, TL.User> Users { get; }
    IDictionary<long, TL.ChatBase> Chats { get; }

    // /// <summary>This event will be called when unsolicited updates/messages are sent by Telegram servers</summary>
    // /// <remarks>Make your handler <see langword="async"/>, or return <see cref="Task.CompletedTask"/> or <see langword="null"/><br/>See <see href="https://github.com/wiz0u/WTelegramClient/blob/master/Examples/Program_ListenUpdates.cs?ts=4#L23">Examples/Program_ListenUpdate.cs</see> for how to use this</remarks>
    // event Func<UpdatesBase, Task> OnUpdate;

    // /// <summary>This event is called for other types of notifications (login states, reactor errors, ...)</summary>
    // event Func<IObject, Task> OnOther;

    // /// <summary>Url for using a MTProxy. https://t.me/proxy?server=... </summary>
    // string MTProxyUrl { get; set; }

    // /// <summary>Telegram configuration, obtained at connection time</summary>
    // TL.Config TLConfig { get; }

    // /// <summary>Number of automatic reconnections on connection/reactor failure</summary>
    // int MaxAutoReconnects { get; set; }

    // /// <summary>Number of attempts in case of wrong verification_code or password</summary>
    // int MaxCodePwdAttempts { get; set; }

    // /// <summary>Number of seconds under which an error 420 FLOOD_WAIT_X will not be raised and your request will instead be auto-retried after the delay</summary>
    // int FloodRetryThreshold { get; set; }

    // /// <summary>Number of seconds between each keep-alive ping. Increase this if you have a slow connection or you're debugging your code</summary>
    // int PingInterval { get; set; }

    // /// <summary>Size of chunks when uploading/downloading files. Reduce this if you don't have much memory</summary>
    // int FilePartSize { get; set; }

    // /// <summary>Is this Client instance the main or a secondary DC session</summary>
    // bool IsMainDC { get; }

    // /// <summary>Has this Client established connection been disconnected?</summary>
    // bool Disconnected { get; }

    // /// <summary>ID of the current logged-in user or 0</summary>
    // long UserId { get; }

    // /// <summary>Info about the current logged-in user. This is filled after a successful (re)login</summary>
    // User User { get; }
}


// public delegate Task HandleUpdate(UpdatesBase update);
// public delegate Task HandleOther(IObject obj);
