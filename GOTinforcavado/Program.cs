using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GOTinforcavado.Services;
using Blazored.SessionStorage;
using MudBlazor.Services;
using GOTinforcavado.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using DevExpress;
using GOTinforcavado.wwwroot;

namespace GOTinforcavado
{
    public class Program
    {

        public static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddDevExpressBlazor();


            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.IsDevelopment()
                    ? "https://localhost:7111/" 
                    : "https://localhost:7111/") 
            });





            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<TicketService>();
            builder.Services.AddScoped<UtilizadorService>();
            builder.Services.AddScoped<NewsLetterService>();
            builder.Services.AddScoped<ChatBotService>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<IHtmlEditorStringDataProvider, HtmlEditorStringDataProvider>() ;

            await builder.Build().RunAsync();
        }
    }
}
