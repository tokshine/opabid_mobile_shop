using Shivonet.MobileShop.Core.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Shivonet.MobileShop.Core.Extensions;

namespace Shivonet.MobileShop.Core.Utility
{
    public static class AppSettings
    {
        private static ISettings Settings => CrossSettings.Current;

        public static User User
        {
            get => Settings.GetValueOrDefault(nameof(User), default(User));

            set => Settings.AddOrUpdateValue(nameof(User), value);
        }
    }
}
