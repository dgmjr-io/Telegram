using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot.Components.Extensions;

using IMiddleware = Microsoft.Bot.Builder.IMiddleware;

namespace Telegram.Bot.Components.Middleware;

public class UserDataMiddleware(
    UserDataAccessor userStateAccessor,
    ConversationState conversationState
) : IMiddleware
{
    public UserData UserData { get; private set; }
    public UserDataAccessor UserDataAccessor => userStateAccessor;

    public async Task OnTurnAsync(
        ITurnContext turnContext,
        NextDelegate next,
        CancellationToken cancellationToken = default
    )
    {
        UserData = await UserDataAccessor.GetAsync(turnContext, cancellationToken);
        if (turnContext.Activity.ChannelData is not null)
        {
            var telegramChannelData = turnContext.GetTelegramChannelData();
            telegramChannelData.AssignTo(UserData);
        }

        await next(cancellationToken);

        await conversationState.SaveChangesAsync(turnContext, true, cancellationToken);
    }
}
