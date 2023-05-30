/*
 * UriFragments.cs
 *
 *   Created: 2023-03-19-02:53:23
 *   Modified: 2023-03-23-11:31:36
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */


namespace Telegram.Identity;

public static partial class StrClaimTypes
{
    public static class UriFragments
    {
        /// <summary>A URI fragment for <inheritdoc cref="UserId" path="/value" /></summary>
        /// <value>user_id/</value>
        public const string UserId = "user_id/";

        /// <summary>A URI fragment for <inheritdoc cref="Username" path="/value" /></summary>
        /// <value>username/</value>
        public const string Username = "username/";

        // / <summary>A URI fragment for <inheritdoc cref="GivenName" path="/value" /></summary>
        // / <value>givenname/</value>
        // public const string GivenName = "givenname/";
        // /// <summary>A URI fragment for <inheritdoc cref="Surname" path="/value" /></summary>
        // /// <value>surname/</value>
        // public const string Surname = "surname/";
        // /// <summary>A URI fragment for <inheritdoc cref="LanguageCode" path="/value" /></summary>
        // /// <value>languagecode/</value>
        /// <summary>A URI fragment for <inheritdoc cref="LanguageCode" path="/value" /></summary>
        /// <value>language_code/</value>
        public const string LanguageCode = "language_code/";

        /// <summary>A URI fragment for <inheritdoc cref="IsBot" path="/value" /></summary>
        /// <value>is_bot/</value>
        public const string IsBot = "is_bot/";

        /// <summary>A URI fragment for <inheritdoc cref="CanJoinGroups" path="/value" /></summary>
        /// <value>can_join_groups/</value>
        public const string CanJoinGroups = "can_join_groups/";

        /// <summary>A URI fragment for <inheritdoc cref="CanReadAllGroupMessages" path="/value" /></summary>
        /// <value>can_read_all_group_messages/</value>
        public const string CanReadAllGroupMessages = "can_read_all_group_messages/";

        /// <summary>A URI fragment for <inheritdoc cref="SupportsInlineQueries" path="/value" /></summary>
        /// <value>supports_inline_queries/</value>
        public const string SupportsInlineQueries = "supports_inline_queries/";

        /// <summary>A URI fragment for <inheritdoc cref="BotApiToken" path="/value" /></summary>
        /// <value>bot_api_token/</value>
        public const string BotApiToken = "bot_api_token/";
    }
}
