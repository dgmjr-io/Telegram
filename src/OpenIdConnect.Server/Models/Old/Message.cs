namespace Telegram.OpenIdConnect.Models;

using CorrelationId.Abstractions;
using CorrelationId;
using Errors.Abstractions;
using Telegram.OpenIdConnect.Models.Responses;

public interface IMessage
{
    [JProp("correlation_id")]
    string CorrelationId { get; set; }
}

public abstract class Message : IMessage
{
    public virtual string CorrelationId { get; set; }
}

public abstract class Message<TOkResponse, TErrorResponse>(
// IHttpContextAccessor? httpContextAccessor = default
) : Message()
    where TOkResponse : IMessage, new()
    where TErrorResponse : ErrorResponse, IMessage, new()
{
    // public IHttpContextAccessor HttpContextAccessor
    // {
    //     get =>
    //         httpContextAccessor
    //         ?? new HttpContextAccessor { HttpContext = new DefaultHttpContext() };
    //     set => httpContextAccessor = value;
    // }

    // public HttpContext HttpContext => httpContextAccessor?.HttpContext ?? new DefaultHttpContext();

    // public override CorrelationContext? CorrelationContext
    // {
    //     get =>
    //         HttpContext?.RequestServices
    //             .GetRequiredService<ICorrelationContextAccessor>()
    //             .CorrelationContext;
    //     set =>
    //         HttpContext.RequestServices
    //             .GetRequiredService<ICorrelationContextAccessor>()
    //             .CorrelationContext = value;
    // }

    public override string CorrelationId { get; set; }

    public TOkResponse CreateResponse() => new() { CorrelationId = CorrelationId };

    public TErrorResponse CreateErrorResponse(IErrorType errorType) =>
        new() { CorrelationId = CorrelationId, ErrorType = errorType };
}
