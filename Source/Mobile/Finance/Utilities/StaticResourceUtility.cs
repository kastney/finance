namespace Finance;

internal static class StaticResourceUtility {

    public static T Get<T>(string resourceName) {
        try {
            var success = Application.Current.Resources.TryGetValue(resourceName, out var outValue);
            return success && outValue is T t ? t : default;
        } catch {
            return default;
        }
    }
}