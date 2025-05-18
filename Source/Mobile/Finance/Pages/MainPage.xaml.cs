using Finance.Utilities;

namespace Finance.Pages;

public partial class MainPage : ContentPage {

    #region Fields

    private readonly MainViewModel viewModel;

    #endregion Fields

    #region Constructor

    public MainPage() {
        InitializeComponent();
        BindingContext = viewModel = Service.Get<MainViewModel>();
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
}