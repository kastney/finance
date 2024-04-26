using Finance.Pages;

namespace Finance;

public partial class App : Application {

    public App() {
        InitializeComponent();
        MainPage = new AppShell();
    }
}