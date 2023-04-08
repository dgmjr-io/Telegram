/* 
 * TelegramDbContext.cs
 * 
 *   Created: 2023-03-23-09:50:46
 *   Modified: 2023-03-23-11:00:21
 * 
 *   Author: David G. Moore, Jr. <david@dgmjr.io>
 *   
 *   Copyright © 2022 - 2023 David G. Moore, Jr., All Rights Reserved
 *      License: MIT (https://opensource.org/licenses/MIT)
 */

using System.Runtime.Serialization;
using Dgmjr.EntityFrameworkCore.Constants;
using Microsoft.EntityFrameworkCore;
using Telegram.Constants.DbConstants;
using Telegram.EntityFrameworkCore.Abstractions;
using Bot = Telegram.Bots.Models.Bot;
namespace Telegram.EntityFrameworkCore;

public class TelegramDbContext : ValidatedDbContext, ITelegramDbContext
{
    public TelegramDbContext(DbContextOptions<TelegramDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Bot> Bots { get; set; } = default!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Telegram.Bots.Models.Bot>(entity =>
        {
            entity.ToTable(TableNames.Bot, DboSchema);
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName(ColumnNames.Bot.Id);
            entity.Property(e => e.TelegramUsername).HasColumnName(ColumnNames.Bot.TelegramUsername);
            entity.Property(e => e.Name).HasColumnName(ColumnNames.Bot.Name);
            entity.Property(e => e.SendPulseId).HasColumnName(ColumnNames.Bot.SendPulseId);
            entity.Property(e => e.ApiToken).HasColumnName(ColumnNames.Bot.ApiToken);
        });
    }
}
