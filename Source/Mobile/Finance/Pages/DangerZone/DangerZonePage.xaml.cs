using Finance.Utilities;

namespace Finance.Pages.DangerZone;

/// <summary>
/// Página que representa a zona de perigo, onde ações críticas podem ser executadas.
/// </summary>
public partial class DangerZonePage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel responsável pela lógica da página de zona de perigo.
    /// </summary>
    private readonly DangerZoneViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa os componentes visuais e configura o binding com o ViewModel.
    /// </summary>
    public DangerZonePage() {
        // Inicializa os componentes definidos no XAML.
        InitializeComponent();
        // Obtém o ViewModel via injeção de dependência e define como contexto da página.
        BindingContext = viewModel = Service.Get<DangerZoneViewModel>();
    }

    #endregion Constructor

    #region Started Methods

    /// <summary>
    /// Executado quando a página aparece na tela. Controla o estado de carregamento.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se a página já está em estado de carregamento.
        if(!viewModel.IsRunning) {
            // Define que está em execução.
            viewModel.IsRunning = true;

            // Aguarda um pequeno atraso, geralmente usado para garantir fluidez.
            await Task.Delay(100);

            // Finaliza o estado de carregamento.
            viewModel.IsRunning = false;
        }
    }

    #endregion Started Methods

    #region Navigation Methods

    /// <summary>
    /// Substitui o comportamento padrão do botão de voltar, redirecionando para lógica personalizada.
    /// </summary>
    /// <returns>Retorna true para cancelar a navegação padrão.</returns>
    protected override bool OnBackButtonPressed() {
        // Dispara manualmente a ação de voltar ao clicar no botão de navegação.
        BackButtom_Clicked(null, null);
        // Cancela a navegação padrão do botão de voltar.
        return true;
    }

    /// <summary>
    /// Lida com o evento de clique no botão de voltar da barra de navegação.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (ignorado).</param>
    /// <param name="e">Argumentos do evento (ignorados).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se é possível voltar para a tela anterior.
        if(viewModel.CanBack()) {
            // Executa a navegação de volta.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods
}