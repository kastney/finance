namespace Finance;

internal static class Service {

    public static IServiceProvider Current {
        get {
#if ANDROID
            return IPlatformApplication.Current.Services;
#else
            return null;
#endif
        }
    }

    public static TService Get<TService>() {
        return Current.GetService<TService>();
    }
}