using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Background;

namespace GeekShow.Tasks
{
    public sealed class NotifyShowsTask : IBackgroundTask
    {
        static NotifyShowsTask()
        {
            IoC.Container.RegisterType<ITvShowEpisodeService, TvShowEpisodeImdbService>();
            IoC.Container.RegisterType<ITvShowPersistManager, TvShowPersistManager>();
            IoC.Container.RegisterType<INotificationManager, NotificationManager>();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var persistManager = IoC.Container.ResolveType<ITvShowPersistManager>();
            var notificationManager = IoC.Container.ResolveType<INotificationManager>();

            await new TvShowNotifier(persistManager, notificationManager).CalculateAndSendNotificationsAsync();

            deferral.Complete();
        }
    }
}
