using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookShelf.ViewModels;

public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void InvokePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void InvokeAllPropertiesChanged()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    public bool SetPropertyValue<T>(ref T valueStorage, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(valueStorage, value))
            return false;

        valueStorage = value;

        InvokePropertyChanged(propertyName);

        return true;
    }
}