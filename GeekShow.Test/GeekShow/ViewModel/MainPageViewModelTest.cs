using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
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
    public class MainPageViewModelTest : UnitTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            InitializeTestRun();
        }

        [TestMethod]
        public void SelectViewModel_List_SetsSelectedIndexToZero()
        {
            var persistManager = GetTvShowPersistManager();
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(Enumerable.Empty<TvShowSubscribedItem>()));

            var viewModel = new MainPageViewModel();

            viewModel.SelectViewModel(MainPageItems.List);

            Assert.AreEqual(0, viewModel.SelectedMenuIndex);
        }

        [TestMethod]
        public void SelectViewModel_Search_SetsSelectedIndexToOne()
        {
            var persistManager = GetTvShowPersistManager();
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(Enumerable.Empty<TvShowSubscribedItem>()));

            var viewModel = new MainPageViewModel();

            viewModel.SelectViewModel(MainPageItems.Search);

            Assert.AreEqual(1, viewModel.SelectedMenuIndex);
        }
    }
}
