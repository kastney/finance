using Finance.Enumerations;
using Finance.Models;

namespace Finance.Utilities;

/// <summary>
/// Contém metadados e estratégias padrão para os tipos de ativos suportados pelo sistema.
/// Fornece dados como nome, cultura e ícone de bandeira, além de métodos auxiliares para inicialização.
/// </summary>
internal static class AssetMetadata {

    #region Properties

    /// <summary>
    /// Retorna um dicionário com os metadados de cada tipo de ativo, incluindo título, cultura e bandeira associada.
    /// Esses dados são utilizados para exibição amigável na interface do usuário.
    /// </summary>
    public static Dictionary<AssetType, AssetAllocationMeta> Data => new() {
        {
            // Ações brasileiras.
            AssetType.BRA_STOCK,
            new AssetAllocationMeta() {
                Title = "Ações",
                Culture = "pt-br",
                Flag = "pt-br"
            }
        },
        {
            // Fundos Imobiliários Brasileiros.
            AssetType.BRA_FII,
            new AssetAllocationMeta() {
                Title = "FIIs",
                Culture = "pt-br",
                Flag = "pt-br"
            }
        },
        {
            // Brazilian Depositary Receipts.
            AssetType.BRA_BDR,
            new AssetAllocationMeta() {
                Title = "BDRs",
                Culture = "pt-br",
                Flag = "pt-br"
            }
        }
    };

    #endregion Properties

    #region Methods

    /// <summary>
    /// Retorna um dicionário com alocações padrão (vazias) para cada tipo de ativo suportado.
    /// Esse método é utilizado para inicializar carteiras que ainda não possuem dados persistidos.
    /// </summary>
    /// <returns>Dicionário de alocações padrão por tipo de ativo.</returns>
    public static Dictionary<AssetType, AssetAllocationData> GetDefaultAllocations() => new() {
        {
            // Inicializa dados de alocação de ações.
            AssetType.BRA_STOCK,
            new()
        },
        {
            // Inicializa dados de alocação de FIIs.
            AssetType.BRA_FII,
            new()
        },
        {
            // Inicializa dados de alocação de BDRs.
            AssetType.BRA_BDR,
            new()
        }
    };

    /// <summary>
    /// Retorna uma estratégia padrão com um único grupo contendo todos os tipos de ativos disponíveis.
    /// Essa estrutura é usada por carteiras recém-criadas antes de qualquer personalização.
    /// </summary>
    /// <returns>Lista contendo o grupo padrão com os ativos suportados.</returns>
    public static List<AssetGroup> GetDefaultStrategy() => [
       new() {
            Name = "Lista de Ativos",
            Assets = [
                new() { Type = AssetType.BRA_STOCK },
                new() { Type = AssetType.BRA_FII },
                new() { Type = AssetType.BRA_BDR }
            ]
        }
    ];

    #endregion Methods
}