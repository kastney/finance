using Finance.Utilities;

namespace Finance.Pages.Initialize;

/// <summary>
/// P�gina de apresenta��o inicial do aplicativo, exibindo banners informativos em carrossel com rota��o autom�tica.
/// </summary>
public partial class PresentationPage : ContentPage {

    #region Fields

    /// <summary>
    /// N�mero total de itens exibidos no CarouselView.
    /// </summary>
    private readonly int count;

    /// <summary>
    /// Timer respons�vel por alternar automaticamente os itens do carrossel ap�s um intervalo fixo.
    /// </summary>
    private readonly IDispatcherTimer timer;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa a interface de apresenta��o e configura o timer de rota��o do carrossel.
    /// </summary>
    public PresentationPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();

        // Define o contexto de binding da p�gina com o ViewModel correspondente.
        BindingContext = Service.Get<PresentationViewModel>();

        // Obt�m a quantidade de itens no carrossel para controle de rota��o.
        count = carouselView.ItemsSource.Cast<object>().Count();
        // Configura o evento de mudan�a de item atual do carrossel.
        timer = Dispatcher.CreateTimer();
        // Configura o intervalo do timer para 10 segundos.
        timer.Interval = TimeSpan.FromSeconds(10);
        // Adiciona o manipulador de eventos para quando o item atual do carrossel mudar.
        timer.Tick += Timer_Tick;
    }

    #endregion Constructor

    #region Timer Methods

    /// <summary>
    /// Manipulador chamado a cada tick do timer para atualizar a posi��o do CarouselView.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento.</param>
    private void Timer_Tick(object sender, EventArgs e) {
        // Avan�a para o pr�ximo item no carrossel, retornando ao in�cio se for o �ltimo.
        carouselView.Position = (carouselView.Position + 1) % count;
    }

    /// <summary>
    /// Manipulador chamado sempre que o item atual do carrossel muda manualmente.
    /// Reinicia o timer para reiniciar o intervalo de rota��o autom�tica.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos com informa��es do item alterado.</param>
    private void CarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e) {
        // Para o timer para evitar que ele dispare enquanto o usu�rio navega manualmente.
        timer.Stop();
        // Reinicia o timer para o pr�ximo intervalo de rota��o autom�tica.
        timer.Start();
    }

    #endregion Timer Methods
}