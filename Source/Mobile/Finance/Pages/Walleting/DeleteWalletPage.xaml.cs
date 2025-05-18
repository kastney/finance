using Finance.Utilities;

namespace Finance.Pages.Walleting;

/// <summary>
/// P�gina respons�vel por exibir a interface para deletar uma carteira espec�fica,
/// permitindo ao usu�rio confirmar a exclus�o com um aviso visual e um checkbox.
/// </summary>
public partial class DeleteWalletPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel que cont�m a l�gica e os dados necess�rios para a exclus�o da carteira.
    /// </summary>
    private readonly DeleteWalletViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa os componentes da interface e associa o contexto de dados ao ViewModel.
    /// </summary>
    public DeleteWalletPage() {
        // Inicializa os componentes visuais da p�gina.
        InitializeComponent();
        // Define o contexto de binding para o ViewModel associado.
        BindingContext = viewModel = Service.Get<DeleteWalletViewModel>();
    }

    #endregion Constructor

    #region Started Methods

    /// <summary>
    /// Executa a��es quando a p�gina aparece, iniciando e finalizando o indicador de carregamento.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se a opera��o ainda n�o est� em execu��o.
        if(!viewModel.IsRunning) {
            // Define que a opera��o est� em execu��o.
            viewModel.IsRunning = true;
            // Desabilita a intera��o enquanto carrega.
            viewModel.IsRunningInverse = false;

            // Simula uma pequena espera para dar tempo de anima��es ou carregamentos visuais.
            await Task.Delay(100);

            // Reabilita a intera��o do usu�rio.
            viewModel.IsRunningInverse = true;
            // Finaliza a execu��o.
            viewModel.IsRunning = false;
        }
    }

    #endregion Started Methods

    #region Navigation Methods

    /// <summary>
    /// Intercepta o bot�o de voltar f�sico ou da barra do dispositivo, executando a navega��o personalizada.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado manualmente.</returns>
    protected override bool OnBackButtonPressed() {
        // Chama o m�todo que trata o clique no bot�o de voltar.
        BackButtom_Clicked(null, null);
        // Impede o comportamento padr�o do bot�o voltar.
        return true;
    }

    /// <summary>
    /// Manipula o evento de clique no bot�o de voltar na barra de navega��o personalizada.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (n�o utilizado).</param>
    /// <param name="e">Argumentos do evento (n�o utilizado).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se a navega��o de retorno � permitida.
        if(viewModel.CanBack()) {
            // Executa a navega��o de retorno definida no ViewModel.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods

    #region Behavior Methods

    /// <summary>
    /// Alterna o estado do checkbox quando o texto ao lado dele � tocado.
    /// </summary>
    /// <param name="sender">Objeto que disparou o toque (Label).</param>
    /// <param name="e">Argumentos do evento de toque.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        // Inverte o estado atual do checkbox.
        checkBox.IsChecked = !checkBox.IsChecked;
    }

    #endregion Behavior Methods
}