using GeekShow.Core.Component;
using GeekShow.Core.Model;
using GeekShow.Core.Model.TvMaze;
using GeekShow.Shared.Component;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace GeekShow.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Members

        static ObservableCollection<TvMazeTvShow> _tvShows;
        
        #endregion

        #region Constructor

        static ViewModelBase()
        {
            _tvShows = new ObservableCollection<TvMazeTvShow>();
        }

        public ViewModelBase(INavigationService navigationService, Core.Service.ITvShowService tvShowService)
        {
            NavigationService = navigationService;
            TvShowService = tvShowService;
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

        protected Core.Service.ITvShowService TvShowService
        {
            get;
            set;
        }

        public ObservableCollection<TvMazeTvShow> TvShows
        {
            get
            {
                return _tvShows;
            }
        }

        #endregion

        #region Static Properties

        public static ITvShowPersistManager PersistManager
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        public virtual void ClearViewModel()
        {
        }

        public static async void LoadPersistedTvShows()
        {
            _tvShows.Clear();

            foreach(var tvShow in await PersistManager.LoadTvShowsAsync())
            {
                _tvShows.Add(tvShow);
            }
        }

        public static async void UpdateTvShows()
        {
            var updatedShows = await PersistManager.LoadTvShowsAsync();

            foreach (var tvShow in _tvShows)
            {
                var updatedShow = updatedShows.FirstOrDefault(item => item.TvShow.Id == tvShow.TvShow.Id);

                if (updatedShow == null)
                {
                    continue;
                }
                
                UpdateTvShow(tvShow, updatedShow);
            }
        }
        
        #endregion

        #region Protected Methods

        protected async void AddTvShowAndPersist(TvMazeTvShow tvShow)
        {
            TvShows.Add(tvShow);

            await PersistManager.SaveTvShowsAsync(TvShows);
        }

        protected async void RemoveTvShowAndPersist(TvMazeTvShow tvShow)
        {
            TvShows.Remove(tvShow);

            await PersistManager.SaveTvShowsAsync(TvShows);
        }

        protected async void SaveTvShows()
        {
            await PersistManager.SaveTvShowsAsync(TvShows);
        }

        #endregion

        #region Private Methods

        private static void UpdateTvShow(TvMazeTvShow current, TvMazeTvShow updated)
        {
            current.TvShow = updated.TvShow;
            current.PreviousEpisode = updated.PreviousEpisode;
            current.NextEpisode = updated.NextEpisode;
            current.Episodes = updated.Episodes;
        }

        #endregion
    }
}
