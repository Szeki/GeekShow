using GeekShow.Shared.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekShow.Shared.Model;

namespace GeekShow.Test.Mocks
{
    public class NotificationManagerMock : MockBase, INotificationManager
    {
        public void SendEpisodeReminderNotification(TvShowSubscribedItem tvShow)
        {
            RecordCall(nameof(INotificationManager.SendEpisodeReminderNotification), tvShow);

            ProcessMethodAction(nameof(INotificationManager.SendEpisodeReminderNotification));
        }
    }
}
