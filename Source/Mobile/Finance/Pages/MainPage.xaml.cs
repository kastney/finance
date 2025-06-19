using Finance.Utilities;

namespace Finance.Pages;

/// <summary>
/// Página principal da aplicação, responsável por exibir o conteúdo da carteira selecionada.
/// </summary>
public partial class MainPage : ContentPage {

    #region Fields

    /// <summary>
    /// ViewModel associado à página, responsável pela lógica e dados apresentados na interface.
    /// </summary>
    private readonly MainViewModel viewModel;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa a interface e define o contexto de binding com a ViewModel.
    /// </summary>
    public MainPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Obtém a instância da ViewModel a partir do serviço de injeção de dependência.
        BindingContext = viewModel = Service.Get<MainViewModel>();
    }

    #endregion Constructor

    #region Start Methods

    /// <summary>
    /// Evento acionado quando a página se torna visível. Controla a flag de execução para evitar chamadas duplicadas.
    /// </summary>
    protected override async void OnAppearing() {
        // Verifica se a execução já está em andamento.
        if(!viewModel.IsRunning) {
            // Marca como em execução.
            viewModel.IsRunning = true;

            // Aguarda um pequeno intervalo antes de prosseguir (simula carregamento inicial).
            await Task.Delay(100);

            // Marca como finalizado.
            viewModel.IsRunning = false;
        }
    }

    #endregion Start Methods

    #region Navigation Methods

    /// <summary>
    /// Manipula o evento de pressionar o botão "voltar" do sistema.
    /// </summary>
    /// <returns>Retorna verdadeiro para cancelar a navegação, falso para permitir.</returns>
    protected override bool OnBackButtonPressed() {
        // Cancela a navegação se a ViewModel indicar que não é permitido voltar.
        return !viewModel.CanBack();
    }

    #endregion Navigation Methods
}