namespace Finance.Pages;

public partial class MainPage : ContentPage {

    #region Fields

    private readonly MainViewModel viewModel;

    #endregion Fields

    #region Constructor

    public MainPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<MainViewModel>();
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        Current_RequestedThemeChanged(null, null);
    }

    #endregion Constructor

    #region Started Methods

    internal void Initialization() {
        viewModel.Initialization();
    }

    protected override async void OnAppearing() {
        if(!viewModel.IsRunning) {
            viewModel.IsRunning = true;
            await Task.Delay(100);
            viewModel.IsRunning = false;
        }
    }

    #endregion Started Methods

    #region Navigation Methods

    protected override bool OnBackButtonPressed() {
        return !viewModel.CanBack();
    }

    #endregion Navigation Methods

    #region Theme Methods

    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e) {
        if(Application.Current.RequestedTheme == AppTheme.Light) {
            chartStyle.BackgroundColor = StaticResourceUtility.Get<Color>("Gray100");
            legendStyle.Color = labelStyle.Color = StaticResourceUtility.Get<Color>("Black");
        } else {
            chartStyle.BackgroundColor = StaticResourceUtility.Get<Color>("Gray600");
            legendStyle.Color = labelStyle.Color = StaticResourceUtility.Get<Color>("White");
        }
    }

    #endregion Theme Methods
}