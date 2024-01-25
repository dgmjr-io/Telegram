namespace Telegram.Bot.Extensions.TagHelpers;

using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;

using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Caching.Memory;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Security;

[HtmlTargetElement("telegram-login-widget")]
public class TelegramLoginWidget(
    IWebHostEnvironment hostingEnvironment,
    TagHelperMemoryCacheProvider cacheProvider,
    IFileVersionProvider fileVersionProvider,
    HtmlEncoder htmlEncoder,
    JavaScriptEncoder javaScriptEncoder,
    IUrlHelperFactory urlHelperFactory
)
    : ScriptTagHelper(
        hostingEnvironment,
        cacheProvider,
        fileVersionProvider,
        htmlEncoder,
        javaScriptEncoder,
        urlHelperFactory
    )
{
    [HtmlAttributeName("callback-uri")]
    public Uri CallbackUri { get; set; }

    [HtmlAttributeName("bot-name")]
    public string BotName { get; set; }

    [HtmlAttributeName("corner-radius")]
    [Range(0, 20)]
    public int CornerRadius { get; set; } = 20;

    [HtmlAttributeName("show-user-photo")]
    public bool ShowUserPhoto { get; set; } = true;

    public override void Init(TagHelperContext context)
    {
        Src = "https://telegram.org/js/telegram-widget.js?22";
        base.Init(context);
    }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("data-telegram-login", BotName);
        output.Attributes.Add("data-size", "large");
        output.Attributes.Add("data-radius", CornerRadius.ToString());
        output.Attributes.Add("data-request-access", "write");
        output.Attributes.Add("data-userpic", ShowUserPhoto.ToString().ToLower());
        output.Attributes.Add("data-auth-url", CallbackUri.ToString());
        return base.ProcessAsync(context, output);
    }
}

public enum TelegramLoginWidgetSize
{
    Small,
    small = Small,
    Medium,
    medium = Medium,
    Large,
    large = Large
}
