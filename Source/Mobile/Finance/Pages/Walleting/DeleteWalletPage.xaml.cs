using Finance.Utilities;

namespace Finance.Pages.Walleting;

/// <summary>
/// Página responsável por exibir a interface para deletar uma carteira específica,
/// permitindo ao usuário confirmar a exclusão com um aviso visual e um checkbox.
/// </summary>
public partial class DeleteWalletPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel que contém a lógica e os dados necessários para a exclusão da carteira.
    /// </summary>
    private readonly DeleteWalletViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa os componentes da interface e associa o contexto de dados ao ViewModel.
    /// </summary>
    public DeleteWalletPage() {
        // Inicializa os componentes visuais da página.
        InitializeComponent();
        // Define o contexto de binding para o ViewModel associado.
        BindingContext = viewModel = Service.Get<DeleteWalletViewModel>();
    }

    #endregion Constructor

    #region Started Methods

    /// <summary>
    /// Executa ações quando a página aparece, iniciando e finalizando o indicador de carregamento.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se a operação ainda não está em execução.
        if(!viewModel.IsRunning) {
            // Define que a operação está em execução.
            viewModel.IsRunning = true;
            // Desabilita a interação enquanto carrega.
            viewModel.IsRunningInverse = false;

            // Simula uma pequena espera para dar tempo de animações ou carregamentos visuais.
            await Task.Delay(100);

            // Reabilita a interação do usuário.
            viewModel.IsRunningInverse = true;
            // Finaliza a execução.
            viewModel.IsRunning = false;
        }
    }

    #endregion Started Methods

    #region Navigation Methods

    /// <summary>
    /// Intercepta o botão de voltar físico ou da barra do dispositivo, executando a navegação personalizada.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado manualmente.</returns>
    protected override bool OnBackButtonPressed() {
        // Chama o método que trata o clique no botão de voltar.
        BackButtom_Clicked(null, null);
        // Impede o comportamento padrão do botão voltar.
        return true;
    }

    /// <summary>
    /// Manipula o evento de clique no botão de voltar na barra de navegação personalizada.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (não utilizado).</param>
    /// <param name="e">Argumentos do evento (não utilizado).</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se a navegação de retorno é permitida.
        if(viewModel.CanBack()) {
            // Executa a navegação de retorno definida no ViewModel.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods

    #region Behavior Methods

    /// <summary>
    /// Alterna o estado do checkbox quando o texto ao lado dele é tocado.
    /// </summary>
    /// <param name="sender">Objeto que disparou o toque (Label).</param>
    /// <param name="e">Argumentos do evento de toque.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        // Inverte o estado atual do checkbox.
        checkBox.IsChecked = !checkBox.IsChecked;
    }

    #endregion Behavior Methods
}