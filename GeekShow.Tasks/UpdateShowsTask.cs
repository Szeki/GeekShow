using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using System;
using Windows.ApplicationModel.Background;

namespace GeekShow.Tasks
{
    public sealed class UpdateShowsTask : IBackgroundTask
    {
        static UpdateShowsTask()
        {
            IoC.Container.RegisterType<ITvShowEpisodeService, TvShowEpisodeImdbService>();
            IoC.Container.RegisterType<ITvShowPersistManager, TvShowPersistManager>();
            IoC.Container.RegisterType<ITvShowService, TvShowImdbService>();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            
            var tvShowService = IoC.Container.ResolveType<ITvShowService>();
            var persistManager = IoC.Container.ResolveType<ITvShowPersistManager>();
            
            var updated = await new TvShowUpdateManager(tvShowService, persistManager).UpdateTvShowsAsync();

            if(updated)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["UpdateSynched"] = false;
            }

            deferral.Complete();
        }
    }
}
