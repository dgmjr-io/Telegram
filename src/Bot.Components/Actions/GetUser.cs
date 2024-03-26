namespace Telegram.Bot.Components.Actions;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

[CustomAction(DeclarativeTypeConst)]
public class GetUser() : TelegramBotCustomAction(DeclarativeTypeConst)
{
    public new const string DeclarativeTypeConst = $"{Constants.Namespace}.{nameof(GetUser)}";

    public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object? options = default, CancellationToken cancellationToken = default)
    {
        var user = await CallBotAsync(dc, async bot => await bot.GetChatAsync(RecipientId.GetValue(dc.State)));
        return await dc.EndDialogAsync(user, cancellationToken);
    }
}
