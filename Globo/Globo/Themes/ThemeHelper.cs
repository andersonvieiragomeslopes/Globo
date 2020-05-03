using Globo.Model;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Globo.Themes
{
    public static class ThemeHelper
    {

        /// <summary>
        /// Get's and applies the users preferred Theme
        /// </summary>
        public static void GetAppTheme()
        {
            string theme = Settings.GetSetting(Settings.AppPrefrences.AppTheme);

            if (theme != null)
            {
                var appTheme = Enums.ConvertToEnum<Settings.Theme>(theme);

                switch (appTheme)
                {
                    case Settings.Theme.LightTheme:
                        ChangeToLightTheme();
                        break;
                    case Settings.Theme.DarkTheme:
                        ChangeToDarkTheme();
                        break;
                    case Settings.Theme.SystemPreferred:
                        ChangeToSystemPreferredTheme();
                        break;
                    default:
                        ChangeToSystemPreferredTheme();
                        break;
                }
            }
            else
            {
                ChangeToSystemPreferredTheme();
            }
        }

        /// <summary>
        /// Changes theme to Light Theme
        /// </summary>
        public static void ChangeToLightTheme()
        {
            Application.Current.Resources = new LightTheme();
            Settings.AddSetting(Settings.AppPrefrences.AppTheme, Enums.ConvertToString(Settings.Theme.LightTheme));
        }

        /// <summary>
        /// Changes to Dark Theme
        /// </summary>
        public static void ChangeToDarkTheme()
        {
            Application.Current.Resources = new DarkTheme();
            Settings.AddSetting(Settings.AppPrefrences.AppTheme, Enums.ConvertToString(Settings.Theme.DarkTheme));
        }

        /// <summary>
        /// Changes to SystemPreferred Theme
        /// </summary>
        public static void ChangeToSystemPreferredTheme()
        {
            Xamarin.Essentials.AppTheme appTheme = AppInfo.RequestedTheme;

            if (appTheme == Xamarin.Essentials.AppTheme.Dark)
            {
                ChangeToDarkTheme();
            }
            else if (appTheme == Xamarin.Essentials.AppTheme.Light)
            {
                ChangeToLightTheme();
            }
            else
            {
                ChangeToLightTheme();
            }

            Settings.AddSetting(Settings.AppPrefrences.AppTheme, Enums.ConvertToString(Settings.Theme.SystemPreferred));
        }
    }
}