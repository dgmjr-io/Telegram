namespace Telegram.Bot.Identity;

using ExtensionProperty = Microsoft.Graph.ExtensionProperty;
using Constants;

public static class GraphServiceClientExtensions
{
    public static async Task<string?> GetTelegramIdPropertyAsync(this IDirectoryObjectsService directoryObjectsService)
    {
        var properties = await directoryObjectsService.GetExtensionPropertiesAsync();

        return Array.Find(properties, p => p.Name.EndsWith(ExtensionProperties.TelegramId))?.Name;
    }
    public static async Task<string?> GetTelegramUsernamePropertyAsync(this IDirectoryObjectsService directoryObjectsService)
    {
        var properties = await directoryObjectsService.GetExtensionPropertiesAsync();

        return Array.Find(properties, p => p.Name.EndsWith(ExtensionProperties.TelegramUsername))?.Name;
    }
}
