using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// Página para criação de um novo grupo de ativos, responsável pela interface visual
/// e interação do usuário com a ViewModel correspondente.
/// </summary>
public partial class CreateAssetGroupPage : ContentPage {

    #region Fields

    /// <summary>
    /// Instância da ViewModel associada, que contém a lógica e dados da página.
    /// </summary>
    private readonly CreateAssetGroupViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da página, configurando componentes visuais
    /// e definindo o BindingContext para a ViewModel obtida via injeção de dependência.
    /// </summary>
    public CreateAssetGroupPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Obtém a instância da ViewModel do serviço de injeção e define como contexto de dados.
        BindingContext = viewModel = Service.Get<CreateAssetGroupViewModel>();
    }

    #endregion Constructor

    #region Start Methods

    /// <summary>
    /// Método chamado quando a página está prestes a ser exibida.
    /// Executa operações de inicialização como foco em campo e controle de estado.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se não está em execução outro processo para evitar duplicidade.
        if(!viewModel.IsRunning) {
            // Define a flag indicando que a operação está em execução.
            viewModel.IsRunning = true;

            // Pequena pausa para garantir que a interface esteja pronta para receber foco.
            await Task.Delay(100);
            // Define o foco no campo de entrada para o usuário digitar imediatamente.
            entry.Focus();

            // Reset da flag indicando fim da operação.
            viewModel.IsRunning = false;
        }
    }

    #endregion Start Methods

    #region Navigation Methods

    /// <summary>
    /// Sobrescreve o comportamento do botão de voltar do dispositivo.
    /// Redireciona para o método customizado de navegação de retorno.
    /// </summary>
    /// <returns>Retorna true para indicar que o evento foi tratado.</returns>
    protected override bool OnBackButtonPressed() {
        // Invoca o método de clique no botão voltar, simulando a ação do usuário.
        BackButtom_Clicked(null, null);
        // Indica que o evento foi tratado para evitar comportamento padrão.
        return true;
    }

    /// <summary>
    /// Evento disparado ao clicar no botão voltar da barra de navegação.
    /// Executa a navegação para a página anterior se permitido pela ViewModel.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento (botão).</param>
    /// <param name="e">Argumentos do evento.</param>
    private async void BackButtom_Clicked(object sender, EventArgs e) {
        // Verifica se a ViewModel permite o retorno (validação ou confirmação).
        if(viewModel.CanBack()) {
            // Executa a navegação assíncrona para a página anterior.
            await viewModel.NavigationBack();
        }
    }

    #endregion Navigation Methods
}