using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Component
{
    public interface INavigationService
    {
        void Navigate(Type pageType);
        void Navigate(Type pageType, object param);
        void GoBack();
    }
}
