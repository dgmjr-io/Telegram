using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Telegram.Bot.Configuration;

namespace Telegram.Bot.Filters;

/// <summary>
/// Check for "<inheritdoc cref="XTelegramBotApiSecretTokenHeader" path="/value" />" header value
/// </summary>
/// <remarks>Read more: <see href="https://core.telegram.org/bots/api#setwebhook"/> "secret_token"</remarks>
[AttributeUsage(AttributeTargets.Method)]
public sealed class ValidateTelegramBotRequestAttribute : TypeFilterAttribute
{
    /// <summary>The header name for the secret token</summary>
    /// <value>X-Telegram-Bot-Api-Secret-Token</value>
    public const string XTelegramBotApiSecretTokenHeader = "X-Telegram-Bot-Api-Secret-Token";

    public ValidateTelegramBotRequestAttribute()
        : base(typeof(ValidateTelegramBotRequestFilter)) { }

    private sealed class ValidateTelegramBotRequestFilter(IOptions<BotConfiguration> options)
        : IActionFilter
    {
        private readonly string? _secretToken = options?.Value?.SecretToken;

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsValidRequest(context.HttpContext.Request))
            {
                context.Result = new ObjectResult(
                    new ProblemDetails
                    {
                        Title = "Forbidden",
                        Detail = $"\"{XTelegramBotApiSecretTokenHeader}\" is invalid",
                        Type = "https://developer.mozilla.org/en-us/docs/Web/HTTP/Status/403",
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status403Forbidden
                    }
                );
            }
        }

        private bool IsValidRequest(HttpRequest request)
        {
            var isSecretTokenProvided = request.Headers.TryGetValue(
                XTelegramBotApiSecretTokenHeader,
                out var secretTokenHeader
            );
            return isSecretTokenProvided && string.Equals(secretTokenHeader, _secretToken, Ordinal);
        }
    }
}
