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
    public class TvShowImdbServiceQueryReultParser
    {
        #region Public Methods

        public IEnumerable<TvShowImdbSearchItem> GetSearchedShows(string searchResult)
        {
            if (!IsResponseSuccessful(searchResult))
            {
                return Enumerable.Empty<TvShowImdbSearchItem>();
            }

            var jObject = JObject.Parse(searchResult);

            var result = JsonConvert.DeserializeObject<SearchResult>(searchResult);

            return result.Search;
        }

        public TvShowImdbItem ParseTvShowResponse(string response)
        {
            if(!IsResponseSuccessful(response))
            {
                return null;
            }

            var serializerSetting = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return JsonConvert.DeserializeObject<TvShowImdbItem>(response, serializerSetting);
        }

        #endregion

        #region Private Methods

        private bool IsResponseSuccessful(string response)
        {
            if(string.IsNullOrEmpty(response))
            {
                return false;
            }

            var jObject = JObject.Parse(response);

            var responseProperty = jObject.Property("Response");

            if (responseProperty != null)
            {
                return (bool)responseProperty.Value;
            }

            return true;
        }

        #endregion

        #region Internal Helper Objects

        struct SearchResult
        {
            public IEnumerable<TvShowImdbSearchItem> Search
            {
                get;
                set;
            }
        }

        #endregion
    }
}
