namespace Telegram.Bot.Components.Actions;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;

[CustomAction(DeclarativeTypeConst)]
public class GetUser(
    IBotTelemetryClient telemetryClient,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
)
    : TelegramBotCustomAction<GetUser>(
        telemetryClient,
        DeclarativeTypeConst,
        sourceFilePath,
        sourceLineNumber
    )
{
    public new const string DeclarativeTypeConst = $"{Constants.Namespace}.{nameof(GetUser)}";

    public override async Task<DialogTurnResult> BeginDialogAsync(
        DialogContext dc,
        object? options = default,
        CancellationToken cancellationToken = default
    )
    {
        var recipientIdValue = RecipientId.GetValue(dc.State);
        var userChatId = recipientIdValue;
        var user = await CallBotAsync(
            dc,
            async bot => await bot.GetChatAsync(userChatId, cancellationToken)
        );
        return await dc.EndDialogAsync(user, cancellationToken);
    }
}
