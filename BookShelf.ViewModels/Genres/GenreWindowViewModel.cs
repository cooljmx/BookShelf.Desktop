using System;
using System.Windows.Input;
using BookShelf.Domain.Genres;
using BookShelf.Domain.Settings;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.Genres;

public class GenreWindowViewModel : WindowViewModel<IGenreWindowMementoWrapper>,
    IGenreWindowViewModel,
    IGenreData
{
    private readonly Command _cancelCommand;
    private readonly IGenre _genre;
    private readonly IGenreRepository _genreRepository;
    private readonly bool _isNewGenre;
    private readonly Command _saveCommand;
    private readonly IWindowManager _windowManager;
    private string _description;
    private string _name;

    public GenreWindowViewModel(
        Guid genreId,
        IGenreWindowMementoWrapper genreWindowMementoWrapper,
        IGenreRepository genreRepository,
        IGenreFactory genreFactory,
        IWindowManager windowManager)
        : base(genreWindowMementoWrapper)
    {
        _genreRepository = genreRepository;
        _windowManager = windowManager;
        _isNewGenre = genreId == Guid.Empty;

        _genre = _isNewGenre
            ? genreFactory.Create()
            : _genreRepository.Get(genreId);

        _saveCommand = new Command(Save, CanSave);
        _cancelCommand = new Command(Cancel);

        Id = _genre.Id;
        Name = _genre.Name;
        Description = _genre.Description;
    }

    public string Title => "Genre editor";
    public ICommand SaveCommand => _saveCommand;
    public ICommand CancelCommand => _cancelCommand;

    public Guid Id { get; }

    public string Name
    {
        get => _name;
        set
        {
            if (SetPropertyValue(ref _name, value))
                _saveCommand.InvokeCanExecuteChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (SetPropertyValue(ref _description, value))
                _saveCommand.InvokeCanExecuteChanged();
        }
    }

    private void Cancel()
    {
        _windowManager.Close(this);
    }

    private bool CanSave()
    {
        return !string.Equals(_genre.Name, Name)
               || !string.Equals(_genre.Description, Description);
    }

    private void Save()
    {
        _genre.Update(this);

        if (_isNewGenre)
            _genreRepository.Add(_genre);

        _windowManager.Close(this);
    }
}