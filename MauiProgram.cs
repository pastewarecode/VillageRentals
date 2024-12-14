using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using VillageRentals.Components.Controller;

namespace VillageRentals
{
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<ClientController>();
            builder.Services.AddSingleton<InventoryController>();
            builder.Services.AddSingleton<RentalController>();

            //Initialize a database object and add load data to the database using the database.cs class
            var VillageRentalsDB = new Database();
            VillageRentalsDB.LoadData();
            Console.WriteLine("Database loaded successfully."); //success message

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
