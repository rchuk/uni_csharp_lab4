using System.Windows;

namespace Lab4.ViewModels.Common;

public class BaseServiceViewModel : BaseBindable
{
    protected void TryRunBackgroundDisplayError(Func<Task> func, Action? callback = null)
    {
        TryRunBackground(
            func,
            exception => DisplayError(exception.Message),
            callback
        );
    }
    
    protected void TryRunBackground(Func<Task> func, Action<Exception> errorHandler, Action? callback = null)
    {
        RunBackground(async () =>
        {
            try
            {
                await func.Invoke();
                if (callback != null)
                    RunOnUI(callback);
            }
            catch (Exception e)
            {
                RunOnUI(() => errorHandler.Invoke(e));
            }
        });
    }

    protected void RunBackground(Func<Task> func)
    {
        Task.Run(func);
    }
    
    protected void RunOnUI(Action callback)
    {
        Application.Current.Dispatcher.Invoke(callback);
    }

    protected void DisplayError(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}