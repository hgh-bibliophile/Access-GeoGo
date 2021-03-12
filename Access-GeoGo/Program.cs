using Geotab.Checkmate;
using System;
using System.Windows.Forms;
using Access_GeoGo.Forms;
using System.Configuration;
using Access_GeoGo.Data.Configuration;

namespace Access_GeoGo
{    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuPage());
        }        
        /// <summary>
        /// True if user is authenticated.
        /// </summary>
        public static bool AuthStatus = false;
        /// <summary>
        /// After user authentication, contains the authenticated Geotab <see cref="Geotab.Checkmate.API"/> object
        /// </summary>
        public static API API;
        /// <summary>
        /// Gets the application <see cref="Configuration"/>
        /// </summary>
        public static Configuration CONFIG = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        /// <summary>
        /// Gets the <see cref="GeoGoCDM"/> section in the app.config file, and stores the data
        /// </summary>
        public static GeoGoCDM GeoGoCONFIG = CONFIG.GetSection("GeoGoCDM") as GeoGoCDM;
        /// <summary>
        /// Checks the current authentication status of the user, and returns if false
        /// </summary>
        public static bool CheckAuth()
        {
            if (!Program.AuthStatus)
            {
                MessageBox.Show("Not Authenticated");
                return false;
            }
            return true;
        }
        public static void ShowError(Exception e)
        {
            //ConfigurationManager.OpenMappedExeConfiguration()
            MessageBox.Show("Message: " + e.Message + "\nSource: " + e.Source + "\nTarget Site: " + e.TargetSite+ "Stack:\n"+ e.StackTrace);
        }
    }
}
