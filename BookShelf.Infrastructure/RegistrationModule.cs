using Autofac;
using BookShelf.Domain.Settings;
using BookShelf.Infrastructure.Settings;

namespace BookShelf.Infrastructure
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindowMementoWrapper>()
                .As<IMainWindowMementoWrapper>()
                .As<IMainWindowMementoWrapperInitializer>()
                .SingleInstance();
        }
    }
}