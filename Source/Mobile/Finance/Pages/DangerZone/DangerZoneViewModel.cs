namespace Finance.Pages.DangerZone;

/// <summary>
/// ViewModel responsável pela lógica da página de Zona de Perigo, onde são realizadas ações sensíveis como deletar uma carteira.
/// </summary>
internal partial class DangerZoneViewModel : ViewModel {

    #region Methods

    /// <summary>
    /// Comando responsável por iniciar o fluxo de exclusão da carteira.
    /// </summary>
    /// <returns>Uma tarefa assíncrona que representa o processo de navegação para a tela de exclusão.</returns>
    [RelayCommand]
    private async Task DeleteWallet() {
        // Garante que a operação não seja executada mais de uma vez simultaneamente.
        if(!IsRunning) {
            // Define o estado como em execução.
            IsRunning = true;

            // Navega para a página de exclusão da carteira.
            await navigationService.NavigateTo("delete");
            // Aguarda um pequeno intervalo para permitir animações ou processamento.
            await Task.Delay(100);

            // Finaliza o estado de execução.
            IsRunning = false;
        }
    }

    #endregion Methods
}