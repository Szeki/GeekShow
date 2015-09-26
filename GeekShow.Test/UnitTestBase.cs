using GeekShow.Shared.Component;
using GeekShow.Shared.Service;
using GeekShow.Test.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Test
{
    public abstract class UnitTestBase
    {
        private static bool _isInitialized;

        [TestInitialize]
        public void InitializeTest()
        {
            GetNavigationService().ClearRecords();
            GetTvShowPersistManager().ClearRecords();
            GetTvShowService().ClearRecords();
            GetPopupMessageService().ClearRecords();
            GetNotificationManager().ClearRecords();

            new ViewModelBaseMock().TvShows.Clear();
        }

        #region Protected Methods

        protected NavigationServiceMock GetNavigationService()
        {
            return IoC.Container.ResolveType<INavigationService>() as NavigationServiceMock;
        }

        protected TvShowPersistManagerMock GetTvShowPersistManager()
        {
            return IoC.Container.ResolveType<ITvShowPersistManager>() as TvShowPersistManagerMock;
        }

        protected TvShowServiceMock GetTvShowService()
        {
            return IoC.Container.ResolveType<ITvShowService>() as TvShowServiceMock;
        }

        protected PopupMessageServiceMock GetPopupMessageService()
        {
            return IoC.Container.ResolveType<IPopupMessageService>() as PopupMessageServiceMock;
        }

        protected NotificationManagerMock GetNotificationManager()
        {
            return IoC.Container.ResolveType<INotificationManager>() as NotificationManagerMock;
        }

        protected void SetDatesForDateTimeProvider(DateTime now, DateTime utcNow)
        {
            DateTimeProvider.CustomProvider = new CustomDateTimeProviderMock() { Now = now, UtcNow = utcNow };
        }
        
        protected static void InitializeTestRun()
        {
            if (_isInitialized) return;

            IoC.Container.RegisterInstance<INavigationService>(new NavigationServiceMock());
            IoC.Container.RegisterInstance<ITvShowPersistManager>(new TvShowPersistManagerMock());
            IoC.Container.RegisterInstance<ITvShowService>(new TvShowServiceMock());
            IoC.Container.RegisterInstance<IPopupMessageService>(new PopupMessageServiceMock());
            IoC.Container.RegisterInstance<INotificationManager>(new NotificationManagerMock());

            _isInitialized = true;
        }

        #endregion
    }
}
