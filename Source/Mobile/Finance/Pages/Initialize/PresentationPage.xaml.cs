using Finance.Utilities;

namespace Finance.Pages.Initialize;

/// <summary>
/// Página de apresentação inicial do aplicativo, exibindo banners informativos em carrossel com rotação automática.
/// </summary>
public partial class PresentationPage : ContentPage {

    #region Fields

    /// <summary>
    /// Número total de itens exibidos no CarouselView.
    /// </summary>
    private readonly int count;

    /// <summary>
    /// Timer responsável por alternar automaticamente os itens do carrossel após um intervalo fixo.
    /// </summary>
    private readonly IDispatcherTimer timer;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Inicializa a interface de apresentação e configura o timer de rotação do carrossel.
    /// </summary>
    public PresentationPage() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();

        // Define o contexto de binding da página com o ViewModel correspondente.
        BindingContext = Service.Get<PresentationViewModel>();

        // Obtém a quantidade de itens no carrossel para controle de rotação.
        count = carouselView.ItemsSource.Cast<object>().Count();
        // Configura o evento de mudança de item atual do carrossel.
        timer = Dispatcher.CreateTimer();
        // Configura o intervalo do timer para 10 segundos.
        timer.Interval = TimeSpan.FromSeconds(10);
        // Adiciona o manipulador de eventos para quando o item atual do carrossel mudar.
        timer.Tick += Timer_Tick;
    }

    #endregion Constructor

    #region Timer Methods

    /// <summary>
    /// Manipulador chamado a cada tick do timer para atualizar a posição do CarouselView.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos do evento.</param>
    private void Timer_Tick(object sender, EventArgs e) {
        // Avança para o próximo item no carrossel, retornando ao início se for o último.
        carouselView.Position = (carouselView.Position + 1) % count;
    }

    /// <summary>
    /// Manipulador chamado sempre que o item atual do carrossel muda manualmente.
    /// Reinicia o timer para reiniciar o intervalo de rotação automática.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos com informações do item alterado.</param>
    private void CarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e) {
        // Para o timer para evitar que ele dispare enquanto o usuário navega manualmente.
        timer.Stop();
        // Reinicia o timer para o próximo intervalo de rotação automática.
        timer.Start();
    }

    #endregion Timer Methods
}