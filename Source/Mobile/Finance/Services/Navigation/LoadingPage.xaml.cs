namespace Finance.Services.Navigation;

/// <summary>
/// Representa a p�gina de carregamento exibida durante opera��es ass�ncronas.
/// Esta p�gina bloqueia a intera��o do usu�rio e oculta a barra de navega��o,
/// exibindo apenas um indicador de atividade centralizado.
/// </summary>
public partial class LoadingPage : ContentPage {

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia da classe <see cref="LoadingPage"/>.
    /// </summary>
    public LoadingPage() {
        // Inicializa os componentes definidos no XAML.
        InitializeComponent();
    }

    #endregion Constructor

    #region Navigation Methods

    /// <summary>
    /// Intercepta o bot�o de voltar e impede que o usu�rio saia da tela.
    /// </summary>
    /// <returns><c>true</c> para bloquear a navega��o de volta.</returns>
    protected override bool OnBackButtonPressed() {
        // Impede que o usu�rio volte da tela de carregamento.
        return true;
    }

    #endregion Navigation Methods
}