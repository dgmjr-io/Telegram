/*
 * ChatType.cs
 *
 *   Created: 2023-03-09-05:31:27
 *   Modified: 2023-03-09-05:31:27
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Schema.Enum;

[GenerateEnumerationRecordStruct("ChatType", "Telegram.Schema")]
/// <summary>
/// Type of chat, can be either “private”, “group”, “supergroup” or “channel”
/// </summary>
public enum ChatType
{
    Channel,
    Group,
    Private,
    Supergroup
};
