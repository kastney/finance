using Finance.Utilities;

namespace Finance.Pages.Notify;

/// <summary>
/// Página de notificações da carteira, responsável por apresentar a .
/// </summary>
public partial class NotifyPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel associado à página, responsável pela lógica e dados apresentados na interface.
    /// </summary>
    private readonly NotifyViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa a página de notificação e o contexto de binding com a ViewModel.
    /// </summary>
    public NotifyPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Obtém a instância da ViewModel a partir do serviço de injeção de dependência.
        BindingContext = viewModel = Service.Get<NotifyViewModel>();
    }

    #endregion Constructor

    #region Navigation Methods

    /// <summary>
    /// Manipula o evento de pressionar o botão "voltar" do sistema.
    /// </summary>
    /// <returns>Retorna verdadeiro para cancelar a navegação, falso para permitir.</returns>
    protected override bool OnBackButtonPressed() {
        // Executa manualmente o método de clique do botão de voltar da interface.
        BackButtom_Clicked(null, null);
        // Indica que a navegação padrão do sistema não deve ser executada.
        return true;
    }

    /// <summary>
    /// Executado quando o botão de voltar na interface de navegação é clicado.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (pode ser nulo).</param>
    /// <param name="e">Argumentos do evento (pode ser nulo).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se é possível navegar para a tela anterior de forma segura.
        if(viewModel.CanBack()) {
            // Solicita ao ViewModel que execute a navegação de retorno.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods
}