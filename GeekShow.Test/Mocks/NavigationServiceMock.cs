using GeekShow.Shared.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Test.Mocks
{
    public class NavigationServiceMock : MockBase, INavigationService
    {
        public void GoBack()
        {
            RecordCall(nameof(INavigationService.GoBack));

            ProcessMethodAction(nameof(INavigationService.GoBack));
        }

        public void Navigate(Type pageType)
        {
            RecordCall(nameof(INavigationService.Navigate), pageType);

            ProcessMethodAction(nameof(INavigationService.Navigate));
        }

        public void Navigate(Type pageType, object param)
        {
            RecordCall(nameof(INavigationService.Navigate), pageType, param);

            ProcessMethodAction(nameof(INavigationService.Navigate));
        }
    }
}
