using GeekShow.Core.Service;

namespace GeekShow.Shared.Component
{
    public static class SettingsHelper
    {
        private const string IsNotificationOnSettingKey = "IsNotificationOn";

        public static bool GetNotificationSetting(ISettingsService service)
        {
            if (!service.ContainsKey(IsNotificationOnSettingKey))
            {
                service.AddOrOverwrite(IsNotificationOnSettingKey, bool.TrueString);
            }

            return bool.Parse(service.Get(IsNotificationOnSettingKey));
        }

        public static void SetNotificationSetting(ISettingsService service, bool value)
        {
            service.AddOrOverwrite(IsNotificationOnSettingKey, value.ToString());
        }
    }
}
