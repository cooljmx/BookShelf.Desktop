using System;
using Autofac;
using BookShelf.Domain.Factories;

namespace BookShelf.Bootstrapper.Factories;

internal class Factory<TResult> : IFactory<TResult>
{
    private readonly IComponentContext _componentContext;

    public Factory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public TResult Create()
    {
        var factory = _componentContext.Resolve<Func<TResult>>();

        return factory.Invoke();
    }
}

internal class Factory<TParameter, TResult> : IFactory<TParameter, TResult>
{
    private readonly IComponentContext _componentContext;

    public Factory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public TResult Create(TParameter parameter)
    {
        var factory = _componentContext.Resolve<Func<TParameter, TResult>>();

        return factory.Invoke(parameter);
    }
}