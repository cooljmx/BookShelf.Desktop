using BookShelf.ViewModels.MainWindow;

namespace BookShelf.Views.AboutWindow;

public partial class AboutWindow : IAboutWindow
{
    public AboutWindow(IAboutWindowViewModel aboutWindowViewModel)
    {
        InitializeComponent();

        DataContext = aboutWindowViewModel;
    }
}