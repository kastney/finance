using Finance.Utilities;

namespace Finance.Pages.Notify;

/// <summary>
/// P�gina de notifica��es da carteira, respons�vel por apresentar a .
/// </summary>
public partial class NotifyPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel associado � p�gina, respons�vel pela l�gica e dados apresentados na interface.
    /// </summary>
    private readonly NotifyViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa a p�gina de notifica��o e o contexto de binding com a ViewModel.
    /// </summary>
    public NotifyPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Obt�m a inst�ncia da ViewModel a partir do servi�o de inje��o de depend�ncia.
        BindingContext = viewModel = Service.Get<NotifyViewModel>();
    }

    #endregion Constructor

    #region Navigation Methods

    /// <summary>
    /// Manipula o evento de pressionar o bot�o "voltar" do sistema.
    /// </summary>
    /// <returns>Retorna verdadeiro para cancelar a navega��o, falso para permitir.</returns>
    protected override bool OnBackButtonPressed() {
        // Executa manualmente o m�todo de clique do bot�o de voltar da interface.
        BackButtom_Clicked(null, null);
        // Indica que a navega��o padr�o do sistema n�o deve ser executada.
        return true;
    }

    /// <summary>
    /// Executado quando o bot�o de voltar na interface de navega��o � clicado.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (pode ser nulo).</param>
    /// <param name="e">Argumentos do evento (pode ser nulo).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se � poss�vel navegar para a tela anterior de forma segura.
        if(viewModel.CanBack()) {
            // Solicita ao ViewModel que execute a navega��o de retorno.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods
}