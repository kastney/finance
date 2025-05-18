using Finance.Utilities;

namespace Finance.Pages.DangerZone;

/// <summary>
/// P�gina que representa a zona de perigo, onde a��es cr�ticas podem ser executadas.
/// </summary>
public partial class DangerZonePage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel respons�vel pela l�gica da p�gina de zona de perigo.
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
        // Obt�m o ViewModel via inje��o de depend�ncia e define como contexto da p�gina.
        BindingContext = viewModel = Service.Get<DangerZoneViewModel>();
    }

    #endregion Constructor

    #region Started Methods

    /// <summary>
    /// Executado quando a p�gina aparece na tela. Controla o estado de carregamento.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se a p�gina j� est� em estado de carregamento.
        if(!viewModel.IsRunning) {
            // Define que est� em execu��o.
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
    /// Substitui o comportamento padr�o do bot�o de voltar, redirecionando para l�gica personalizada.
    /// </summary>
    /// <returns>Retorna true para cancelar a navega��o padr�o.</returns>
    protected override bool OnBackButtonPressed() {
        // Dispara manualmente a a��o de voltar ao clicar no bot�o de navega��o.
        BackButtom_Clicked(null, null);
        // Cancela a navega��o padr�o do bot�o de voltar.
        return true;
    }

    /// <summary>
    /// Lida com o evento de clique no bot�o de voltar da barra de navega��o.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (ignorado).</param>
    /// <param name="e">Argumentos do evento (ignorados).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se � poss�vel voltar para a tela anterior.
        if(viewModel.CanBack()) {
            // Executa a navega��o de volta.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods
}