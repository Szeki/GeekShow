using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using GeekShow.ViewModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Test.GeekShow.ViewModel
{
    [TestClass]
    public class TvShowSearchItemDetailsViewModelTest : UnitTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            InitializeTestRun();
        }

        [TestMethod]
        public void CancelCommand_NavigatesBack()
        {
            var navigationService = GetNavigationService();
            var tvShow = new TvShowItem(1, "Lost");
            var viewModel = new TvShowSearchItemDetailsViewModel(tvShow);

            viewModel.CancelCommand.Execute(null);

            Assert.AreEqual(1, navigationService.GetNumberOfRecordedCalls(nameof(INavigationService.GoBack)));
        }

        [TestMethod]
        public void SubscribeCommand_SubscribeToAlreadySubscribed_DoesNothing()
        {
            var navigationService = GetNavigationService();
            var tvShowService = GetTvShowService();
            var persistManager = GetTvShowPersistManager();

            var tvShow = new TvShowItem(1, "Lost");
            var viewModel = new TvShowSearchItemDetailsViewModel(tvShow);

            viewModel.TvShows.Add(TvShowSubscribedItem.CloneBaseItem(tvShow));

            viewModel.SubscribeCommand.Execute(null);

            Assert.AreEqual(0, tvShowService.GetNumberOfRecordedCalls(nameof(ITvShowService.GetTvShowQuickInfoAsync)));
            Assert.AreEqual(0, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveTvShowsAsync)));
            Assert.AreEqual(0, navigationService.GetNumberOfRecordedCalls(nameof(INavigationService.Navigate)));
            Assert.AreEqual(1, viewModel.TvShows.Count);
        }

        [TestMethod]
        public void SubscribeCommand_QuickInfoThrowsException_DoesNothing()
        {
            var navigationService = GetNavigationService();
            var tvShowService = GetTvShowService();
            var persistManager = GetTvShowPersistManager();
            var popupService = GetPopupMessageService();

            tvShowService.RegisterMethodAction(nameof(ITvShowService.GetTvShowQuickInfoAsync), () => { throw new Exception(); });

            var tvShow = new TvShowItem(1, "Lost");
            var viewModel = new TvShowSearchItemDetailsViewModel(tvShow);

            viewModel.SubscribeCommand.Execute(null);

            Assert.AreEqual(1, tvShowService.GetNumberOfRecordedCalls(nameof(ITvShowService.GetTvShowQuickInfoAsync)));
            Assert.AreEqual(0, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveTvShowsAsync)));
            Assert.AreEqual(0, navigationService.GetNumberOfRecordedCalls(nameof(INavigationService.Navigate)));
            Assert.AreEqual(1, popupService.GetNumberOfRecordedCalls(nameof(IPopupMessageService.DisplayMessage)));
            Assert.AreEqual(0, viewModel.TvShows.Count);
        }

        [TestMethod]
        public void SubscribeCommand_PersistsAndNavigates()
        {
            var navigationService = GetNavigationService();
            var tvShowService = GetTvShowService();
            var persistManager = GetTvShowPersistManager();
            
            var tvShow = new TvShowItem(1, "Lost")
            {
                Classification = "Drama",
                Country = "US",
                EndDate = null,
                Ended = null,
                Link = "http",
                Seasons = 6,
                Started = 1,
                Status = "Ongoing"
            };

            var tvShowQuickInfo = new TvShowQuickInfoItem()
            {
                AirTime = "9PM CET",
                Classification = "Drama",
                Country = "US",
                Ended = new DateTime(2010, 5, 23),
                Genres = "Drama",
                LastEpisodeDate = new DateTime(2010, 5, 18),
                LastEpisodeId = "6x16",
                LastEpisodeName = "What They Died For",
                Network = "ABC",
                NextEpisodeDate = new DateTime(2010, 5, 23),
                NextEpisodeId = "6x17",
                NextEpisodeName = "The End",
                Premiered = 0,
                Runtime = 60,
                ShowId = 1,
                ShowName = "Lost",
                ShowUrl = "http",
                Started = new DateTime(2004, 9, 22),
                Status = "Ended"
            };

            tvShowService.ReturnsForAnyArgs(nameof(ITvShowService.GetTvShowQuickInfoAsync), Task.FromResult(tvShowQuickInfo));

            var viewModel = new TvShowSearchItemDetailsViewModel(tvShow);

            viewModel.SubscribeCommand.Execute(null);

            Assert.AreEqual(1, tvShowService.GetNumberOfRecordedCalls(nameof(ITvShowService.GetTvShowQuickInfoAsync)));
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveTvShowsAsync)));
            Assert.AreEqual(1, viewModel.TvShows.Count);
            Assert.AreEqual(1, navigationService.GetNumberOfRecordedCalls(nameof(INavigationService.Navigate)));

            var arguments = navigationService.GetFirstCallArguments(nameof(INavigationService.Navigate));

            Assert.AreEqual(2, arguments.Length);
            Assert.AreEqual(typeof(MainPage), arguments[0]);
            Assert.AreEqual(MainPageItems.List, arguments[1]);
        }
    }
}
