/*
 * BotEntityTypeBuilder.cs
 *
 *   Created: 2023-03-23-11:09:29
 *   Modified: 2023-03-23-11:09:30
 *
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

namespace Telegram.Bots.EntityFrameworkCore;
using System.Runtime.Serialization;
using Dgmjr.EntityFrameworkCore.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telegram.Constants.DbConstants;
using Telegram.EntityFrameworkCore.Abstractions;
using Bot = Telegram.Bots.Models.Bot;

public class BotTypeConfigurator : IEntityTypeConfiguration<Bot>,  IEntityTypeConfiguration<MyBot>,  IEntityTypeConfiguration<SendPulseBot>
{
    public void Configure(EntityTypeBuilder<Bot> builder)
    {
        builder.ToTable(TableNames.Bot, TeleSchema);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName(ColumnNames.Bot.Id);
        builder.Property(e => e.TelegramUsername).HasColumnName(ColumnNames.Bot.TelegramUsername);
        builder.Property(e => e.Name).HasColumnName(ColumnNames.Bot.Name);
    }

    public void Configure(EntityTypeBuilder<MyBot> builder)
    {
        builder.ToTable(TableNames.Bot, TeleSchema);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName(ColumnNames.Bot.Id);
        builder.Property(e => e.TelegramUsername).HasColumnName(ColumnNames.Bot.TelegramUsername);
        builder.Property(e => e.Name).HasColumnName(ColumnNames.Bot.Name);
        builder.Property(e => e.ApiToken).ha
    }
}
