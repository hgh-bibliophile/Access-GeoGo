using Access_GeoGo.Data.Configuration;
using Access_GeoGo.Forms;
using Geotab.Checkmate;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Windows.Forms;

//x using static Access_GeoGo.Data.Configuration.GeoGoCDM;

namespace Access_GeoGo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (CONFIG.UserConfig == null) CONFIG.UserConfig = new GeoGo_UserConfig();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuPage());
        }

        /// <summary>
        /// True if user is authenticated.
        /// </summary>
        public static bool AuthStatus;

        /// <summary>
        /// After user authentication, contains the authenticated Geotab <see cref="Geotab.Checkmate.API"/> object
        /// </summary>
        public static API API;

        /// <summary>
        /// Configuration settings
        /// </summary>
        public static class CONFIG
        {
            /// <summary>
            /// Gets the application <see cref="Configuration"/>
            /// </summary>
            public static Configuration APP_CONFIG = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            /// <summary>
            /// Loads config.json user-specific settings
            /// </summary>
            private static readonly IConfigurationRoot JSON_CONFIG = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("config.json", optional: true).Build();

            /// <summary>
            /// Settings from config.json
            /// </summary>
            public static GeoGo_UserConfig UserConfig = JSON_CONFIG.GetSection(nameof(GeoGo_UserConfig)).Get<GeoGo_UserConfig>();

            /// <summary>
            /// Gets the <see cref="GeoGo_Users"/> section in the app.config file, and stores the data
            /// </summary>
            public static GeoGo_UserFeeds GeotabFeeds = (GeoGo_UserFeeds)APP_CONFIG.GetSection(nameof(GeoGo_UserFeeds));
        }

        /// <summary>
        /// Checks the current authentication status of the user, and returns if false
        /// </summary>
        public static bool CheckAuth()
        {
            if (AuthStatus) return true;
            MessageBox.Show("Not Authenticated");
            return false;
        }

        public static void ShowError(Exception e)
        {
            MessageBox.Show("Message: " + e.Message + "\nSource: " + e.Source + "\nTarget Site: " + e.TargetSite + "Stack:\n" + e.StackTrace);
        }
    }
}