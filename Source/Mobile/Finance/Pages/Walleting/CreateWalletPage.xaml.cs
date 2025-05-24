using Finance.Utilities;

namespace Finance.Pages.Walleting;

/// <summary>
/// Página responsável por criar uma nova carteira.
/// </summary>
public partial class CreateWalletPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel associado à página de criação de carteira.
    /// </summary>
    private readonly CreateWalletViewModel viewModel;

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Inicializa os componentes visuais e o BindingContext com a instância do ViewModel.
    /// </summary>
    public CreateWalletPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Injeta a instância do ViewModel e associa ao BindingContext.
        BindingContext = viewModel = Service.Get<CreateWalletViewModel>();
    }

    #endregion Constructors

    #region Start Methods

    /// <summary>
    /// Executado automaticamente quando a página aparece na tela.
    /// Define o foco no campo de entrada e bloqueia interações enquanto estiver em carregamento.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se o processo já está em execução para evitar sobreposição.
        if(!viewModel.IsRunning) {
            // Indica que a tela está em estado de carregamento.
            viewModel.IsRunning = true;

            // Aguarda brevemente antes de aplicar foco no campo de texto.
            await Task.Delay(100);
            // Define o foco para o campo de entrada.
            entry.Focus();

            // Libera o estado de carregamento permitindo interação do usuário.
            viewModel.IsRunning = false;
        }
    }

    #endregion Start Methods

    #region Navigation Methods

    /// <summary>
    /// Sobrescreve o comportamento padrão do botão "voltar" do dispositivo.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado manualmente.</returns>
    protected override bool OnBackButtonPressed() {
        // Executa o mesmo comportamento do botão de navegação personalizado.
        BackButtom_Clicked(null, null);
        // Impede que o sistema execute a navegação padrão.
        return true;
    }

    /// <summary>
    /// Evento chamado quando o botão de voltar da barra de navegação é clicado.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (não utilizado).</param>
    /// <param name="e">Argumentos do evento (não utilizados).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se é permitido voltar para a tela anterior.
        if(viewModel.CanBack()) {
            // Executa a navegação para a tela anterior de forma assíncrona.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods
}