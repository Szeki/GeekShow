using GeekShow.Component;
using GeekShow.Core.Service;
using GeekShow.Shared.Component;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;

        private RelayCommand _homeCommand;

        private bool _isNotificationSettingsOn;

        public SettingsPageViewModel(INavigationService navigationService, ITvShowService tvShowService, ISettingsService settingsService)
            : base(navigationService, tvShowService)
        {
            _settingsService = settingsService;
        }

        #region Properties

        public bool IsNotificationSettingsOn
        {
            get
            {
                return _isNotificationSettingsOn;
            }
            set
            {
                if (_isNotificationSettingsOn == value) return;

                _isNotificationSettingsOn = value;

                SettingsHelper.SetNotificationSetting(_settingsService, value);
            }
        }

        public ICommand HomeCommand
        {
            get
            {
                return _homeCommand ?? (_homeCommand = new RelayCommand(param => GoHome()));
            }
        }

        #endregion

        #region Public Methods

        public void InitializeSettings()
        {
            this.IsNotificationSettingsOn = SettingsHelper.GetNotificationSetting(_settingsService);
        }

        #endregion

        #region Private Methods

        private void GoHome()
        {
            NavigationService.GoHome();
        }

        #endregion
    }
}
