namespace BookShelf.Domain.Factories;

public interface IFactory<out TResult>
{
    TResult Create();
}

public interface IFactory<in TParameter, out TResult>
{
    TResult Create(TParameter parameter);
}