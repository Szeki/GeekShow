using GeekShow.Shared.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Service
{
    public class TvShowEpisodeServiceQueryResultParser
    {
        #region Public Methods

        public IEnumerable<TvShowEpisodeQucikInfo> ParseEpisodesFromResponse(string tvShowName, string response)
        {
            var jObject = JObject.Parse(response);
            var token = jObject[tvShowName];

            if(token == null)
            {
                return Enumerable.Empty<TvShowEpisodeQucikInfo>();
            }

            var episodeResult = token.ToObject<EpisodesResult>();

            return episodeResult.Episodes;
        }

        public TvShowEpisode ParseEpisodeFromResponse(string response)
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return JsonConvert.DeserializeObject<TvShowEpisode>(response, serializerSettings);
        }

        #endregion

        #region Internal Helpers Objects

        struct EpisodesResult
        {
            public IEnumerable<TvShowEpisodeQucikInfo> Episodes
            {
                get;
                set;
            }
        }

        #endregion
    }
}
