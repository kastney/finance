using Finance.Models;
using Finance.Pages.Initialize;
using Finance.Utilities;
using Finance.Validators.Objects;
using Finance.Validators.Rules;

namespace Finance.Pages.Walleting;

/// <summary>
/// ViewModel responsável por controlar a lógica de criação de uma nova carteira,
/// incluindo validação do nome, verificação de existência e navegação.
/// </summary>
internal partial class CreateWalletViewModel : ViewModel {

    #region Properties

    /// <summary>
    /// Representa o nome da carteira inserido pelo usuário e contém regras de validação.
    /// </summary>
    public ValidatableObject<string> WalletName { get; }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância do ViewModel de criação de carteira.
    /// </summary>
    public CreateWalletViewModel() {
        // Inicializa o campo WalletName, associando o método de notificação de mudança de propriedade.
        WalletName = new ValidatableObject<string>(nameof(WalletName), OnPropertyChanged);
        // Adiciona regra de validação para verificar se o campo está vazio ou nulo.
        WalletName.Validations.Add(new IsNullOrEmptyRule { Message = "Este campo é obrigatório" });
        // Adiciona regra de validação para verificar o comprimento mínimo da string.
        WalletName.Validations.Add(new IsStringRangeRule(5, 51) { Message = "É requerido no mínimo 5 caracteres" });
        // Reinicia o estado do campo (valor e erros).
        WalletName.Reset();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Comando responsável por criar uma nova carteira após validações.
    /// </summary>
    /// <returns>Task assíncrona da operação.</returns>
    [RelayCommand]
    private async Task Create() {
        // Remove espaços em branco nas extremidades do nome da carteira.
        WalletName.Value = (WalletName.Value ?? string.Empty).Trim();
        // Executa as validações definidas no campo WalletName.
        if(!WalletName.Validate()) { return; }

        // Verifica se já existe uma carteira com o mesmo nome.
        if(await walletService.Exists(WalletName.Value)) {
            // Exibe mensagem de erro ao usuário indicando nome duplicado.
            WalletName.AddError("Nome da Carteira já está em uso, tente outro nome!");
            return;
        }

        // Ativa o estado de carregamento para bloquear a interface.
        IsRunning = true;

        // Cria uma nova instância da carteira com o nome fornecido.
        var wallet = new Wallet { Name = WalletName.Value };
        // Persiste a carteira no serviço de armazenamento.
        await walletService.Create(wallet);
        // Define a carteira criada como a carteira atual do contexto.
        walletService.SetWallet(wallet);

        // Aguarda um breve momento para garantir transição visual.
        await Task.Delay(100);
        // Navega para a página de loading e inicia sua lógica de inicialização, se for bem-sucedido.
        if(await navigationService.NavigateTo("///loading", false) is LoadingPage page) { page.Initialization(); }

        // Finaliza o estado de carregamento permitindo novas ações.
        IsRunning = false;
    }

    #endregion Methods
}