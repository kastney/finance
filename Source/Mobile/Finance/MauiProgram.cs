using DevExpress.Maui;
using Finance.Pages;
using Finance.Pages.SelectWallet;
using Finance.Services;
using Microsoft.Extensions.Logging;

namespace Finance {

    public static class MauiProgram {

        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .UseDevExpress()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "IconsSolid");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "IconsRegular");
                });

            /// Services
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IWalletService, WalletService>();

            /// ViewModels
            // ...
            builder.Services.AddTransient<LoadingViewModel>();
            builder.Services.AddTransient<WalletPresentationViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            // ...
            builder.Services.AddTransient<SelectWalletViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}