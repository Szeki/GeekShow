using Autofac;
using GeekShow.Core.Component;
using GeekShow.Core.Service;
using GeekShow.Shared.Component;
using GeekShow.ViewModel;

namespace GeekShow.Component
{
    public static class IoC
    {
        static ILifetimeScope scope;

        public static void BuildContainer()
        {
            var container = new ContainerBuilder();
            
            container.RegisterType<NavigationService>().As<INavigationService>();
            container.RegisterType<TvShowPersistManager>().As<ITvShowPersistManager>();
            container.RegisterType<MessageBoxPopupService>().As<IPopupMessageService>();
            container.RegisterType<TvMazeService>().As<Core.Service.ITvShowService>();
            
            container.RegisterType<MainPageViewModel>().AsSelf();
            container.RegisterType<SearchTvShowViewModel>().AsSelf();
            container.RegisterType<ListMyTvShowViewModel>().AsSelf();
            container.RegisterType<TvShowSearchItemDetailsViewModel>().AsSelf();
            container.RegisterType<TvShowSubscribedItemDetailsViewModel>().AsSelf();
            container.RegisterType<EpisodeSummaryViewModel>().AsSelf();

            scope = container.Build().BeginLifetimeScope();
        }

        public static T Resolve<T>()
        {
            return scope.Resolve<T>();
        }
    }
}
