using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShow.Shared.Component
{
    public class TvShowPersistManager : ITvShowPersistManager
    {
        #region Members

        readonly static JsonSerializerSettings _serializerSettings;

        #endregion

        #region Constructor

        static TvShowPersistManager()
        {
            _serializerSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }

        #endregion

        #region ITvShowPersistManager implementation

        public void SaveTvShows(IEnumerable<Model.TvShowSubscribedItem> tvShows)
        {
            var file = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("tvshows.json", Windows.Storage.CreationCollisionOption.ReplaceExisting).GetResults();

            Windows.Storage.FileIO.WriteTextAsync(file, SerializeJson(tvShows)).GetResults();
        }

        public async Task SaveTvShowsAsync(IEnumerable<Model.TvShowSubscribedItem> tvShows)
        {
            var file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("tvshows.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);

            await Windows.Storage.FileIO.WriteTextAsync(file, SerializeJson(tvShows));
        }

        public IEnumerable<Model.TvShowSubscribedItem> LoadTvShows()
        {
            try
            {
                var file = Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("tvshows.json").GetResults();
                var json = Windows.Storage.FileIO.ReadTextAsync(file).GetResults();

                return DeserializeJson<Model.TvShowSubscribedItem>(json);
            }
            catch
            {
                return Enumerable.Empty<Model.TvShowSubscribedItem>();
            }
        }

        public async Task<IEnumerable<Model.TvShowSubscribedItem>> LoadTvShowsAsync()
        {
            try
            {
                var file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("tvshows.json");
                var json = await Windows.Storage.FileIO.ReadTextAsync(file);

                return DeserializeJson<Model.TvShowSubscribedItem>(json);
            }
            catch
            {
                return Enumerable.Empty<Model.TvShowSubscribedItem>();
            }
        }

        public IEnumerable<Model.NotifiedTvShowItem> LoadNotifiedTvShows()
        {
            try
            {
                var file = Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("notifiedshows.json").GetResults();
                var json = Windows.Storage.FileIO.ReadTextAsync(file).GetResults();

                return DeserializeJson<Model.NotifiedTvShowItem>(json);
            }
            catch
            {
                return Enumerable.Empty<Model.NotifiedTvShowItem>();
            }
        }

        public async Task<IEnumerable<Model.NotifiedTvShowItem>> LoadNotifiedTvShowsAsync()
        {
            try
            {
                var file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("notifiedshows.json");
                var json = await Windows.Storage.FileIO.ReadTextAsync(file);

                return DeserializeJson<Model.NotifiedTvShowItem>(json);
            }
            catch
            {
                return Enumerable.Empty<Model.NotifiedTvShowItem>();
            }
        }

        public void SaveNotifiedTvShows(IEnumerable<Model.NotifiedTvShowItem> tvShows)
        {
            var file = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("notifiedshows.json", Windows.Storage.CreationCollisionOption.ReplaceExisting).GetResults();

            Windows.Storage.FileIO.WriteTextAsync(file, SerializeJson(tvShows)).GetResults();
        }

        public async Task SaveNotifiedTvShowsAsync(IEnumerable<Model.NotifiedTvShowItem> tvShows)
        {
            var file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("notifiedshows.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);

            await Windows.Storage.FileIO.WriteTextAsync(file, SerializeJson(tvShows));
        }

        #endregion

        #region Private Methods

        private string SerializeJson<T>(IEnumerable<T> tvShows)
        {
            return JsonConvert.SerializeObject(tvShows, _serializerSettings);
        }

        private IEnumerable<T> DeserializeJson<T>(string jsonValue)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonValue, _serializerSettings);
        }

        #endregion
    }
}
