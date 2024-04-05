using System.ComponentModel;

namespace Lab4.ViewModels.Common;

public abstract class BaseBindable : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected bool UpdateProperty<T>(ref T property, T value, string propertyName)
    {
        if (EqualityComparer<T>.Default.Equals(property, value))
            return false;
        
        property = value;

        OnPropertyChanged(propertyName);

        return true;
    }
    
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
