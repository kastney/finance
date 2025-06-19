namespace Finance.Utilities;

/// <summary>
/// Classe utilitária para operações relacionadas a cores.
/// </summary>
internal static class ColorUtility {

    /// <summary>
    /// Lista de grupos de cores, onde cada grupo contém variações de uma mesma cor principal.
    /// </summary>
    private static readonly List<List<string>> ColorGroups = [
        // Grupo Vermelho.
        ["#ffa183", "#ff805b", "#ff5b34", "#ff4326", "#d12508", "#a4240c", "#7a200d"],
        // Grupo Amarelo.
        ["#ffea98", "#ffe474", "#ffdd4c", "#ffd700", "#d0af12", "#a38a17", "#786518"],
        // Grupo Verde.
        ["#99c58f", "#77b26b", "#529e48", "#228b22", "#22731f", "#1f5b1c", "#1c4518"],
        // Grupo Ciano.
        ["#96c4c3", "#72b1b0", "#499e9d", "#008b8b", "#117372", "#165b5b", "#174444"],
        // Grupo Azul.
        ["#73B9FF", "#4DA6FF", "#2693FF", "#007fff", "#1b69d0", "#2153a2", "#213f77"],
        // Grupo Roxo.
        ["#c3b1f8", "#ad98f5", "#9680f2", "#7b68ee", "#6756c2", "#534598", "#3f3570"],
        // Grupo Margenta.
        ["#ffa5ff", "#ff83ff", "#ff5aff", "#ff00ff", "#d018d0", "#a31ea2", "#781e77"],

        // Grupo Cinza.
        ["#b9c2cb", "#a3aeba", "#8d9ba9", "#778899", "#62707e", "#4f5963", "#292e33"]
    ];

    /// <summary>
    /// Obtém uma lista contendo a cor principal de cada grupo de cores.
    /// </summary>
    /// <returns>Lista de objetos Color representando a cor principal de cada grupo.</returns>
    internal static List<Color> GetColors() {
        // Cria uma nova lista para armazenar as cores principais de cada grupo.
        var colors = new List<Color>();
        // Itera sobre cada grupo de cores definido na lista ColorGroups.
        for(int i = 0; i < ColorGroups.Count - 1; i++) {
            // Verifica se o grupo possui pelo menos uma cor definida.
            if(ColorGroups[i].Count > 0) {
                // Índice da cor primária;
                var index = ColorGroups.Count / 2;
                // Adiciona a primeira cor do grupo (cor principal) convertida para o tipo Color.
                colors.Add(Color.FromArgb(ColorGroups[i][index]));
            }
        }
        // Retorna a lista contendo as cores principais de todos os grupos.
        return colors;
    }

    /// <summary>
    /// Obtém uma lista contendo a cor principal de cada grupo de cores.
    /// </summary>
    /// <param name="gruopIndex">A cor do grupo de ativos.</param>
    /// <returns>Lista de objetos Color representando a cor principal de cada grupo.</returns>
    internal static List<Color> GetColors(int gruopIndex) {
        // Cria uma nova lista para armazenar as cores principais de cada grupo.
        var colors = new List<Color>();
        // Itera sobre cada grupo de cores definido na lista ColorGroups.
        foreach(var group in ColorGroups[gruopIndex]) {
            // Adiciona a primeira cor do grupo (cor principal) convertida para o tipo Color.
            colors.Add(Color.FromArgb(group));
        }
        // Retorna a lista contendo as cores principais de todos os grupos.
        return colors;
    }

    /// <summary>
    /// Obtém a cor padrão associada a um grupo de ativos, com base no índice do grupo.
    /// </summary>
    /// <param name="groupIndex">O índice do grupo de ativos. Se for -1 ou inválido, retorna uma cor padrão.</param>
    /// <param name="assetIndex">O índice do tipo de ativo.</param>
    /// <returns>A cor primária do grupo correspondente, ou a cor padrão cinza ("#333333") se o índice for inválido.</returns>
    internal static Color GetColor(int groupIndex, int assetIndex = -1) {
        // Se o índice for inválido (menor que 0 ou maior ou igual ao número de grupos), retorna a cor padrão.
        if(groupIndex < 0 || groupIndex >= ColorGroups.Count) {
            // Cor padrão cinza.
            return Color.FromArgb("#44000000");
        }

        // Obtém o grupo de cores correspondente ao índice.
        var group = ColorGroups[groupIndex];

        // Verifica se o índice do ativo foi informado.
        if(assetIndex == -1) {
            // Índice da cor primária;
            assetIndex = group.Count / 2;
        }

        // Se o índice for inválido (menor que 0 ou maior ou igual ao número de grupos), retorna a cor padrão.
        if(assetIndex < 0 || assetIndex >= group.Count) {
            // Cor padrão cinza.
            return Color.FromArgb("#44000000");
        }

        // Retorna a primeira cor do grupo.
        return Color.FromArgb(group[assetIndex]);
    }

    /// <summary>
    /// Obtém a cor padrão do grupo.
    /// </summary>
    /// <returns>O índice do grupo de cores.</returns>
    internal static int DefaultGruopColor() {
        // Obtém o último índice do grupo.
        return ColorGroups.Count - 1;
    }
}