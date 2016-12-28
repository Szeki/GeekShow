using GeekShow.Shared.Component;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GeekShow.Component
{
    public class NavigationService : INavigationService
    {
        public void Navigate(Type pageType)
        {
            ((Frame)(Window.Current.Content)).Navigate(pageType);
        }

        public void Navigate(Type pageType, object param)
        {
            ((Frame)(Window.Current.Content)).Navigate(pageType, param);
        }

        public void GoBack()
        {
            ((Frame)(Window.Current.Content)).GoBack();
        }

        public void GoHome()
        {
            ((Frame)(Window.Current.Content)).Navigate(typeof(MainPage));
        }
    }
}
