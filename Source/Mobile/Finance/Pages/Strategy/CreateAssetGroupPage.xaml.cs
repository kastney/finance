using Finance.Utilities;

namespace Finance.Pages.Strategy;

/// <summary>
/// Página para criação de um novo grupo de ativos, responsável pela interface visual
/// e interação do usuário com a ViewModel correspondente.
/// </summary>
[QueryProperty(nameof(GroupName), "group")]
public partial class CreateAssetGroupPage : ContentPage {

    #region Fields

    /// <summary>
    /// Instância da ViewModel associada, que contém a lógica e dados da página.
    /// </summary>
    private readonly CreateAssetGroupViewModel viewModel;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Nome do grupo de ativos. <c>null</c> indica que a página será de criação; caso contrário a página será de edição.
    /// </summary>
    public string GroupName { get; set; }

    #endregion Properties

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
        // Verifica se a página está no mode de criação ou de edição;
        if(string.IsNullOrWhiteSpace(GroupName)) {
            // Informa na barra de navegação que é o mode de criação.
            navigationBar.Title = "Novo grupo de ativos";
        } else {
            // Informa na barra de navegação que é o mode de edição.
            navigationBar.Title = "Editar grupo de ativos";
            // Define o viewModel com o mode de Edição.
            viewModel.GroupName = GroupName;
            // Adiciona no viewModel o nome do grupo de ativos.
            viewModel.AssetGroupName.Value = GroupName;
        }

        // Pequena pausa para garantir que a interface esteja pronta para receber foco.
        await Task.Delay(100);
        // Define o foco no campo de entrada para o usuário digitar imediatamente.
        entry.Focus();
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