namespace Telegram.OpenIdConnect.Models.Responses;

using Errors.Abstractions;

public class CheckClientResult
{
    public Client Client { get; set; }

    public string ClientId { get; set; }

    /// <summary>
    /// The clinet is found in my Clients Store
    /// </summary>
    public bool IsSuccess { get; set; }
    public IErrorType Error { get; set; }
}
