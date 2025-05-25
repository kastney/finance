using Finance.Utilities;
using Finance.Validators.Objects;
using Finance.Validators.Rules;

namespace Finance.Pages.Strategy;

/// <summary>
/// ViewModel responsável pela lógica e estado da página de criação de um novo grupo de ativos.
/// Contém validações, comandos e comunicação com serviços para gerenciar o processo.
/// </summary>
internal partial class CreateAssetGroupViewModel : ViewModel {

    #region Properties

    /// <summary>
    /// Propriedade validável que representa o nome do grupo de ativos a ser criado.
    /// Inclui regras de validação para garantir entrada correta do usuário.
    /// </summary>
    public ValidatableObject<string> AssetGroupName { get; }

    #endregion Properties

    #region Constructor

    /// <summary>
    /// Inicializa uma nova instância da ViewModel, configurando as validações da propriedade
    /// e preparando o estado inicial para o formulário de criação do grupo de ativos.
    /// </summary>
    public CreateAssetGroupViewModel() {
        // Cria a instância do objeto validável vinculada à propriedade AssetGroupName.
        AssetGroupName = new ValidatableObject<string>(nameof(AssetGroupName), OnPropertyChanged);
        // Adiciona a regra de validação para garantir que o campo não seja vazio.
        AssetGroupName.Validations.Add(new IsNullOrEmptyRule { Message = "Este campo é obrigatório" });
        // Adiciona a regra que valida o tamanho mínimo e máximo da string.
        AssetGroupName.Validations.Add(new IsStringRangeRule(3, 51) { Message = "É requerido no mínimo 3 caracteres" });
        // Reseta o estado da propriedade, limpando erros e valor inicial.
        AssetGroupName.Reset();
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Comando assíncrono responsável por criar um novo grupo de ativos após validação dos dados.
    /// Executa verificações, interage com o serviço de carteira e navega após o sucesso.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona do comando.</returns>
    [RelayCommand]
    private async Task Create() {
        // Remove espaços em branco nas extremidades da string para validação precisa.
        AssetGroupName.Value = (AssetGroupName.Value ?? string.Empty).Trim();
        // Se a validação falhar, interrompe a execução do comando.
        if(!AssetGroupName.Validate()) { return; }

        // Verifica se o nome do grupo já existe na carteira atual.
        if(walletService.Wallet.AssetGroupNameExists(AssetGroupName.Value)) {
            // Adiciona mensagem de erro para o usuário informar que o nome está em uso.
            AssetGroupName.AddError("Nome do Grupo de Ativos já está em uso, tente outro nome!");
            // Interrompe o fluxo para correção do nome.
            return;
        }

        // Sinaliza que a operação está em andamento para controle da interface.
        IsRunning = true;

        // Tenta adicionar o novo grupo ao serviço e navega para a página anterior em caso de sucesso.
        if(await walletService.AddAssetGroup(AssetGroupName.Value)) {
            // Retorna para a página anterior.
            await NavigationBack();
        }
        // Pequena pausa para garantir que todas operações assíncronas estejam concluídas.
        await Task.Delay(100);

        // Finaliza a operação, liberando controle para a interface.
        IsRunning = false;
    }

    #endregion Methods
}