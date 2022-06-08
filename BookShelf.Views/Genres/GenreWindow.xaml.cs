using BookShelf.ViewModels.Genres;

namespace BookShelf.Views.Genres;

public partial class GenreWindow
{
    public GenreWindow(IGenreWindowViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}