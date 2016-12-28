using GeekShow.Core.Service;
using System;

namespace GeekShow.Shared.Component
{
    public class SettingsService : ISettingsService
    {
        #region ISettingsService implementation

        public void Add(string key, string value)
        {
            if (ContainsKey(key))
            {
                throw new Exception($"{key} already exist. Use {nameof(AddOrOverwrite)} to update the value");
            }

            Windows.Storage.ApplicationData.Current.LocalSettings.Values.Add(key, value);
        }

        public void AddOrOverwrite(string key, string value)
        {
            if (ContainsKey(key))
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values[key] = value;
            }
            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values.Add(key, value);
            }
        }

        public bool ContainsKey(string key)
        {
            return Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key);
        }

        public string Get(string key)
        {
            if (!ContainsKey(key))
            {
                throw new Exception($"{key} doesn't exist.");
            }

            return Windows.Storage.ApplicationData.Current.LocalSettings.Values[key] as string;
        }

        public string GetOrNull(string key)
        {
            return ContainsKey(key) ? (Windows.Storage.ApplicationData.Current.LocalSettings.Values[key] as string) : null;
        }

        public void RemoveKey(string key)
        {
            if (ContainsKey(key))
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove(key);
            }
        }

        #endregion
    }
}
