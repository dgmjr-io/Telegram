namespace Telegram.Bot.Types;

using static Dgmjr.EntityFrameworkCore.Constants.Prefixes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

public static class BotApiTokenEfCoreExtensions
{
    public static PropertyBuilder<BotApiToken> BotTokenProperty<TEntity>(
        this EntityTypeBuilder<TEntity> entityBuilder,
        Expression<Func<TEntity, BotApiToken>> propertyExpression,
        string validationFunctionSchemaName = DboSchema.ShortName,
        string validationFunctionName = IsValidBotToken
    )
        where TEntity : class
    {
        entityBuilder.HasCheckConstraint(
            ck_ + nameof(BotApiToken),
            $"[{validationFunctionSchemaName}].[{validationFunctionName}]([{entityBuilder.Property(propertyExpression).Metadata.GetColumnName()}])"
        );
        return entityBuilder
            .Property(propertyExpression)
            .HasConversion(new BotApiTokenConverter())
            .HasMaxLength(BotApiToken.Length)
            .IsUnicode(false);
    }

    public static PropertyBuilder<ObjectId> BotTokenProperty<TEntity>(
        this EntityTypeBuilder<TEntity> entityBuilder,
        Expression<Func<TEntity, ObjectId>> propertyExpression,
        string validationFunctionSchemaName = DboSchema.ShortName,
        string validationFunctionName = IsValidBotToken
    )
        where TEntity : class =>
        entityBuilder.BotTokenProperty(
            propertyExpression,
            validationFunctionSchemaName,
            validationFunctionName
        );

    public static PropertyBuilder<ObjectId> BotTokenProperty<TEntity>(
        this ModelBuilder modelBuilder,
        Expression<Func<TEntity, ObjectId>> propertyExpression
    )
        where TEntity : class =>
        modelBuilder.Entity<TEntity>().BotTokenProperty(propertyExpression);

    public static MigrationBuilder HasIsValidBotTokenFunction(
        this MigrationBuilder migrationBuilder
    ) => migrationBuilder.HasIsValidBotTokenFunction("ufn_IsValidBotToken");

    public static MigrationBuilder HasIsValidBotTokenFunction(
        this MigrationBuilder migrationBuilder,
        string functionName
    ) => migrationBuilder.HasIsValidBotTokenFunction("dbo", functionName);

    public static MigrationBuilder HasIsValidBotTokenFunction(
        this MigrationBuilder migrationBuilder,
        string schema,
        string functionName
    )
    {
        migrationBuilder.Operations.Add(
            new CreateFunctionOperation(
                schema,
                functionName,
                "@value varchar(255)",
                typeof(Constants).Assembly.ReadAssemblyResourceAllText(IsValidBotToken + _sql)
            )
        );
        return migrationBuilder;
    }

    public static MigrationBuilder RollBackIsValidBotTokenFunction(
        this MigrationBuilder migrationBuilder
    ) => migrationBuilder.RollBackIsValidBotTokenFunction(IsValidBotToken);

    public static MigrationBuilder RollBackIsValidBotTokenFunction(
        this MigrationBuilder migrationBuilder,
        string functionName
    ) => migrationBuilder.RollBackIsValidBotTokenFunction("dbo", functionName);

    public static MigrationBuilder RollBackIsValidBotTokenFunction(
        this MigrationBuilder migrationBuilder,
        string schema,
        string functionName
    )
    {
        migrationBuilder.Operations.Add(new DropFunctionOperation(schema, functionName));
        return migrationBuilder;
    }
}
