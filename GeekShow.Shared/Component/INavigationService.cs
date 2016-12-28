using System;

namespace GeekShow.Shared.Component
{
    public interface INavigationService
    {
        void Navigate(Type pageType);
        void Navigate(Type pageType, object param);
        void GoBack();
        void GoHome();
    }
}
