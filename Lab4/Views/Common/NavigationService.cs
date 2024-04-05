using System.Windows.Navigation;

namespace Lab4.Views.Common;

public static class NavigationServiceExtension
{
    public static void HandleLoad(this NavigationService service, Action<object?> callback)
    {
        service.LoadCompleted += OnLoadCompleted;

        void OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            service.LoadCompleted -= OnLoadCompleted;
            
            callback.Invoke(e.ExtraData);
        }
    }
    
    public static bool NavigateSkipBackEntry(this NavigationService service, object root, object? navigationState = null)
    {
        return Navigate(service, root, navigationState, () => service.RemoveBackEntry());
    }
    
    public static bool Navigate(this NavigationService service, object root, object? navigationState = null, Action? callback = null)
    {
        bool result = service.Navigate(root, navigationState);
        if (callback != null)
            service.Navigated += OnNavigated;

        return result;
        
        void OnNavigated(object sender, NavigationEventArgs e)
        {
            service.Navigated -= OnNavigated;
            
            callback.Invoke();
        }
    }
}