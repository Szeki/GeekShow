using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using GeekShow.Shared.Component;
using GeekShow.Test.Mocks;
using GeekShow.ViewModel;
using GeekShow.Shared.Service;
using GeekShow.Shared.Model;
using GeekShow.View;

namespace GeekShow.Test.GeekShow.ViewModel
{
    [TestClass]
    public class ListMyTvShowViewModelTest : UnitTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            InitializeTestRun();
        }

        [TestMethod]
        public void TvShowItemClickedCommand_WithNull_DoesNothing()
        {
            var navigationService = GetNavigationService();

            var viewModel = new ListMyTvShowViewModel();

            viewModel.TvShowItemClickedCommand.Execute(null);

            Assert.AreEqual(0, navigationService.GetNumberOfRecordedCalls(nameof(INavigationService.Navigate)));
        }

        [TestMethod]
        public void TvShowItemClickedCommand_WithTvShow_NavigatesToDetailPage()
        {
            var navigationService = GetNavigationService();

            var tvShow = new TvShowSubscribedItem(1, "Test TvShow");
            var viewModel = new ListMyTvShowViewModel();

            viewModel.TvShowItemClickedCommand.Execute(tvShow);

            Assert.AreEqual(1, navigationService.GetNumberOfRecordedCalls(nameof(INavigationService.Navigate)));

            var arguments = navigationService.GetFirstCallArguments(nameof(INavigationService.Navigate));

            Assert.AreEqual(2, arguments.Length);
            Assert.AreEqual(typeof(TvShowSubscribedItemDetailsPage), arguments[0]);
            Assert.AreEqual(typeof(TvShowSubscribedItemDetailsViewModel), arguments[1].GetType());
            Assert.AreEqual(tvShow, ((TvShowSubscribedItemDetailsViewModel)arguments[1]).TvShow);
        }
    }
}
