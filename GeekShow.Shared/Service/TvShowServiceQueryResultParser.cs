using GeekShow.Shared.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Windows.Data.Xml.Dom;

namespace GeekShow.Shared.Service
{
    public class TvShowServiceQueryResultParser
    {
        #region Members

        readonly static CultureInfo UsCulture = new CultureInfo("en-US");
        readonly static string DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzzzzzz";

        #endregion

        #region Public Methods

        public IEnumerable<TvShowItem> ParseSearchResult(string searchResult)
        {
            var tvShows = new List<TvShowItem>();

            if(string.IsNullOrEmpty(searchResult))
            {
                return tvShows;
            }

            var doc = new XmlDocument();
            
            doc.LoadXml(searchResult);

            var rootNode = doc.GetElementsByTagName("Results").FirstOrDefault();

            foreach(var node in rootNode.ChildNodes.Where(n => n.NodeName == "show"))
            {
                tvShows.Add(GetTvShowFromNode(node));
            }

            return tvShows;
        }

        public TvShowQuickInfoItem ParseQuckInfoResult(string quickInfoResult)
        {
            var tvShowQuickInfo = new TvShowQuickInfoItem();

            if(quickInfoResult.StartsWith("<pre>"))
            {
                quickInfoResult = quickInfoResult.Remove(0, 5);
            }

            string[] resultLines = quickInfoResult.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in resultLines)
            {
                ParseQuickInfoLine(tvShowQuickInfo, line);
            }

            return tvShowQuickInfo;
        }

        public TvShowItem ParseShowQueryResult(string showResult)
        {
            if (string.IsNullOrEmpty(showResult))
            {
                return null;
            }

            var doc = new XmlDocument();

            doc.LoadXml(showResult);

            var rootNode = doc.GetElementsByTagName("Showinfo").FirstOrDefault();

            return GetTvShowFromNode(rootNode);
        }

        #endregion

        #region Private Methods

        private TvShowItem GetTvShowFromNode(IXmlNode node)
        {
            var tvShow = new TvShowItem();

            foreach(var childNode in node.ChildNodes.Where(n => n.ChildNodes.Count > 0))
            {
                switch(childNode.NodeName)
                {
                    case "showid":
                        tvShow.ShowId = int.Parse(childNode.FirstChild.NodeValue.ToString());
                        break;
                    case "name":
                    case "showname":
                        tvShow.Name = childNode.FirstChild.NodeValue.ToString();
                        break;
                    case "link":
                    case "showlink":
                        tvShow.Link = childNode.FirstChild.NodeValue.ToString();
                        break;
                    case "country":
                    case "origin_country":
                        tvShow.Country = childNode.FirstChild.NodeValue.ToString();
                        break;
                    case "started":
                        tvShow.Started = int.Parse(childNode.FirstChild.NodeValue.ToString());
                        break;
                    case "ended":
                        var endValue = childNode.FirstChild.NodeValue.ToString();

                        if (endValue.Contains('/'))
                        {
                            tvShow.EndDate = DateTime.ParseExact(endValue, "MMM/dd/yyyy", UsCulture);
                        }
                        else
                        {
                            tvShow.Ended = int.Parse(endValue);
                        }
                        break;
                    case "seasons":
                        tvShow.Seasons = int.Parse(childNode.FirstChild.NodeValue.ToString());
                        break;
                    case "status":
                        tvShow.Status = childNode.FirstChild.NodeValue.ToString();
                        break;
                    case "classification":
                        tvShow.Classification = childNode.FirstChild.NodeValue.ToString();
                        break;
                }
            }

            return tvShow;
        }

        private void ParseQuickInfoLine(TvShowQuickInfoItem tvShow, string line)
        {
            var parts = line.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1]))
            {
                switch (parts[0])
                {
                    case "Show ID":
                        tvShow.ShowId = int.Parse(parts[1]);
                        break;
                    case "Show Name":
                        tvShow.ShowName = parts[1];
                        break;
                    case "Show URL":
                        tvShow.ShowUrl = parts[1];
                        break;
                    case "Premiered":
                        tvShow.Premiered = int.Parse(parts[1]);
                        break;
                    case "Started":
                        tvShow.Started = DateTime.ParseExact(parts[1], "MMM/dd/yyyy", UsCulture);
                        break;
                    case "Ended":
                        tvShow.Ended = DateTime.ParseExact(parts[1], "MMM/dd/yyyy", UsCulture);
                        break;
                    case "Latest Episode":
                        var latestEpisodeInformation = ParseEpisodeData(parts[1]);
                        tvShow.LastEpisodeId = latestEpisodeInformation.Id;
                        tvShow.LastEpisodeName = latestEpisodeInformation.Name;
                        tvShow.LastEpisodeDate = latestEpisodeInformation.Date.DateTime;

                        break;
                    case "Next Episode":
                        var nextEpisodeInformation = ParseEpisodeData(parts[1]);
                        tvShow.NextEpisodeId = nextEpisodeInformation.Id;
                        tvShow.NextEpisodeName = nextEpisodeInformation.Name;
                        tvShow.NextEpisodeDate = nextEpisodeInformation.Date;

                        break;
                    case "RFC3339":
                        tvShow.NextEpisodeDate = XmlConvert.ToDateTimeOffset(parts[1], DateTimeFormat);
                        break;
                    case "Country":
                        tvShow.Country = parts[1];
                        break;
                    case "Status":
                        tvShow.Status = parts[1];
                        break;
                    case "Classification":
                        tvShow.Classification = parts[1];
                        break;
                    case "Genres":
                        tvShow.Genres = parts[1];
                        break;
                    case "Network":
                        tvShow.Network = parts[1];
                        break;
                    case "Airtime":
                        tvShow.AirTime = parts[1];
                        break;
                    case "Runtime":
                        tvShow.Runtime = int.Parse(parts[1]);
                        break;
                }
            }
        }

        private EpisodeInformation ParseEpisodeData(string episodeData)
        {
            var parts = episodeData.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);

            return new EpisodeInformation()
            {
                Id = parts[0],
                Name = parts[1],
                Date = DateTime.ParseExact(parts[2], "MMM/dd/yyyy", UsCulture)
            };
        }

        #endregion

        #region Episode Information Struct

        struct EpisodeInformation
        {
            public string Id
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }

            public DateTimeOffset Date
            {
                get;
                set;
            }
        }

        #endregion
    }
}
