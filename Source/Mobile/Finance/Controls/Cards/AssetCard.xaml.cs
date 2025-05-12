namespace Finance.Controls.Cards;

/// <summary>
/// Representa um cart�o visual com informa��es resumidas de um ativo, como nome, cor, valor, varia��o, desempenho e cultura.
/// </summary>
public partial class AssetCard : ContentView {

    #region Fields

    /// <summary>
    /// Propriedade vincul�vel que define o t�tulo do cart�o.
    /// </summary>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(AssetCard), string.Empty, propertyChanged: OnTitleChanged);

    /// <summary>
    /// Propriedade vincul�vel que define a cor associada ao ativo.
    /// </summary>
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(AssetCard), default, propertyChanged: OnColorChanged);

    /// <summary>
    /// Propriedade vincul�vel que define a quantidade de ativos.
    /// </summary>
    public static readonly BindableProperty AssetCountProperty = BindableProperty.Create(nameof(AssetCount), typeof(int), typeof(AssetCard), -1, propertyChanged: OnAssetCountChanged);

    /// <summary>
    /// Propriedade vincul�vel que define o pre�o atual do ativo.
    /// </summary>
    public static readonly BindableProperty PriceProperty = BindableProperty.Create(nameof(Price), typeof(float?), typeof(AssetCard), null, propertyChanged: OnPriceChanged);

    /// <summary>
    /// Propriedade vincul�vel que define a varia��o percentual do ativo.
    /// </summary>
    public static readonly BindableProperty VariationProperty = BindableProperty.Create(nameof(Variation), typeof(float?), typeof(AssetCard), null, propertyChanged: OnVariationChanged);

    /// <summary>
    /// Propriedade vincul�vel que define o desempenho percentual do ativo.
    /// </summary>
    public static readonly BindableProperty PerformanceProperty = BindableProperty.Create(nameof(Performance), typeof(float?), typeof(AssetCard), null, propertyChanged: OnPerformanceChanged);

    /// <summary>
    /// Propriedade vincul�vel que define a cultura usada para formatar os valores monet�rios.
    /// </summary>
    public static readonly BindableProperty CultureProperty = BindableProperty.Create(nameof(Culture), typeof(string), typeof(AssetCard), default(string), propertyChanged: OnCultureChanged);

    /// <summary>
    /// Propriedade vincul�vel que define o c�digo da bandeira (flag) do pa�s relacionado ao ativo.
    /// </summary>
    public static readonly BindableProperty FlagProperty = BindableProperty.Create(nameof(Flag), typeof(string), typeof(AssetCard), default(string), propertyChanged: OnFlagChanged);

    #endregion Fields

    #region Properties

    /// <summary>
    /// Obt�m ou define o t�tulo do cart�o.
    /// </summary>
    public string Title {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a cor do cart�o.
    /// </summary>
    public Color Color {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a quantidade de ativos.
    /// </summary>
    public int AssetCount {
        get => (int)GetValue(AssetCountProperty);
        set => SetValue(AssetCountProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o pre�o do ativo.
    /// </summary>
    public float? Price {
        get => (float?)GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    /// <summary>
    /// Obt�m ou define a varia��o percentual do ativo.
    /// </summary>
    public float? Variation {
        get => (float?)GetValue(VariationProperty);
        set => SetValue(VariationProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o desempenho percentual do ativo.
    /// </summary>
    public float? Performance {
        get => (float?)GetValue(PerformanceProperty);
        set => SetValue(PerformanceProperty, value);
    }

    /// <summary>
    /// Obt�m ou define o nome da cultura usada para formata��o.
    /// </summary>
    public string Culture {
        get => (string)GetValue(CultureProperty);
        set => SetValue(CultureProperty, value);
    }

    /// <summary>
    /// Obt�m o objeto CultureInfo com base na propriedade Culture.
    /// </summary>
    private CultureInfo ParsedCulture => new(Culture ?? "en-us");

    /// <summary>
    /// Obt�m ou define o c�digo da bandeira do pa�s.
    /// </summary>
    public string Flag {
        get => (string)GetValue(FlagProperty);
        set => SetValue(FlagProperty, value);
    }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova inst�ncia da classe <see cref="AssetCard"/>.
    /// </summary>
    public AssetCard() {
        // Inicializa os componentes visuais definidos no XAML.
        InitializeComponent();
        // Registra o evento de mudan�a de tema do aplicativo para atualizar a apar�ncia conforme o tema atual.
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    #endregion Constructor

    #region Property Methods

    /// <summary>
    /// Atualiza o t�tulo exibido no cart�o de ativo.
    /// </summary>
    /// <param name="bindable">Inst�ncia do objeto associado � propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � um AssetCard v�lido.
        if(bindable is not AssetCard control) { return; }
        // Define o novo t�tulo no controle.
        control.title.Text = newValue.ToString();
    }

    /// <summary>
    /// Atualiza a cor do indicador visual do ativo.
    /// </summary>
    /// <param name="bindable">Inst�ncia do objeto associado � propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � um AssetCard e se a nova cor � v�lida.
        if(bindable is not AssetCard control || newValue is not Color color) { return; }
        // Aplica a nova cor ao indicador.
        control.color.Color = color;
    }

    /// <summary>
    /// Atualiza a quantidade de ativos exibida no cart�o.
    /// </summary>
    /// <param name="bindable">Inst�ncia do objeto associado � propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnAssetCountChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � um AssetCard v�lido.
        if(bindable is not AssetCard control) { return; }
        // Atualiza o texto da contagem de ativos.
        control.assetCount.Text = newValue.ToString();
    }

    /// <summary>
    /// Atualiza o pre�o do ativo exibido no cart�o, utilizando a cultura definida.
    /// </summary>
    /// <param name="bindable">Inst�ncia do objeto associado � propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnPriceChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � um AssetCard v�lido e se o pre�o � um float.
        if(bindable is not AssetCard control || newValue is not float price) { return; }
        // Formata e exibe o pre�o usando a cultura configurada.
        control.price.Text = price.ToString("C", control.ParsedCulture);
    }

    /// <summary>
    /// Atualiza a varia��o percentual do ativo e aplica a cor correspondente.
    /// </summary>
    /// <param name="bindable">Inst�ncia do objeto associado � propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnVariationChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � um AssetCard v�lido e se a varia��o � um float.
        if(bindable is not AssetCard control || newValue is not float variation) { return; }
        // Formata e exibe a varia��o percentual.
        control.variation.Text = (variation / 100).ToString("P2");
        // Aplica a cor conforme o valor da varia��o.
        ApplyColor(control.variation, variation, "Green", "Red", "TextColor");
    }

    /// <summary>
    /// Atualiza a performance percentual do ativo e aplica a cor correspondente.
    /// </summary>
    /// <param name="bindable">Inst�ncia do objeto associado � propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnPerformanceChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � um AssetCard v�lido e se a performance � um float.
        if(bindable is not AssetCard control || newValue is not float performance) { return; }
        // Formata e exibe a performance percentual.
        control.performance.Text = (performance / 100).ToString("P2");
        // Aplica a cor conforme o valor da performance.
        ApplyColor(control.performance, performance, "Green", "Red", "TextColor");
    }

    /// <summary>
    /// Atualiza a cultura usada para formatar o pre�o.
    /// </summary>
    /// <param name="bindable">Inst�ncia do objeto associado � propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnCultureChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � um AssetCard v�lido e se o pre�o est� definido.
        if(bindable is not AssetCard control || control.Price is not float price) { return; }
        // Reaplica a formata��o do pre�o com a nova cultura.
        control.price.Text = price.ToString("C", control.ParsedCulture);
    }

    /// <summary>
    /// Atualiza a bandeira exibida com base no c�digo cultural informado.
    /// </summary>
    /// <param name="bindable">Inst�ncia do objeto associado � propriedade.</param>
    /// <param name="oldValue">Valor anterior da propriedade.</param>
    /// <param name="newValue">Novo valor da propriedade.</param>
    private static void OnFlagChanged(BindableObject bindable, object oldValue, object newValue) {
        // Verifica se o objeto � um AssetCard v�lido e se o c�digo da cultura � v�lido.
        if(bindable is not AssetCard control || newValue is not string cultureCode || string.IsNullOrWhiteSpace(cultureCode)) { return; }
        // Normaliza o c�digo da cultura.
        var normalizedCode = cultureCode.Replace('-', '_').ToLowerInvariant();
        // Define o caminho da imagem da bandeira.
        var imagePath = $"{normalizedCode}.jpg";
        // Atualiza a imagem da bandeira no controle.
        control.flag.Source = ImageSource.FromFile(imagePath);
    }

    #endregion Property Methods

    #region Help Methods

    /// <summary>
    /// Atualiza as cores da varia��o e performance ao mudar o tema do sistema.
    /// </summary>
    /// <param name="sender">Objeto que disparou o evento.</param>
    /// <param name="e">Argumentos da mudan�a de tema.</param>
    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        // Aplica a cor da varia��o com base no tema atual, se houver valor definido.
        if(Variation.HasValue) {
            // Aplica a cor da varia��o com base no valor definido.
            ApplyColor(variation, Variation.Value, "Green", "Red", "TextColor");
        }

        // Aplica a cor da performance com base no tema atual, se houver valor definido.
        if(Performance.HasValue) {
            // Aplica a cor da performance com base no valor definido.
            ApplyColor(performance, Performance.Value, "Green", "Red", "TextColor");
        }
    }

    /// <summary>
    /// Aplica uma cor a um r�tulo (<see cref="Label"/>) com base no valor fornecido.
    /// </summary>
    /// <param name="label">R�tulo que receber� a cor.</param>
    /// <param name="value">Valor num�rico a ser avaliado.</param>
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
        // Caso contr�rio, aplica cor neutra conforme o tema.
        else {
            // Aplica a cor neutra com base no tema atual.
            label.TextColor = Application.Current.RequestedTheme == AppTheme.Light ? StaticResourceUtility.Get<Color>(neutralColorKey) : StaticResourceUtility.Get<Color>(neutralColorKey + "Dark");
        }
    }

    #endregion Help Methods
}