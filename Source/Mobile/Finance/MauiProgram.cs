using DevExpress.Maui;
using Finance.Pages;
using Finance.Pages.DangerZone;
using Finance.Pages.Initialize;
using Finance.Pages.Walleting;
using Finance.Services;
using Finance.Services.Walleting;
using Microsoft.Extensions.Logging;

namespace Finance {

    public static class MauiProgram {

        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .UseDevExpress(useLocalization: false)
                .UseDevExpressCollectionView()
                .UseDevExpressControls()
                .UseDevExpressEditors()
                .UseDevExpressCharts()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "IconsSolid");
                });

            // Services
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IWalletService, WalletService>();

            // ViewModels
            // ...
            builder.Services.AddTransient<LoadingViewModel>();
            builder.Services.AddTransient<PresentationViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            // Manage...
            builder.Services.AddTransient<SelectWalletViewModel>();
            builder.Services.AddTransient<CreateWalletViewModel>();
            builder.Services.AddTransient<DeleteWalletViewModel>();
            // ..
            builder.Services.AddTransient<DangerZoneViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}