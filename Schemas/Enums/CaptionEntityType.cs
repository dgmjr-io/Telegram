/*
 * CaptionEntityType.cs
 *
 *   Created: 2023-03-09-06:23:00
 *   Modified: 2023-03-09-06:23:00
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022-2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */
namespace Telegram.Schema.Enum;

[GenerateEnumerationRecordStruct("CaptionEntityType", "Telegram.Schema")]
/// <summary>
/// Type of the entity. Can be “mention” (`@username`), “hashtag” (`#hashtag`), “cashtag”
/// (`$USD`), “bot\_command” (`/start@jobs_bot`), “url” (`https://telegram.org`), “email”
/// (`do-not-reply@telegram.org`), “phone\_number” (`+1-212-555-0123`), “bold” (**bold
/// text**), “italic” (*italic text*), “underline” (underlined text), “strikethrough”
/// (strikethrough text), “code” (monowidth string), “pre” (monowidth block), “text\_link”
/// (for clickable text URLs), “text\_mention” (for users [without
/// usernames](https://telegram.org/blog/edit#new-mentions))
/// </summary>
public enum CaptionEntityType
{
    Bold,
    BotCommand,
    Cashtag,
    Code,
    Email,
    Hashtag,
    Italic,
    Mention,
    PhoneNumber,
    Pre,
    Strikethrough,
    TextLink,
    TextMention,
    Underline,
    Url
};
