using GeekShow.Core.Service;
using GeekShow.Shared.Component;
using Windows.ApplicationModel.Background;

namespace GeekShow.Tasks
{
    public sealed class UpdateShowsTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            
            var tvShowService = new TvMazeService();
            var persistManager = new TvShowPersistManager();
            
            var updated = await new TvShowUpdateManager(tvShowService, persistManager).UpdateTvShowsAsync();

            if(updated)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["UpdateSynched"] = false;
            }

            deferral.Complete();
        }
    }
}
