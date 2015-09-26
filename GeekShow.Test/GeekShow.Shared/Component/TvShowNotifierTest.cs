using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Test.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Test.GeekShow.Shared.Component
{
    [TestClass]
    public class TvShowNotifierTest : UnitTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            InitializeTestRun();
        }

        [TestMethod]
        public void CalculateAndSendNotifications_ShowWithEndDate_DoesNotGetNotified()
        {
            var persistManager = GetTvShowPersistManager();
            var notificationManager = GetNotificationManager();

            var tvShow = new TvShowSubscribedItem(1, "Lost") { EndDate = new DateTime(2010, 5, 23) };

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(new TvShowSubscribedItem[] { tvShow } as IEnumerable<TvShowSubscribedItem>));
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync), Task.FromResult(Enumerable.Empty<NotifiedTvShowItem>()));

            var notifier = new TvShowNotifier(persistManager, notificationManager);

            notifier.CalculateAndSendNotifications();

            Assert.AreEqual(0, notificationManager.GetNumberOfRecordedCalls(nameof(INotificationManager.SendEpisodeReminderNotification)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync)));

            var arguments = persistManager.GetFirstCallArguments(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            Assert.AreEqual(1, arguments.Length);
            Assert.AreEqual(0, (arguments[0] as IEnumerable<NotifiedTvShowItem>).Count());
        }

        [TestMethod]
        public void CalculateAndSendNotifications_ShowWithoutNextEpisodeDate_DoesNotGetNotified()
        {
            var persistManager = GetTvShowPersistManager();
            var notificationManager = GetNotificationManager();

            var tvShow = new TvShowSubscribedItem(1, "Lost") { NextEpisodeDate = null };

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(new TvShowSubscribedItem[] { tvShow } as IEnumerable<TvShowSubscribedItem>));
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync), Task.FromResult(Enumerable.Empty<NotifiedTvShowItem>()));

            var notifier = new TvShowNotifier(persistManager, notificationManager);

            notifier.CalculateAndSendNotifications();

            Assert.AreEqual(0, notificationManager.GetNumberOfRecordedCalls(nameof(INotificationManager.SendEpisodeReminderNotification)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync)));

            var arguments = persistManager.GetFirstCallArguments(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            Assert.AreEqual(1, arguments.Length);
            Assert.AreEqual(0, (arguments[0] as IEnumerable<NotifiedTvShowItem>).Count());
        }

        [TestMethod]
        public void CalculateAndSendNotifications_ShowWithNextEpisodeDateInThePast_DoesNotGetNotified()
        {
            var persistManager = GetTvShowPersistManager();
            var notificationManager = GetNotificationManager();

            var tvShow = new TvShowSubscribedItem(1, "Lost") { NextEpisodeDate = new DateTime(2010, 5, 23, 21, 0, 0) };
            var now = new DateTime(2010, 5, 24, 8, 0, 0);

            SetDatesForDateTimeProvider(now, now);

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(new TvShowSubscribedItem[] { tvShow } as IEnumerable<TvShowSubscribedItem>));
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync), Task.FromResult(Enumerable.Empty<NotifiedTvShowItem>()));

            var notifier = new TvShowNotifier(persistManager, notificationManager);

            notifier.CalculateAndSendNotifications();

            Assert.AreEqual(0, notificationManager.GetNumberOfRecordedCalls(nameof(INotificationManager.SendEpisodeReminderNotification)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync)));

            var arguments = persistManager.GetFirstCallArguments(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            Assert.AreEqual(1, arguments.Length);
            Assert.AreEqual(0, (arguments[0] as IEnumerable<NotifiedTvShowItem>).Count());
        }

        [TestMethod]
        public void CalculateAndSendNotifications_ShowWithNextEpisodeBeforeNotificationDeadLine_DoesNotGetNotified()
        {
            var persistManager = GetTvShowPersistManager();
            var notificationManager = GetNotificationManager();

            var tvShow = new TvShowSubscribedItem(1, "Lost") { NextEpisodeDate = new DateTime(2010, 5, 23, 21, 0, 0) };
            var now = new DateTime(2010, 5, 23, 8, 0, 0);

            SetDatesForDateTimeProvider(now, now);

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(new TvShowSubscribedItem[] { tvShow } as IEnumerable<TvShowSubscribedItem>));
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync), Task.FromResult(Enumerable.Empty<NotifiedTvShowItem>()));

            var notifier = new TvShowNotifier(persistManager, notificationManager);

            notifier.CalculateAndSendNotifications();

            Assert.AreEqual(0, notificationManager.GetNumberOfRecordedCalls(nameof(INotificationManager.SendEpisodeReminderNotification)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync)));

            var arguments = persistManager.GetFirstCallArguments(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            Assert.AreEqual(1, arguments.Length);
            Assert.AreEqual(0, (arguments[0] as IEnumerable<NotifiedTvShowItem>).Count());
        }

        [TestMethod]
        public void CalculateAndSendNotifications_AlreadyNotifiedShow_DoesNotGetNotified()
        {
            var persistManager = GetTvShowPersistManager();
            var notificationManager = GetNotificationManager();

            var tvShow = new TvShowSubscribedItem(1, "Lost") { NextEpisodeDate = new DateTime(2010, 5, 23, 21, 0, 0), NextEpisodeId = "6x17" };
            var notifiedShow = new NotifiedTvShowItem(1) { EpisodeDate = new DateTime(2010, 5, 23, 21, 0, 0), EpisodeId = "6x17" };
            var now = new DateTime(2010, 5, 23, 16, 0, 0);

            SetDatesForDateTimeProvider(now, now);

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(new TvShowSubscribedItem[] { tvShow } as IEnumerable<TvShowSubscribedItem>));
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync), Task.FromResult(new NotifiedTvShowItem[] { notifiedShow } as IEnumerable<NotifiedTvShowItem>));

            var notifier = new TvShowNotifier(persistManager, notificationManager);

            notifier.CalculateAndSendNotifications();

            Assert.AreEqual(0, notificationManager.GetNumberOfRecordedCalls(nameof(INotificationManager.SendEpisodeReminderNotification)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync)));

            var arguments = persistManager.GetFirstCallArguments(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            Assert.AreEqual(1, arguments.Length);
            Assert.AreEqual(1, (arguments[0] as IEnumerable<NotifiedTvShowItem>).Count());
        }

        [TestMethod]
        public void CalculateAndSendNotifications_OneShow_GetNotified()
        {
            var persistManager = GetTvShowPersistManager();
            var notificationManager = GetNotificationManager();

            var tvShow = new TvShowSubscribedItem(1, "Lost") { NextEpisodeDate = new DateTime(2010, 5, 23, 21, 0, 0), NextEpisodeId = "6x17" };
            var now = new DateTime(2010, 5, 23, 16, 0, 0);

            SetDatesForDateTimeProvider(now, now);

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(new TvShowSubscribedItem[] { tvShow } as IEnumerable<TvShowSubscribedItem>));
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync), Task.FromResult(Enumerable.Empty<NotifiedTvShowItem>()));

            var notifier = new TvShowNotifier(persistManager, notificationManager);

            notifier.CalculateAndSendNotifications();

            Assert.AreEqual(1, notificationManager.GetNumberOfRecordedCalls(nameof(INotificationManager.SendEpisodeReminderNotification)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync)));

            var arguments = persistManager.GetFirstCallArguments(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            Assert.AreEqual(1, arguments.Length);
            Assert.AreEqual(1, (arguments[0] as IEnumerable<NotifiedTvShowItem>).Count());
        }

        [TestMethod]
        public void CalculateAndSendNotifications_MultipleShows_OneGetNotified()
        {
            var persistManager = GetTvShowPersistManager();
            var notificationManager = GetNotificationManager();

            var tvShow1 = new TvShowSubscribedItem(1, "Lost") { NextEpisodeDate = new DateTime(2010, 5, 23, 21, 0, 0), NextEpisodeId = "6x17" };
            var tvShow2 = new TvShowSubscribedItem(2, "House, M.D.") { NextEpisodeDate = new DateTime(2010, 5, 23, 20, 0, 0), NextEpisodeId = "6x22" }; //Fake
            var notifiedShow = new NotifiedTvShowItem(2) { EpisodeDate = new DateTime(2010, 5, 23, 20, 0, 0), EpisodeId = "6x22" };
            var now = new DateTime(2010, 5, 23, 16, 0, 0);

            SetDatesForDateTimeProvider(now, now);

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(new TvShowSubscribedItem[] { tvShow1, tvShow2 } as IEnumerable<TvShowSubscribedItem>));
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync), Task.FromResult(new NotifiedTvShowItem[] { notifiedShow } as IEnumerable<NotifiedTvShowItem>));

            var notifier = new TvShowNotifier(persistManager, notificationManager);

            notifier.CalculateAndSendNotifications();

            Assert.AreEqual(1, notificationManager.GetNumberOfRecordedCalls(nameof(INotificationManager.SendEpisodeReminderNotification)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync)));

            var arguments = persistManager.GetFirstCallArguments(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            Assert.AreEqual(1, arguments.Length);
            Assert.AreEqual(2, (arguments[0] as IEnumerable<NotifiedTvShowItem>).Count());
        }

        [TestMethod]
        public void CalculateAndSendNotifications_MultipleShows_AllGetNotified()
        {
            var persistManager = GetTvShowPersistManager();
            var notificationManager = GetNotificationManager();

            var tvShow1 = new TvShowSubscribedItem(1, "Lost") { NextEpisodeDate = new DateTime(2010, 5, 23, 21, 0, 0), NextEpisodeId = "6x17" };
            var tvShow2 = new TvShowSubscribedItem(2, "House, M.D.") { NextEpisodeDate = new DateTime(2010, 5, 23, 21, 0, 0), NextEpisodeId = "6x22" }; //Fake
            var now = new DateTime(2010, 5, 23, 16, 0, 0);

            SetDatesForDateTimeProvider(now, now);

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(new TvShowSubscribedItem[] { tvShow1, tvShow2 } as IEnumerable<TvShowSubscribedItem>));
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync), Task.FromResult(Enumerable.Empty<NotifiedTvShowItem>()));

            var notifier = new TvShowNotifier(persistManager, notificationManager);

            notifier.CalculateAndSendNotifications();

            Assert.AreEqual(2, notificationManager.GetNumberOfRecordedCalls(nameof(INotificationManager.SendEpisodeReminderNotification)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync)));

            var arguments = persistManager.GetFirstCallArguments(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            Assert.AreEqual(1, arguments.Length);
            Assert.AreEqual(2, (arguments[0] as IEnumerable<NotifiedTvShowItem>).Count());
        }
    }
}
