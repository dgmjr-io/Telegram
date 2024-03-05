namespace Telegram.Bot;

using Microsoft.AspNetCore.Mvc;
using Dgmjr.AspNetCore.Mvc;

using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Controller;
using Telegram.Bot.Extensions;

public abstract class TelegramBotController(IServiceProvider services)
    : ApiControllerBase(services.GetRequiredService<ILogger<TelegramBotController>>()),
        ITelegramBotController,
        ITelegramBotClient
{
    /// <summary>The MIME type for a Telegram Bot update.</summary>
    /// <value><inheritdoc cref="MediaType.Application.DisplayName" path="/value" />/telegram-bot-update<inheritdoc cref="Suffixes.Json.DisplayName" path="/value" /></value>
    public const string TelegramBotUpdateMimeType =
        $"{MediaType.Application.DisplayName}/telegram-bot-update{Suffixes.Json.DisplayName}";

    protected ITelegramBotClient Bot => services.GetRequiredService<ITelegramBotClient>();

    #region ITelegramBotClient implementation
    public bool LocalBotServer => Bot.LocalBotServer;

    public long? BotId => Bot.BotId;

    public duration Timeout
    {
        get => Bot.Timeout;
        set => Bot.Timeout = value;
    }
    public IExceptionParser ExceptionsParser
    {
        get => Bot.ExceptionsParser;
        set => Bot.ExceptionsParser = value;
    }

    public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest
    {
        add { Bot.OnMakingApiRequest += value; }
        remove { Bot.OnMakingApiRequest -= value; }
    }

    public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived
    {
        add { Bot.OnApiResponseReceived += value; }
        remove { Bot.OnApiResponseReceived -= value; }
    }

    [NonAction]
    public Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
    {
        return Bot.MakeRequestAsync(request, cancellationToken);
    }

    [NonAction]
    public Task<bool> TestApiAsync(CancellationToken cancellationToken = default)
    {
        return Bot.TestApiAsync(cancellationToken);
    }

    [NonAction]
    public Task DownloadFileAsync(
        string filePath,
        Stream destination,
        CancellationToken cancellationToken = default
    )
    {
        return Bot.DownloadFileAsync(filePath, destination, cancellationToken);
    }
    #endregion

    /// <summary>Processes an update from the Telegram bot server</summary>
    /// <param name="update">The <see cref="Update" /></param>
    [HttpPost]
    [Consumes(TelegramBotUpdateMimeType)]
    [ProducesOKResponse<IActionResult>]
    public async Task<IActionResult> PostUpdateAsync(Update update)
    {
        try
        {
            await HandleUpdateAsync(update);
        }
        catch (Exception ex)
        {
            await SendExceptionAsync(update, ex);
        }
        return Ok();
    }

    protected virtual async Task SendExceptionAsync(Update update, Exception ex)
    {
        await Bot.SendTextMessageAsync(
            update.GetChatId(),
            $"An exception occurred whilst processing your request:\n{ex.GetType().Name}: {ex.Message}."
        );
        Logger.LogError(ex, ex.Message);
    }

    protected abstract Task HandleUpdateAsync(Update update);

    protected virtual Task ExecuteBotCommandAsync(Update update, string botCommand, string[] args)
    {
        return Task.CompletedTask;
    }
}
