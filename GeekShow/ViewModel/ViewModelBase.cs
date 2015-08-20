using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace GeekShow.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Members

        static ObservableCollection<TvShowSubscribedItem> _tvShows;
        static ITvShowPersistManager _persistManager;

        #endregion

        #region Constructor

        static ViewModelBase()
        {
            _tvShows = new ObservableCollection<TvShowSubscribedItem>();
            _persistManager = IoC.Container.ResolveType<ITvShowPersistManager>();
        }

        public ViewModelBase()
        {
            NavigationService = IoC.Container.ResolveType<INavigationService>();
            TvShowService = IoC.Container.ResolveType<ITvShowService>();
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Properties

        protected INavigationService NavigationService
        {
            get;
            set;
        }

        protected ITvShowService TvShowService
        {
            get;
            set;
        }

        public ObservableCollection<TvShowSubscribedItem> TvShows
        {
            get
            {
                return _tvShows;
            }
        }

        #endregion

        #region Public Methods

        public virtual void ClearViewModel()
        {
        }

        public static async void LoadPersistedTvShows()
        {
            _tvShows.Clear();

            foreach(var tvShow in await _persistManager.LoadTvShowsAsync())
            {
                _tvShows.Add(tvShow);
            }
        }

        public static async void UpdateTvShows()
        {
            var updatedShows = await _persistManager.LoadTvShowsAsync();

            foreach(var tvShow in _tvShows)
            {
                var updatedShow = updatedShows.FirstOrDefault(item => item.ShowId == tvShow.ShowId);

                if(updatedShow == null)
                {
                    continue;
                }

                UpdateTvShow(tvShow, updatedShow);
            }
        }

        public static IEnumerable<TvShowSubscribedItem> GetTvShows()
        {
            return _tvShows.ToArray();
        }

        #endregion

        #region Protected Methods

        protected async void AddTvShowAndPersist(TvShowSubscribedItem tvShow)
        {
            TvShows.Add(tvShow);

            await _persistManager.SaveTvShowsAsync(TvShows);
        }

        protected async void RemoveTvShowAndPersist(TvShowSubscribedItem tvShow)
        {
            TvShows.Remove(tvShow);

            await _persistManager.SaveTvShowsAsync(TvShows);
        }

        protected async void SaveTvShows()
        {
            await _persistManager.SaveTvShowsAsync(TvShows);
        }

        #endregion

        #region Private Methods

        private static void UpdateTvShow(TvShowSubscribedItem current, TvShowSubscribedItem updated)
        {
            current.LastEpisodeId = updated.LastEpisodeId;
            current.LastEpisodeName = updated.LastEpisodeName;
            current.LastEpisodeDate = updated.LastEpisodeDate;
            current.NextEpisodeId = updated.NextEpisodeId;
            current.NextEpisodeName = updated.NextEpisodeName;
            current.NextEpisodeDate = updated.NextEpisodeDate;
            current.Status = updated.Status;
            current.EndDate = updated.EndDate;
        }

        #endregion
    }
}
