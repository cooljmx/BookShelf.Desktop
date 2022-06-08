namespace BookShelf.Infrastructure.Settings;

internal class GenreWindowMemento : WindowMemento
{
    public GenreWindowMemento()
    {
        Left = 100;
        Top = 100;
        Width = 600;
        Height = 400;
        IsMaximized = false;
    }
}