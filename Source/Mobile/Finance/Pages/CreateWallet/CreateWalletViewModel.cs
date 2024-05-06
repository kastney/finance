using CommunityToolkit.Mvvm.Input;
using Finance.Models;
using Finance.Validators.Objects;
using Finance.Validators.Rules;

namespace Finance.Pages.CreateWallet;

internal partial class CreateWalletViewModel : ViewModel {
    public ValidatableObject<string> WalletName { get; }

    public CreateWalletViewModel() {
        WalletName = new ValidatableObject<string>(nameof(WalletName), OnPropertyChanged);
        WalletName.Validations.Add(new IsNullOrEmptyRule { Message = "Este campo é obrigatório" });
        WalletName.Validations.Add(new IsStringRangeRule(5, 51) { Message = "É requerido no mínimo 5 caracteres" });
        WalletName.Reset();
    }

    [RelayCommand]
    private async Task Create() {
        // Validação do formulário
        WalletName.Value = WalletName.Value.Trim();
        if(!WalletName.Validate()) {
            return;
        }

        // Verifica se a carteira já existe
        if(!walletService.Exists(WalletName.Value)) {
            WalletName.AddError("Nome da Carteira já está em uso, tente outro nome!");
            return;
        }

        IsRunning = true;

        // Cria a nova carteira
        var wallet = new Wallet { Id = Guid.NewGuid(), Name = WalletName.Value };
        walletService.Create(wallet);
        // Define a nova carteira como a principal
        walletService.SetWallet(wallet);

        // Ir para o dashboard, passando pelo loading
        await Task.Delay(250);
        if(await navigationService.NavigateTo("///loading", false) is LoadingPage page) { page.Initialization(); }

        IsRunning = false;
    }
}

//[RelayCommand]
//private async Task DeleteWallet() {
//    IsRunning = true;
//
//    if(await navigationService.NavigateToModal<DeleteWalletPage>() is DeleteWalletPage page) { ((DeleteWalletViewModel)page.BindingContext).Wallet = Wallet; }
//    await Task.Delay(1000);
//
//    IsRunning = false;
//}