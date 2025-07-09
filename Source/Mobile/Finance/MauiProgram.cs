using Finance.Pages;
using Finance.Pages.DangerZone;
using Finance.Pages.Initialize;
using Finance.Pages.Notify;
using Finance.Pages.Strategy;
using Finance.Pages.Walleting;
using Finance.Services.Navigation;
using Finance.Services.Walleting;

namespace Finance;

/// <summary>
/// Classe responsável por configurar e criar a aplicação MAUI.
/// </summary>
public static class MauiProgram {

    #region Methods

    /// <summary>
    /// Cria e configura a instância principal da aplicação MAUI.
    /// </summary>
    /// <returns>Instância da aplicação MAUI configurada.</returns>
    public static MauiApp CreateMauiApp() {
        // Inicializa o builder da aplicação.
        var builder = MauiApp.CreateBuilder();

        // Configura a classe principal do aplicativo e registra os pacotes DevExpress utilizados.
        builder.UseMauiApp<App>()
            // Desativa a localização para o DevExpress.
            .UseDevExpress(useLocalization: false)
            // Habilita suporte ao DevExpress CollectionView.
            .UseDevExpressCollectionView()
            // Registra os controles DevExpress para uso na UI.
            .UseDevExpressControls()
            // Registra os editores DevExpress (como editores de texto, seleção, etc).
            .UseDevExpressEditors()
            // Registra os componentes de gráfico DevExpress.
            .UseDevExpressCharts()
            // Configura as fontes da aplicação.
            .ConfigureFonts(fonts => {
                // Adiciona a fonte padrão "OpenSans-Regular".
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                // Adiciona a fonte semibold "OpenSans-Semibold".
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                // Adiciona a fonte de ícones Font Awesome 6 (estilo sólido).
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "IconsSolid");
            });

        #region Services

        // Registra o serviço de navegação como singleton.
        builder.Services.AddSingleton<INavigationService, Services.Navigation.NavigationService>();
        // Registra o serviço de gerenciamento de carteiras como singleton.
        builder.Services.AddSingleton<IWalletService, WalletService>();

        #endregion Services

        #region ViewModels

        // Registra a ViewModel da tela de carregamento como instância transitória.
        builder.Services.AddTransient<LoadingViewModel>();
        // Registra a ViewModel da tela de apresentação como instância transitória.
        builder.Services.AddTransient<PresentationViewModel>();
        // Registra a ViewModel da tela principal como instância transitória.
        builder.Services.AddTransient<MainViewModel>();
        // Registra a ViewModel para seleção de carteira como instância transitória.
        builder.Services.AddTransient<SelectWalletViewModel>();
        // Registra a ViewModel para página de notificações da página como instância transitória.
        builder.Services.AddTransient<NotifyViewModel>();
        // Registra a ViewModel para criação de carteira como instância transitória.
        builder.Services.AddTransient<CreateWalletViewModel>();
        // Registra a ViewModel da página de exclusão de carteira como instância transitória.
        builder.Services.AddTransient<DeleteWalletViewModel>();
        // Registra a ViewModel da zona de perigo como instância transitória.
        builder.Services.AddTransient<DangerZoneViewModel>();
        // Registra a ViewModel da página de estratégia como instância transitória.
        builder.Services.AddTransient<StrategyViewModel>();
        // Registra a ViewModel da página de criação de um Grupo de Ativos como instância transitória.
        builder.Services.AddTransient<CreateAssetGroupViewModel>();
        // Registra a ViewModel da página de edição dos ativos dentro do grupo de ativos.
        builder.Services.AddTransient<AssetGroupViewModel>();

        #endregion ViewModels

#if DEBUG
        // Ativa o provedor de logs no modo de depuração.
        builder.Logging.AddDebug();
#endif

        // Cria e retorna a aplicação configurada.
        return builder.Build();
    }

    #endregion Methods
}