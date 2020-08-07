using Shivonet.MobileShop.Core.Contracts.Services.General;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Shivonet.MobileShop.Core.Services.General
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _settings;
        private const string UserName = "UserName";
        private const string UserId = "UserId";

        public SettingsService()
        {
            _settings = CrossSettings.Current;
        }

        public void AddItem(string key, string value)
        {
            _settings.AddOrUpdateValue(key, value);
        }

        public string GetItem(string key)
        {
            var item= _settings.GetValueOrDefault(key, string.Empty);
            return item;
        }

        public string UserNameSetting
        {
            get => GetItem(UserName);
            set => AddItem(UserName, value);
        }

        public string UserIdSetting
        {
            get => GetItem(UserId);
            set => AddItem(UserId, value);
        }
    }
}
