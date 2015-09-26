using GeekShow.Shared.Model;
using GeekShow.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Test.Mocks
{
    public class ViewModelBaseMock : ViewModelBase
    {
        public new void AddTvShowAndPersist(TvShowSubscribedItem tvShow)
        {
            base.AddTvShowAndPersist(tvShow);
        }

        public new void RemoveTvShowAndPersist(TvShowSubscribedItem tvShow)
        {
            base.RemoveTvShowAndPersist(tvShow);
        }

        public new void SaveTvShows()
        {
            base.SaveTvShows();
        }
    }
}
