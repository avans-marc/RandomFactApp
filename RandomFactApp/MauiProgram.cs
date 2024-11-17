using Microsoft.Extensions.Logging;
using RandomFactApp.Domain.Services;
using RandomFactApp.Infrastructure;
using RandomFactApp.ViewModels;


namespace RandomFactApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        // The Dependency Injection 'chapter'

        // Register the HttpClient for the random FactClient
        // Using the Microsoft.Extensions.HTTP library allows us to register specific HTTP Clients that are connected to our implementation
        // The lifetime of the UselessFactsJsphPlApiClient and the HttpClient it depends on is managed by the HttpClientFactory.
        // We have less worrying about the 
        builder.Services.AddHttpClient<IRandomFactClient, UselessFactsJsphPlApiClient>(o => {

            // We configure the base address hardcoded, but we could read it from a settings file
            o.BaseAddress = new Uri("https://uselessfacts.jsph.pl/api/v2/");

            // We wait 3 seconds for a response
            o.Timeout = TimeSpan.FromSeconds(3);
        });

        // We could register other HttpClients here with different timeout/resilience options.

        // Transient: resolve the object everytime we need it
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<MainPage>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
