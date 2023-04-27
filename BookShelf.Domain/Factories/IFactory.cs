namespace BookShelf.Domain.Factories;

public interface IFactory<out TResult>
{
    TResult Create();
}