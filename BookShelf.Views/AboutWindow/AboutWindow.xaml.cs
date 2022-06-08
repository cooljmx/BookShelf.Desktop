using BookShelf.ViewModels.MainWindow;

namespace BookShelf.Views.AboutWindow;

public partial class AboutWindow
{
    public AboutWindow(IAboutWindowViewModel aboutWindowViewModel)
        : base(aboutWindowViewModel)
    {
        InitializeComponent();
    }
}