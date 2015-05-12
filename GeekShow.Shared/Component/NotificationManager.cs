using GeekShow.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace GeekShow.Shared.Component
{
    public class NotificationManager
    {
        public void SendEpisodeReminderNotification(TvShowSubscribedItem tvShow)
        {
            var toastTemplate = ToastTemplateType.ToastText02;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            var textElements = toastXml.GetElementsByTagName("text");
            textElements[0].AppendChild(toastXml.CreateTextNode(string.Format("{0} episode today", tvShow.Name)));
            textElements[1].AppendChild(toastXml.CreateTextNode(tvShow.NextEpisodeDate.Value.ToLocalTime().ToString("MMM/dd/yyyy h:mm tt")));

            var toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
