// -----------------------------------------------------------------------------
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
