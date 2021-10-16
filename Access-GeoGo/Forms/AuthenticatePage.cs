using Geotab.Checkmate;
using System;
using System.Windows.Forms;

namespace Access_GeoGo.Forms
{
    public partial class AuthenticatePage : Form
    {
        /// <summary>
        /// The authenticated Geotab API object, transferred to <see cref="Program.API"/>
        /// </summary>
        private static API api;

        /// <summary>
        /// (Optional) Geotab database name
        /// </summary>
        private static string Database;

        /// <summary>
        /// Geotab Account Password
        /// </summary>
        private static string Password;

        /// <summary>
        /// Geotab Account Username
        /// </summary>
        private static string Username;

        public AuthenticatePage() => InitializeComponent();

        public delegate void AuthCompleteHandler(API AuthAPI, bool AuthStatus);

        /// <summary>
        /// On-authorization event and handeler
        /// </summary>
        public event AuthCompleteHandler AuthComplete;

        /// <summary>
        /// Various methods to control various aspects of the form, such as the pressing of the ESC button, and the assigning of the textboxes' text to their respective variables as one types
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// On the <see cref="AuthenticateButton"/> click, generate an <see cref="API"/> object authenticated with the entered username, password, and database
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

        /// <summary>
        /// On load, enter default text & check Authenticate Btn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Checks to see if required fields have been edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_TextChanged(object sender, EventArgs e) => EnableAuthenticateButton();
    }
}