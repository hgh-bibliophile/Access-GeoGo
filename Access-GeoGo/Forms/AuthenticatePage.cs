using Geotab.Checkmate;
using System;
using System.Windows.Forms;

namespace Access_GeoGo.Forms
{
    public partial class AuthenticatePage : Form
    {
        public AuthenticatePage() => InitializeComponent();

        /// <summary>
        /// Geotab Account Username
        /// </summary>
        private static string Username;

        /// <summary>
        /// Geotab Account Password
        /// </summary>
        private static string Password;

        /// <summary>
        /// (Optional) Geotab database name
        /// </summary>
        private static string Database;

        /// <summary>
        /// The authenticated Geotab API object, transferred to <see cref="Program.API"/>
        /// </summary>
        private static API api;

        //On-authorization event and handeler
        public event AuthCompleteHandler AuthComplete;

        public delegate void AuthCompleteHandler(API AuthAPI, bool AuthStatus);

        //Authentication Button Enabling & Default Text Entering
        private void AuthenticatePage_Load(object sender, EventArgs e)
        {
            Username = Username_Input.Text = Program.CONFIG.UserConfig.Geotab_Auth.Username;
            Password = Password_Input.Text = Program.CONFIG.UserConfig.Geotab_Auth.Password;
            Database = Database_Input.Text = Program.CONFIG.UserConfig.Geotab_Auth.Database;
            EnableAuthenticateButton();
        }

        /// <summary>
        /// Enables the <see cref="AuthenticateButton"/> if username & password are filled in
        /// </summary>
        private void EnableAuthenticateButton()
        {
            AuthenticateButton.Enabled = !string.IsNullOrWhiteSpace(Password_Input.Text)
                && !string.IsNullOrWhiteSpace(Username_Input.Text);
        }

        /// <summary>
        /// On the <see cref="AuthenticateButton"/> click, an <see cref="API"/> object authenticated with the entered username, password, and database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AuthenticateButton_Click(object sender, EventArgs e)
        {
            Username = Username_Input.Text;
            Password = Password_Input.Text;
            Database = Database_Input.Text;
            try
            {
                api = new API(Username, Password, null, Database);
                await api.AuthenticateAsync();
                MessageBox.Show("Successfully Authenticated\nSession Id:" + api.SessionId);
                AuthComplete?.Invoke(api, true);
            }
            catch (Exception err)
            {
                Program.ShowError(err);
            }
        }

        /*Various methods to control various aspects of the form, such as the pressing of the ESC button, and the assigning of the textboxes' text
         * to their respective variables as one types*/

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        /*Checks to see if required fields have been edited.*/

        private void Input_TextChanged(object sender, EventArgs e) => EnableAuthenticateButton();
    }
}