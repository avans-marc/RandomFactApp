﻿using Microsoft.Extensions.Logging;
using RandomFactApp.Domain.Clients;
using RandomFactApp.Infrastructure.FunGeneratorFactsApi;
using RandomFactApp.Infrastructure.UselessFactsJsphPIApi;
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

        /*
         *  The Dependency Injection 'chapter'
         *  
         *  Using the Microsoft.Extensions.Http library, we can register specific HttpClient instances tailored to our implementation. 
         *  This approach enables us to cleanly manage dependencies, such as the UselessFactsJsphPlApiClient.
         *  The HttpClientFactory takes care of managing the lifecycle of both the UselessFactsJsphPlApiClient and the underlying HttpClient it relies on. 
         *  This eliminates concerns about manually handling the lifetime of the HttpClient, improving reliability and reducing potential issues like socket exhaustion.
         */

        builder.Services.AddHttpClient<IRandomFactClient, UselessFactsJsphPlApiClient>(o =>
        {

            // We configure the base address hardcoded, but we could read it from a settings file
            o.BaseAddress = new Uri("https://uselessfacts.jsph.pl/api/v2/");

            // We wait 3 seconds for a response
            o.Timeout = TimeSpan.FromSeconds(3);
        });


        /*
         * An alternative approach would be to replace the implementation of the RandomFactClient with another one. 
         * Since classes depending on the client are designed to use an interface (through dependency injection), no changes are required in those classes. 
         * This ensures flexibility and promotes a clean, modular design.
         */

        //builder.Services.AddHttpClient<IRandomFactClient, CatFactNinjaApiClient>(o => {

        //    // We configure the base address hardcoded, but we could read it from a settings file
        //    o.BaseAddress = new Uri("https://catfact.ninja/");

        //    // We wait 10 seconds for a response
        //    o.Timeout = TimeSpan.FromSeconds(10);
        //});




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
