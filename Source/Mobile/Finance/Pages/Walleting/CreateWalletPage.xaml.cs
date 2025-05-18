using Finance.Utilities;

namespace Finance.Pages.Walleting;

/// <summary>
/// P�gina respons�vel por criar uma nova carteira.
/// </summary>
public partial class CreateWalletPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel associado � p�gina de cria��o de carteira.
    /// </summary>
    private readonly CreateWalletViewModel viewModel;

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Inicializa os componentes visuais e o BindingContext com a inst�ncia do ViewModel.
    /// </summary>
    public CreateWalletPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Injeta a inst�ncia do ViewModel e associa ao BindingContext.
        BindingContext = viewModel = Service.Get<CreateWalletViewModel>();
    }

    #endregion Constructors

    #region Start Methods

    /// <summary>
    /// Executado automaticamente quando a p�gina aparece na tela.
    /// Define o foco no campo de entrada e bloqueia intera��es enquanto estiver em carregamento.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se o processo j� est� em execu��o para evitar sobreposi��o.
        if(!viewModel.IsRunning) {
            // Indica que a tela est� em estado de carregamento.
            viewModel.IsRunning = true;

            // Aguarda brevemente antes de aplicar foco no campo de texto.
            await Task.Delay(100);
            // Define o foco para o campo de entrada.
            entry.Focus();

            // Libera o estado de carregamento permitindo intera��o do usu�rio.
            viewModel.IsRunning = false;
        }
    }

    #endregion Start Methods

    #region Navigation Methods

    /// <summary>
    /// Sobrescreve o comportamento padr�o do bot�o "voltar" do dispositivo.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado manualmente.</returns>
    protected override bool OnBackButtonPressed() {
        // Executa o mesmo comportamento do bot�o de navega��o personalizado.
        BackButtom_Clicked(null, null);
        // Impede que o sistema execute a navega��o padr�o.
        return true;
    }

    /// <summary>
    /// Evento chamado quando o bot�o de voltar da barra de navega��o � clicado.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (n�o utilizado).</param>
    /// <param name="e">Argumentos do evento (n�o utilizados).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se � permitido voltar para a tela anterior.
        if(viewModel.CanBack()) {
            // Executa a navega��o para a tela anterior de forma ass�ncrona.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods
}