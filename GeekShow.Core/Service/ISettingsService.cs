namespace GeekShow.Core.Service
{
    public interface ISettingsService
    {
        bool ContainsKey(string key);
        void Add(string key, string value);
        void AddOrOverwrite(string key, string value);
        string Get(string key);
        string GetOrNull(string key);
        void RemoveKey(string key);
    }
}
