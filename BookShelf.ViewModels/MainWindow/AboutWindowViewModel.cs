using BookShelf.Domain.Settings;

namespace BookShelf.ViewModels.MainWindow
{
    public class AboutWindowViewModel : WindowViewModel<IAboutWindowMementoWrapper>, IAboutWindowViewModel
    {
        public AboutWindowViewModel(IAboutWindowMementoWrapper windowMementoWrapper)
            : base(windowMementoWrapper)
        {
        }
    }
}