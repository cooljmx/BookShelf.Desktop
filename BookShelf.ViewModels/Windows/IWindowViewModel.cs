namespace BookShelf.ViewModels.Windows;

public interface IWindowViewModel
{
    void BeforeWindowShown();
    void AfterWindowClosed();
}