using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonorTraceMobile
{
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

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault("email", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("email", value);
            }
        }
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("password", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("password", value);
            }
        }
        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault("token", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("token", value);
            }

        }

        public static string Id
        {
            get
            {
                return AppSettings.GetValueOrDefault("id", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("id", value);
            }

        }

        public static string Role
        {
            get
            {
                return AppSettings.GetValueOrDefault("Role", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Role", value);
            }

        }
    }
}
