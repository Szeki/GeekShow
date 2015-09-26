using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Background;

namespace GeekShow.Tasks
{
    public sealed class NotifyShowsTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var persistManager = new TvShowPersistManager();
            var notificationManager = new NotificationManager();

            new TvShowNotifier(persistManager, notificationManager).CalculateAndSendNotifications();

            deferral.Complete();
        }
    }
}
