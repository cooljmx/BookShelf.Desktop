using System;

namespace BookShelf.ViewModels.Authors;

public class AuthorCollectionItemViewModel
{
    public AuthorCollectionItemViewModel(string firstName, string lastName, DateOnly birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = $"({birthDate.ToShortDateString()})";
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string BirthDate { get; }
}