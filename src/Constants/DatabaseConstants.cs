/*
 * DatabaseConstants.cs
 *
 *   Created: 2023-03-23-08:42:48
 *   Modified: 2023-03-23-11:04:32
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Constants.DbConstants
{
    public static class SchemaNames
    {
        public const string TeleSchema = "tele";
    }

    public static class TableNames
    {
        /// <summary>
        /// The prefix for tables, <inheritdoc cref="tbl_" path="/value" />
        /// </summary>
        /// <value>tbl_</value>
        private const string tbl_ = nameof(tbl_);

        /// <summary>
        /// The table name for Bots, <inheritdoc cref="Bot" path="/value" />
        /// </summary>
        /// <value><inheritdoc cref="tbl_" path="/value" />Bot</value>
        public const string Bot = tbl_ + nameof(Bot);

        /// <summary>
        /// The table name for Groups, <inheritdoc cref="Group" path="/value" />
        /// </summary>
        /// <value><inheritdoc cref="tbl_" path="/value" />Group</value>
        public const string Group = tbl_ + nameof(Group);

        /// <summary>
        /// The table name for Channels, <inheritdoc cref="Channel" path="/value" />
        /// </summary>
        /// <value><inheritdoc cref="tbl_" path="/value" />Channel</value>
        public const string Channel = tbl_ + nameof(Channel);
    }

    public static class ColumnNames
    {
        public static class Bot
        {
            public const string Id = "Id";
            public const string Name = nameof(Name);
            public const string TelegramUsername = nameof(TelegramUsername);
            public const string SendPulseId = nameof(SendPulseId);
            public const string ApiToken = nameof(ApiToken);
        }

        public static class GroupAndChannel
        {
            public const string Id = nameof(Id);
            public const string Name = nameof(Name);
            public const string Description = nameof(Description);
            public const string TelegramUsername = nameof(TelegramUsername);
        }
    }
}
