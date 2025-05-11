using DevExpress.Maui;
using Finance.Pages;
using Finance.Pages.Walleting.CreateWallet;
using Finance.Pages.DangerZone;
using Finance.Pages.SelectWallet;
using Finance.Pages.Strategy;
using Finance.Services;
using Finance.Services.Walleting;
using Finance.ViewModels;
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
            builder.Services.AddTransient<WalletPresentationViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            // ...
            builder.Services.AddTransient<SelectWalletViewModel>();
            builder.Services.AddTransient<CreateWalletViewModel>();
            builder.Services.AddTransient<StrategyViewModel>();
            builder.Services.AddTransient<DangerZoneViewModel>();
            builder.Services.AddTransient<DeleteWalletViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}