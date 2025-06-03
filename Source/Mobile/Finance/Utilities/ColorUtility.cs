namespace Finance.Utilities;

/// <summary>
/// Classe utilitária para operações relacionadas a cores.
/// </summary>
internal static class ColorUtility {

    /// <summary>
    /// Lista de grupos de cores, onde cada grupo contém variações de uma mesma cor principal.
    /// </summary>
    private static readonly List<List<string>> ColorGroups = [
        // Grupo Vermelho com quatro variações de tons.
        ["#E6726A", "#CC655E", "#994C46", "#66332F"],
        // Grupo Verde com quatro variações de tons.
        ["#6AE6A1", "#268F57", "#27553C", "#56BB83"],
        // Grupo Azul com quatro variações de tons.
        ["#6ABCE6", "#5292B3", "#3B6980", "#5EA7CC"]
    ];

    /// <summary>
    /// Obtém uma lista contendo a cor principal de cada grupo de cores.
    /// </summary>
    /// <returns>Lista de objetos Color representando a cor principal de cada grupo.</returns>
    public static List<Color> GetColors() {
        // Cria uma nova lista para armazenar as cores principais de cada grupo.
        var colors = new List<Color>();
        // Itera sobre cada grupo de cores definido na lista ColorGroups.
        foreach(var group in ColorGroups) {
            // Verifica se o grupo possui pelo menos uma cor definida.
            if(group.Count > 0) {
                // Adiciona a primeira cor do grupo (cor principal) convertida para o tipo Color.
                colors.Add(Color.FromArgb(group[0]));
            }
        }
        // Retorna a lista contendo as cores principais de todos os grupos.
        return colors;
    }
}