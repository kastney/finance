using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using Finance.Pages;
using Finance.Services;
using Finance.Validators.Objects;
using Finance.Validators.Rules;

namespace Finance.ViewModels;

internal partial class CreateWalletViewModel : ObservableObject {
    private readonly IWalletService walletService;
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private bool isRunning;

    public ValidatableObject<string> WalletName { get; }

    public CreateWalletViewModel() {
        walletService = Service.Get<IWalletService>();
        navigationService = Service.Get<INavigationService>();

        WalletName = new ValidatableObject<string>(nameof(WalletName), OnPropertyChanged);
        WalletName.Validations.Add(new IsNullOrEmptyRule { Message = "Este campo é obrigatório" });
        WalletName.Reset();
    }

    [RelayCommand]
    private async Task Create() {
        // Validação do formulário
        if(!WalletName.Validate()) {
            return;
        }

        // Verifica se a carteira já existe
        if(!walletService.Exists(WalletName.Value.Trim())) {
            WalletName.AddError("Nome da Carteira já está em uso, tente outro nome!");
            return;
        }

        IsRunning = true;

        // Cria a nova carteira
        var wallet = new Wallet { Id = Guid.NewGuid(), Name = WalletName.Value.Trim() };
        walletService.Create(wallet);
        // Define a nova carteira como a principal
        walletService.SetCurrent(wallet);

        // Ir para o dashboard, passando pelo loading
        await Task.Delay(500);        
        if(await navigationService.NavigateTo("///loading") is LoadingPage page) { page.Initialization(); }
        await navigationService.NavigateToBackModal();

        IsRunning = false;
    }

    public bool IsProcessing() {
        return IsRunning;
    }
}