using GeekShow.Shared.Component;
using Windows.ApplicationModel.Background;

namespace GeekShow.Tasks
{
    public sealed class NotifyShowsTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var persistManager = new TvShowPersistManager();
            var notificationManager = new NotificationManager();

            await new TvShowNotifier(persistManager, notificationManager).CalculateAndSendNotificationsAsync();

            deferral.Complete();
        }
    }
}
