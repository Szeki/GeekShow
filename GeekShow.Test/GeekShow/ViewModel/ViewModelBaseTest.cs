using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Test.Mocks;
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
    public class ViewModelBaseTest : UnitTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            InitializeTestRun();
        }

        [TestMethod]
        public void GetTvShows_ReturnsCurrentTvShows()
        {
            var viewModelMock = new ViewModelBaseMock();
            
            viewModelMock.TvShows.Add(new TvShowSubscribedItem("1", "Lost"));

            var actualTvShows = ViewModelBase.GetTvShows().ToArray();

            Assert.AreEqual(1, actualTvShows.Length);
        }

        [TestMethod]
        public void LoadPersistedTvShows_LoadsFromPersistManager_And_SetsTvShows()
        {
            var persistedTvShows = new List<TvShowSubscribedItem>
            {
                new TvShowSubscribedItem("1", "Lost"),
                new TvShowSubscribedItem("2", "Wire")
            };

            var persistManager = GetTvShowPersistManager();
            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(persistedTvShows as IEnumerable<TvShowSubscribedItem>));

            ViewModelBase.LoadPersistedTvShows();

            var actualTvShows = ViewModelBase.GetTvShows().ToArray();

            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.LoadTvShowsAsync)));
            Assert.AreEqual(persistedTvShows.Count, actualTvShows.Length);
        }

        [TestMethod]
        public void UpdateTvShows_UpdatesFromPersistedShows()
        {
            var persistManager = GetTvShowPersistManager();
            var viewModelMock = new ViewModelBaseMock();

            var originalShow = new TvShowSubscribedItem("1", "Lost")
            {
                LastEpisodeDate = new DateTime(2010, 5, 18),
                LastEpisodeId = "5x17",
                LastEpisodeName = "What They Died For",
                NextEpisodeDate = new DateTime(2010, 5, 23),
                NextEpisodeId = "5x18",
                NextEpisodeName = "The End",
                Status = "Running",
                EndDate = null
            };

            viewModelMock.TvShows.Add(originalShow);

            var updateShow = new TvShowSubscribedItem("1", "Lost")
            {
                LastEpisodeDate = new DateTime(2010, 5, 23),
                LastEpisodeId = "5x18",
                LastEpisodeName = "The End",
                NextEpisodeDate = null,
                NextEpisodeId = null,
                NextEpisodeName = null,
                Status = "Ended",
                EndDate = new DateTime(2010, 5, 23)
            };

            var updateList = new TvShowSubscribedItem[] { updateShow };

            persistManager.ReturnsForAnyArgs(nameof(ITvShowPersistManager.LoadTvShowsAsync), Task.FromResult(updateList as IEnumerable<TvShowSubscribedItem>));

            ViewModelBase.UpdateTvShows();

            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.LoadTvShowsAsync)));

            var tvShows = ViewModelBase.GetTvShows().ToArray();

            Assert.AreEqual(1, tvShows.Length);
            Assert.AreEqual(updateShow.LastEpisodeDate, tvShows[0].LastEpisodeDate);
            Assert.AreEqual(updateShow.LastEpisodeId, tvShows[0].LastEpisodeId);
            Assert.AreEqual(updateShow.LastEpisodeName, tvShows[0].LastEpisodeName);
            Assert.AreEqual(updateShow.NextEpisodeDate, tvShows[0].NextEpisodeDate);
            Assert.AreEqual(updateShow.NextEpisodeId, tvShows[0].NextEpisodeId);
            Assert.AreEqual(updateShow.NextEpisodeName, tvShows[0].NextEpisodeName);
            Assert.AreEqual(updateShow.Status, tvShows[0].Status);
            Assert.AreEqual(updateShow.EndDate, tvShows[0].EndDate);
        }

        [TestMethod]
        public void AddTvShowAndPersist_PersistsTvShow()
        {
            var persistManager = GetTvShowPersistManager();
            var viewModelMock = new ViewModelBaseMock();
            var tvShowToPersist = new TvShowSubscribedItem("1", "Lost");

            viewModelMock.AddTvShowAndPersist(tvShowToPersist);

            Assert.AreEqual(1, viewModelMock.TvShows.Count);
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveTvShowsAsync)));
            Assert.AreEqual(tvShowToPersist, viewModelMock.TvShows[0]);
        }

        [TestMethod]
        public void RemoveTvShowAndPersist_RemovesTvShow()
        {
            var persistManager = GetTvShowPersistManager();
            var viewModelMock = new ViewModelBaseMock();
            var tvShowToRemove = new TvShowSubscribedItem("1", "Lost");

            viewModelMock.TvShows.Add(tvShowToRemove);

            viewModelMock.RemoveTvShowAndPersist(tvShowToRemove);

            Assert.AreEqual(0, viewModelMock.TvShows.Count);
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveTvShowsAsync)));
        }

        [TestMethod]
        public void SaveTvShows_PersistsTvShows()
        {
            var persistManager = GetTvShowPersistManager();
            var viewModelMock = new ViewModelBaseMock();
            
            viewModelMock.SaveTvShows();
            
            Assert.AreEqual(1, persistManager.GetNumberOfRecordedCalls(nameof(ITvShowPersistManager.SaveTvShowsAsync)));
        }
    }
}
