using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using GeekShow.View;
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
    public class SearchTvShowViewModelTest : UnitTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            InitializeTestRun();
        }

        [TestMethod]
        public void CanSearch_NoSearchText_ReturnsFalse()
        {
            var viewModel = new SearchTvShowViewModel();

            Assert.IsFalse(viewModel.CanSearch);
        }

        [TestMethod]
        public void CanSearch_WithSearchText_ReturnsTrue()
        {
            var viewModel = new SearchTvShowViewModel()
            {
                SearchText = "Lost"
            };
            
            Assert.IsTrue(viewModel.CanSearch);
        }

        [TestMethod]
        public void SearchTvShow_Search_SetsSearchResult()
        {
            var serviceSearchResult = new List<TvShowItem>
            {
                new TvShowItem(1, "Lost")
            };

            var tvShowService = GetTvShowService();
            tvShowService.ReturnsForAnyArgs(nameof(ITvShowService.SearchShowAsync), Task.FromResult(serviceSearchResult as IEnumerable<TvShowItem>));

            var viewModel = new SearchTvShowViewModel()
            {
                SearchText = "Lost"
            };

            viewModel.SearchTvShow();

            Assert.AreEqual(1, tvShowService.GetNumberOfRecordedCalls(nameof(ITvShowService.SearchShowAsync)));
            Assert.AreEqual(1, viewModel.SearchResult.Count);
            Assert.AreEqual(serviceSearchResult[0], viewModel.SearchResult[0]);
            Assert.IsFalse(viewModel.IsSearchInProgress);
        }

        [TestMethod]
        public void SearchTvShow_SearchHasException_DisplaysPopupMessage()
        {
            var popupService = GetPopupMessageService();
            var tvShowService = GetTvShowService();
            tvShowService.RegisterMethodAction(nameof(ITvShowService.SearchShowAsync), () => { throw new Exception(); });

            var viewModel = new SearchTvShowViewModel()
            {
                SearchText = "Lost"
            };

            viewModel.SearchTvShow();

            Assert.AreEqual(1, tvShowService.GetNumberOfRecordedCalls(nameof(ITvShowService.SearchShowAsync)));
            Assert.AreEqual(1, popupService.GetNumberOfRecordedCalls(nameof(IPopupMessageService.DisplayMessage)));
            Assert.AreEqual(0, viewModel.SearchResult.Count);
            Assert.IsFalse(viewModel.IsSearchInProgress);
        }

        [TestMethod]
        public void SearchCommand_Execute_SearchesAndSetsSearchResult()
        {
            var serviceSearchResult = new List<TvShowItem>
            {
                new TvShowItem(1, "Lost")
            };

            var tvShowService = GetTvShowService();
            tvShowService.ReturnsForAnyArgs(nameof(ITvShowService.SearchShowAsync), Task.FromResult(serviceSearchResult as IEnumerable<TvShowItem>));

            var viewModel = new SearchTvShowViewModel()
            {
                SearchText = "Lost"
            };

            viewModel.SearchCommand.Execute(null);

            Assert.AreEqual(1, tvShowService.GetNumberOfRecordedCalls(nameof(ITvShowService.SearchShowAsync)));
            Assert.AreEqual(1, viewModel.SearchResult.Count);
            Assert.AreEqual(serviceSearchResult[0], viewModel.SearchResult[0]);
            Assert.IsFalse(viewModel.IsSearchInProgress);
        }

        [TestMethod]
        public void TvShowItemClickedCommand_NullParameter_DoesNothing()
        {
            var navigationService = GetNavigationService();

            var viewModel = new SearchTvShowViewModel();

            viewModel.TvShowItemClickedCommand.Execute(null);

            Assert.AreEqual(0, navigationService.GetNumberOfRecordedCalls(nameof(INavigationService.Navigate)));
        }

        [TestMethod]
        public void TvShowItemClickedCommand_WithParameter_NavigatesToTvShowSearchItemDetailsPage()
        {
            var tvShow = new TvShowItem(1, "Lost");
            var navigationService = GetNavigationService();

            var viewModel = new SearchTvShowViewModel();

            viewModel.TvShowItemClickedCommand.Execute(tvShow);

            Assert.AreEqual(1, navigationService.GetNumberOfRecordedCalls(nameof(INavigationService.Navigate)));

            var arguments = navigationService.GetFirstCallArguments(nameof(INavigationService.Navigate));

            Assert.AreEqual(2, arguments.Length);
            Assert.AreEqual(typeof(TvShowSearchItemDetailsPage), arguments[0]);
            Assert.AreEqual(typeof(TvShowSearchItemDetailsViewModel), arguments[1].GetType());
            Assert.AreEqual(tvShow, ((TvShowSearchItemDetailsViewModel)arguments[1]).TvShow);
        }
    }
}
