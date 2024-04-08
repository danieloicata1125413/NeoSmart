using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace NeoSmart.App.Helpers
{

    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        #region Atributes
        private const string _isLogin = "isLogin";
        private static readonly bool _boolDefault = false;
        #endregion

        #region Properties
        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }
        #endregion
    }
}
