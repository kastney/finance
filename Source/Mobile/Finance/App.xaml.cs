namespace Finance;

/// <summary>
/// Classe principal do aplicativo responsável pela inicialização e configuração da aplicação.
/// </summary>
public partial class App : Application {

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da classe App, configurando os componentes da interface.
    /// </summary>
    public App() {
        // Inicializa os componentes definidos no arquivo XAML associado.
        InitializeComponent();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Cria a janela principal da aplicação quando o aplicativo é ativado.
    /// </summary>
    /// <param name="activationState">Estado de ativação do aplicativo fornecido pelo sistema.</param>
    /// <returns>Uma nova instância da janela principal contendo o AppShell.</returns>
    protected override Window CreateWindow(IActivationState activationState) {
        // Retorna uma nova janela contendo o layout principal (AppShell) da aplicação.
        return new Window(new AppShell());
    }

    #endregion Methods
}