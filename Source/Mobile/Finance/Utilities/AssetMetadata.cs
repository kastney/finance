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
    /// Configurações do serializador JSON, incluindo a opção de ignorar maiúsculas/minúsculas nos nomes das propriedades.
    /// </summary>
    private static JsonSerializerOptions JsonOptions { get; } = new() {
        // Configuração para ignorar maiúsculas/minúsculas nos nomes das propriedades durante a serialização/deserialização.
        PropertyNameCaseInsensitive = true
    };

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

    #region Strategy Methods

    /// <summary>
    /// Retorna uma estratégia padrão com um único grupo contendo todos os tipos de ativos disponíveis.
    /// Essa estrutura é usada por carteiras recém-criadas antes de qualquer personalização.
    /// </summary>
    /// <returns>Lista contendo o grupo padrão com os ativos suportados.</returns>
    public static List<AssetGroup> GetDefaultStrategy() => [];

    /// <summary>
    /// Tenta desserializar uma estratégia personalizada a partir de uma string JSON.
    /// Retorna a estratégia padrão caso a string esteja vazia ou inválida.
    /// </summary>
    /// <param name="json">String JSON contendo a estratégia a ser desserializada.</param>
    /// <returns>Lista de grupos de ativos representando a estratégia.</returns>
    public static List<AssetGroup> DeserializeStrategy(string json) {
        try {
            // Tenta desserializar a string JSON em uma lista de grupos de ativos.
            return string.IsNullOrWhiteSpace(json) ? GetDefaultStrategy() : JsonSerializer.Deserialize<List<AssetGroup>>(json, JsonOptions) ?? GetDefaultStrategy();
        } catch {
            // Em caso de erro na desserialização, retorna a estratégia padrão.
            return GetDefaultStrategy();
        }
    }

    /// <summary>
    /// Serializa uma lista de grupos de ativos em uma string JSON.
    /// </summary>
    /// <param name="strategy">Lista de grupos de ativos a serem serializados.</param>
    /// <returns>String JSON representando a lista de grupos de ativos.</returns>
    public static string SerializeStrategy(List<AssetGroup> strategy) {
        // Serializa a lista de grupos de ativos em uma string JSON.
        return JsonSerializer.Serialize(strategy, JsonOptions);
    }

    /// <summary>
    /// Retorna uma lista de ativos disponíveis para o usuário, incluindo título, cultura, bandeira e tokens de pesquisa.
    /// </summary>
    /// <returns>Lista de ativos disponíveis com metadados.</returns>
    public static AvailableAsset[] GetAvailableAssets() {
        return [
            // Ações Brasileiras.
            new AvailableAsset {
                Title = "Ações ",
                Locale = "Brasil ",
                Flag = "pt_br",
                Token = "acoes ação acao stock empresa negocios negócios brasileiros brasileiras brazilians",
                Type = AssetType.BRA_STOCK
            },
            // Fundos Imobiliários Brasileiros.
            new AvailableAsset {
                Title = "Fundo de Investimento Imobiliário ",
                Locale = "Brasil ",
                Flag = "pt_br",
                Subtitle = "FII ",
                Token = "fii imóveis imoveis imóvel imovel imobiliario brasileiros brasileiras brazilians",
                Type = AssetType.BRA_FII
            },
            // Brazilian Depositary Receipts.
            new AvailableAsset {
                Title = "Brazilian Depositary Receipts ",
                Locale = "Brasil ",
                Flag = "pt_br",
                Subtitle = "BDR ",
                Token = "bdr exterior ações acoes ação acao stock empresa negocios negócios brasileiros brasileiras brazilians",
                Type = AssetType.BRA_BDR
            }
        ];
    }

    #endregion Strategy Methods

    #region Allocation Methods

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
    /// Tenta desserializar alocações a partir de uma string JSON.
    /// Retorna as alocações padrão caso a string esteja vazia ou inválida.
    /// </summary>
    /// <param name="json">String JSON contendo as alocações a serem desserializadas.</param>
    /// <returns>Dicionário de alocações por tipo de ativo.</returns>
    public static Dictionary<AssetType, AssetAllocationData> DeserializeAllocations(string json) {
        try {
            // Tenta desserializar a string JSON em um dicionário de alocações.
            return string.IsNullOrWhiteSpace(json) ? GetDefaultAllocations() : JsonSerializer.Deserialize<Dictionary<AssetType, AssetAllocationData>>(json, JsonOptions) ?? GetDefaultAllocations();
        } catch {
            // Em caso de erro na desserialização, retorna as alocações padrão.
            return GetDefaultAllocations();
        }
    }

    /// <summary>
    /// Serializa um dicionário de alocações em uma string JSON.
    /// </summary>
    /// <param name="allocations">Dicionário de alocações a serem serializadas.</param>
    /// <returns>String JSON representando o dicionário de alocações.</returns>
    public static string SerializeAllocations(Dictionary<AssetType, AssetAllocationData> allocations) {
        // Serializa o dicionário de alocações em uma string JSON.
        return JsonSerializer.Serialize(allocations, JsonOptions);
    }

    #endregion Allocation Methods
}