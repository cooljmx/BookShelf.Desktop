using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using BookShelf.Domain.Factories;
using BookShelf.Domain.Genres;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.Genres;

public class GenreCollectionViewModel : ViewModel, IGenreCollectionViewModel
{
    private readonly Command _addCommand;
    private readonly IFactory<IGenre, IGenreCollectionItemViewModel> _genreCollectionItemViewModelFactory;
    private readonly IGenreRepository _genreRepository;
    private readonly IFactory<Guid, IGenreWindowViewModel> _genreWindowViewModelFactory;
    private readonly ObservableCollection<IGenreCollectionItemViewModel> _items;
    private readonly Command _modifyCommand;
    private readonly Command _removeCommand;
    private readonly IWindowManager _windowManager;
    private IGenreCollectionItemViewModel _selectedItem;

    public GenreCollectionViewModel(
        IGenreRepository genreRepository,
        IFactory<IGenre, IGenreCollectionItemViewModel> genreCollectionItemViewModelFactory,
        IFactory<Guid, IGenreWindowViewModel> genreWindowViewModelFactory,
        IWindowManager windowManager)
    {
        _genreRepository = genreRepository;
        _genreCollectionItemViewModelFactory = genreCollectionItemViewModelFactory;
        _genreWindowViewModelFactory = genreWindowViewModelFactory;
        _windowManager = windowManager;

        _items = new ObservableCollection<IGenreCollectionItemViewModel>();

        InitializeItems();

        _addCommand = new Command(Add);
        _modifyCommand = new Command(Modify, CanModify);
        _removeCommand = new Command(Remove, CanRemove);

        _genreRepository.CollectionChanged += OnGenreCollectionChanged;
    }

    public IEnumerable<IGenreCollectionItemViewModel> Items => _items;

    public IGenreCollectionItemViewModel SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;

            InvokePropertyChanged(nameof(SelectedItem));
            _modifyCommand.InvokeCanExecuteChanged();
            _removeCommand.InvokeCanExecuteChanged();
        }
    }

    public ICommand AddCommand => _addCommand;
    public ICommand ModifyCommand => _modifyCommand;
    public ICommand RemoveCommand => _removeCommand;

    private void OnGenreCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems != null)
                    foreach (var item in e.NewItems)
                        if (item is IGenre genre)
                            CreateGenreCollectionItemViewModel(genre);

                break;
            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems != null)
                    foreach (var item in e.OldItems)
                        if (item is IGenre genre)
                            RemoveGenreCollectionItemViewModel(genre.Id);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void RemoveGenreCollectionItemViewModel(Guid genreId)
    {
        for (var i = _items.Count - 1; i >= 0; i--)
        {
            var genreCollectionItemViewModel = _items[i];

            if (genreCollectionItemViewModel.Id == genreId
                && _items.Remove(genreCollectionItemViewModel))
                genreCollectionItemViewModel.Dispose();
        }
    }

    private void Add()
    {
        var genreWindowViewModel = _genreWindowViewModelFactory.Create(Guid.Empty);

        _windowManager.Show(genreWindowViewModel, true);
    }

    private bool CanRemove()
    {
        return SelectedItem != null;
    }

    private void Remove()
    {
        _genreRepository.Remove(SelectedItem.Id);
    }

    private bool CanModify()
    {
        return SelectedItem != null;
    }

    private void Modify()
    {
        var genreWindowViewModel = _genreWindowViewModelFactory.Create(SelectedItem.Id);

        _windowManager.Show(genreWindowViewModel, true);
    }

    private void InitializeItems()
    {
        foreach (var genre in _genreRepository.Items)
            CreateGenreCollectionItemViewModel(genre);
    }

    private void CreateGenreCollectionItemViewModel(IGenre genre)
    {
        var genreCollectionItemViewModel = _genreCollectionItemViewModelFactory.Create(genre);

        _items.Add(genreCollectionItemViewModel);
    }

    public void Dispose()
    {
        _genreRepository.CollectionChanged -= OnGenreCollectionChanged;

        var items = _items.ToArray();
        _items.Clear();

        foreach (var genreCollectionItemViewModel in items)
            genreCollectionItemViewModel.Dispose();
    }
}