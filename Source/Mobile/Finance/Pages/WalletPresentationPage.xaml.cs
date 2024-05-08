namespace Finance.Pages;

public partial class WalletPresentationPage : ContentPage {
    private readonly int count;
    private readonly IDispatcherTimer timer;

    public WalletPresentationPage() {
        InitializeComponent();

        BindingContext = Service.Get<WalletPresentationViewModel>();

        count = carouselView.ItemsSource.Cast<object>().Count();
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(10);
        timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object sender, EventArgs e) {
        carouselView.Position = (carouselView.Position + 1) % count;
    }

    private void CarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e) {
        timer.Stop();
        timer.Start();
    }
}