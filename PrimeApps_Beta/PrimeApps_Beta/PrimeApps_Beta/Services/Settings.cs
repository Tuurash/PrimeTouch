using System;
using System.Collections.Generic;
using System.Text;
// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PrimeApps_Beta.Services
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        //Username
        private const string SavedUserNameSettingsKey = "Last_username_key";

        //Password
        private const string SavedPasswordSettingsKey = "settings_key";

        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string SavedUserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(SavedUserNameSettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SavedUserNameSettingsKey, value);
            }
        }

        public static string SavedPassword
        {
            get
            {
                return AppSettings.GetValueOrDefault(SavedPasswordSettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SavedPasswordSettingsKey, value);
            }
        }

    }
}
