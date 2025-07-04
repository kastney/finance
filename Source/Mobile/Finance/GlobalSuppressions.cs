﻿// -----------------------------------------------------------------------------
// Este arquivo é utilizado para registrar suprimendas (SuppressMessage) que
// desativam alertas específicos do analisador de código estático (Code Analysis).
// -----------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

// Suprime o aviso MVVMTK0045 relacionado à compatibilidade AOT (Ahead-of-Time)
// do .NET Community Toolkit. Neste projeto MAUI, essa compatibilidade não é
// necessária pois o alvo principal é Windows.
[assembly: SuppressMessage("Toolkit", "MVVMTK0045", Justification = "AOT compatibility not required for this MAUI app targeting Windows", Scope = "module")]

// Suprime o aviso CsWinRT1030 emitido pelo analisador CsWinRT, referente à
// interoperabilidade com o Windows Runtime. A classe afetada não é utilizada
// diretamente em cenários de interoperabilidade com WinRT.
[assembly: SuppressMessage("CsWinRT", "CsWinRT1030", Justification = "Classe não usada em interoperabilidade direta com WinRT.", Scope = "module")]

// Suprime o aviso IDE0051 que indica membros privados não utilizados.
// Este membro é necessário para a lógica de verificação de estado do grupo de ativos,
// mas não é utilizado diretamente no código atual. A suprimenda é temporária
// e será revisada posteriormente para garantir que não há código morto.
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.StrategyPage.AssetGroupCell_PercentageChanged(System.String,System.Int32)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.StrategyPage.AssetGroupCell_CheckedChanged(System.String,System.Int32)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.StrategyPage.AssetGroupCell_ColorChanged(System.String,System.Int32)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.StrategyPage.AssetGroupCell_RenameClicked(System.String)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.StrategyPage.AssetGroupCell_DeleteClicked(System.String)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.StrategyPage.AssetGroupCell_OpenGroupClicked(System.String)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.AssetGroupPage.AssetTypeCell_CheckedChanged(System.String,System.Int32)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.AssetGroupPage.AssetTypeCell_PercentageChanged(System.String,System.Int32)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.AssetGroupPage.AssetTypeCell_ColorChanged(System.String,System.Int32)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remover membros privados não utilizados", Justification = "<Pendente>", Scope = "member", Target = "~M:Finance.Pages.Strategy.AssetGroupPage.AssetTypeCell_DeleteClicked(System.String)")]

// Suprime o aviso CsWinRT1028 que recomenda que a classe seja marcada como `partial`
// para facilitar a interoperabilidade e geração de código com o WinRT. Como a classe
// `AssetGroup` não será exposta ou usada diretamente nesse contexto, a marcação como
// `partial` é desnecessária neste momento. A suprimenda é temporária e será revisada.
[assembly: SuppressMessage("Usage", "CsWinRT1028:Class is not marked partial", Justification = "<Pendente>", Scope = "type", Target = "~T:Finance.Models.AssetGroup")]
[assembly: SuppressMessage("Usage", "CsWinRT1028:Class is not marked partial", Justification = "<Pendente>", Scope = "type", Target = "~T:Finance.Models.AssetAllocation")]
