namespace Finance.Controls.Cards;

/// <summary>
/// Representa um cartão visual com informações resumidas de um ativo, como nome, cor, valor, variação, desempenho e cultura.
/// </summary>
public partial class AssetCard : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vinculável que define o título do cartão.
    /// </summary>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(AssetCard), string.Empty, propertyChanged: OnTitleChanged);

    /// <summary>
    /// Propriedade vinculável que define a cor associada ao ativo.
    /// </summary>
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(AssetCard), default, propertyChanged: OnColorChanged);

    /// <summary>
    /// Propriedade vinculável que define a quantidade de ativos.
    /// </summary>
    public static readonly BindableProperty AssetCountProperty = BindableProperty.Create(nameof(AssetCount), typeof(int), typeof(AssetCard), -1, propertyChanged: OnAssetCountChanged);

    /// <summary>
    /// Propriedade vinculável que define o preço atual do ativo.
    /// </summary>
    public static readonly BindableProperty PriceProperty = BindableProperty.Create(nameof(Price), typeof(float?), typeof(AssetCard), null, propertyChanged: OnPriceChanged);

    /// <summary>
    /// Propriedade vinculável que define a variação percentual do ativo.
    /// </summary>
    public static readonly BindableProperty VariationProperty = BindableProperty.Create(nameof(Variation), typeof(float?), typeof(AssetCard), null, propertyChanged: OnVariationChanged);

    /// <summary>
    /// Propriedade vinculável que define o desempenho percentual do ativo.
    /// </summary>
    public static readonly BindableProperty PerformanceProperty = BindableProperty.Create(nameof(Performance), typeof(float?), typeof(AssetCard), null, propertyChanged: OnPerformanceChanged);

    /// <summary>
    /// Propriedade vinculável que define a cultura usada para formatar os valores monetários.
    /// </summary>
    public static readonly BindableProperty CultureProperty = BindableProperty.Create(nameof(Culture), typeof(string), typeof(AssetCard), default(string), propertyChanged: OnCultureChanged);

    /// <summary>
    /// Propriedade vinculável que define o código da bandeira (flag) do país relacionado ao ativo.
    /// </summary>
    public static readonly BindableProperty FlagProperty = BindableProperty.Create(nameof(Flag), typeof(string), typeof(AssetCard), default(string), propertyChanged: OnFlagChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obtém ou define o título do cartão.
    /// </summary>
    public string Title {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Obtém ou define a cor do cartão.
    /// </summary>
    public Color Color {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Obtém ou define a quantidade de ativos.
    /// </summary>
    public int AssetCount {
        get => (int)GetValue(AssetCountProperty);
        set => SetValue(AssetCountProperty, value);
    }

    /// <summary>
    /// Obtém ou define o preço do ativo.
    /// </summary>
    public float? Price {
        get => (float?)GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    /// <summary>
    /// Obtém ou define a variação percentual do ativo.
    /// </summary>
    public float? Variation {
        get => (float?)GetValue(VariationProperty);
        set => SetValue(VariationProperty, value);
    }

    /// <summary>
    /// Obtém ou define o desempenho percentual do ativo.
    /// </summary>
    public float? Performance {
        get => (float?)GetValue(PerformanceProperty);
        set => SetValue(PerformanceProperty, value);
    }

    /// <summary>
    /// Obtém ou define o nome da cultura usada para formatação.
    /// </summary>
    public string Culture {
        get => (string)GetValue(CultureProperty);
        set => SetValue(CultureProperty, value);
    }

    /// <summary>
    /// Obtém o objeto CultureInfo com base na propriedade Culture.
    /// </summary>
    private CultureInfo ParsedCulture => new(Culture ?? "en-us");

    /// <summary>
    /// Obtém ou define o código da bandeira do país.
    /// </summary>
    public string Flag {
        get => (string)GetValue(FlagProperty);
        set => SetValue(FlagProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="AssetCard"/>.
    /// </summary>
    public AssetCard() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Registra o evento de mudança de tema do aplicativo para atualizar a aparência conforme o tema atual.
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    #endregion Constructor

    #region Property Methods

    /// <summary>
    /// Atualiza o título exibido no cartão de ativo.
    /// </summary>
    /// <param name="bindable">Instância do objeto associado à propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto é um AssetCard válido.
        if(bindable is not AssetCard control) { return; }
        // Define o novo título no controle.
        control.title.Text = newValue.ToString();
    }

    /// <summary>
    /// Atualiza a cor do indicador visual do ativo.
    /// </summary>
    /// <param name="bindable">Instância do objeto associado à propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto é um AssetCard e se a nova cor é válida.
        if(bindable is not AssetCard control || newValue is not Color color) { return; }
        // Aplica a nova cor ao indicador.
        control.color.Color = color;
    }

    /// <summary>
    /// Atualiza a quantidade de ativos exibida no cartão.
    /// </summary>
    /// <param name="bindable">Instância do objeto associado à propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnAssetCountChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto é um AssetCard válido.
        if(bindable is not AssetCard control) { return; }
        // Atualiza o texto da contagem de ativos.
        control.assetCount.Text = newValue.ToString();
    }

    /// <summary>
    /// Atualiza o preço do ativo exibido no cartão, utilizando a cultura definida.
    /// </summary>
    /// <param name="bindable">Instância do objeto associado à propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnPriceChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto é um AssetCard válido e se o preço é um float.
        if(bindable is not AssetCard control || newValue is not float price) { return; }
        // Formata e exibe o preço usando a cultura configurada.
        control.price.Text = price.ToString("C", control.ParsedCulture);
    }

    /// <summary>
    /// Atualiza a variação percentual do ativo e aplica a cor correspondente.
    /// </summary>
    /// <param name="bindable">Instância do objeto associado à propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnVariationChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto é um AssetCard válido e se a variação é um float.
        if(bindable is not AssetCard control || newValue is not float variation) { return; }
        // Formata e exibe a variação percentual.
        control.variation.Text = (variation / 100).ToString("P2");
        // Aplica a cor conforme o valor da variação.
        ApplyColor(control.variation, variation, "Green", "Red", "TextColor");
    }

    /// <summary>
    /// Atualiza a performance percentual do ativo e aplica a cor correspondente.
    /// </summary>
    /// <param name="bindable">Instância do objeto associado à propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnPerformanceChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto é um AssetCard válido e se a performance é um float.
        if(bindable is not AssetCard control || newValue is not float performance) { return; }
        // Formata e exibe a performance percentual.
        control.performance.Text = (performance / 100).ToString("P2");
        // Aplica a cor conforme o valor da performance.
        ApplyColor(control.performance, performance, "Green", "Red", "TextColor");
    }

    /// <summary>
    /// Atualiza a cultura usada para formatar o preço.
    /// </summary>
    /// <param name="bindable">Instância do objeto associado à propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnCultureChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto é um AssetCard válido e se o preço está definido.
        if(bindable is not AssetCard control || control.Price is not float price) { return; }
        // Reaplica a formatação do preço com a nova cultura.
        control.price.Text = price.ToString("C", control.ParsedCulture);
    }

    /// <summary>
    /// Atualiza a bandeira exibida com base no código cultural informado.
    /// </summary>
    /// <param name="bindable">Instância do objeto associado à propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnFlagChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto é um AssetCard válido e se o código da cultura é válido.
        if(bindable is not AssetCard control || newValue is not string cultureCode || string.IsNullOrWhiteSpace(cultureCode)) { return; }
        // Normaliza o código da cultura.
        var normalizedCode = cultureCode.Replace('-', '_').ToLowerInvariant();
        // Define o caminho da imagem da bandeira.
        var imagePath = $"{normalizedCode}.jpg";
        // Atualiza a imagem da bandeira no controle.
        control.flag.Source = ImageSource.FromFile(imagePath);
    }

    #endregion Property Methods

    #region Help Methods

    /// <summary>
    /// Atualiza as cores da variação e performance ao mudar o tema do sistema.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos da mudança de tema.</param>
    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        // Aplica a cor da variação com base no tema atual, se houver valor definido.
        if(Variation.HasValue) {
            // Aplica a cor da variação com base no valor definido.
            ApplyColor(variation, Variation.Value, "Green", "Red", "TextColor");
        }

        // Aplica a cor da performance com base no tema atual, se houver valor definido.
        if(Performance.HasValue) {
            // Aplica a cor da performance com base no valor definido.
            ApplyColor(performance, Performance.Value, "Green", "Red", "TextColor");
        }
    }

    /// <summary>
    /// Aplica uma cor a um rótulo (<see cref="Label"/>) com base no valor fornecido.
    /// </summary>
    /// <param name="label">Rótulo que receberá a cor.</param>
    /// <param name="value">Valor numérico a ser avaliado.</param>
    /// <param name="positiveColorKey">Chave de cor para valores positivos.</param>
    /// <param name="negativeColorKey">Chave de cor para valores negativos.</param>
    /// <param name="neutralColorKey">Chave de cor para valores neutros.</param>
    private static void ApplyColor(Label label, float value, string positiveColorKey, string negativeColorKey, string neutralColorKey) {
        // Se valor positivo, aplica cor positiva conforme o tema.
        if(value > 0) {
            // Aplica a cor positiva com base no tema atual.
            label.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>(positiveColorKey) : StaticResourceUtility.Get<Color>(positiveColorKey + "Dark");
        }
        // Se valor negativo, aplica cor negativa conforme o tema.
        else if(value < 0) {
            // Aplica a cor negativa com base no tema atual.
            label.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>(negativeColorKey) : StaticResourceUtility.Get<Color>(negativeColorKey + "Dark");
        }
        // Caso contrário, aplica cor neutra conforme o tema.
        else {
            // Aplica a cor neutra com base no tema atual.
            label.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>(neutralColorKey) : StaticResourceUtility.Get<Color>(neutralColorKey + "Dark");
        }
    }

    #endregion Help Methods
}