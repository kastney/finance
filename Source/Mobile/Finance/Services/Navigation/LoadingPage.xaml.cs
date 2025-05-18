namespace Finance.Services.Navigation;

/// <summary>
/// Representa a página de carregamento exibida durante operações assíncronas.
/// Esta página bloqueia a interação do usuário e oculta a barra de navegação,
/// exibindo apenas um indicador de atividade centralizado.
/// </summary>
public partial class LoadingPage : ContentPage {

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="LoadingPage"/>.
    /// </summary>
    public LoadingPage() {
        // Inicializa os componentes definidos no XAML.
        InitializeComponent();
    }

    #endregion Constructor

    #region Navigation Methods

    /// <summary>
    /// Intercepta o botão de voltar e impede que o usuário saia da tela.
    /// </summary>
    /// <returns><c>true</c> para bloquear a navegação de volta.</returns>
    protected override bool OnBackButtonPressed() {
        // Impede que o usuário volte da tela de carregamento.
        return true;
    }

    #endregion Navigation Methods
}