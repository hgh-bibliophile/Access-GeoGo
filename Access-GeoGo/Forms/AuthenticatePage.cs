using Geotab.Checkmate;
using System;
using System.Windows.Forms;
using Geotab.Checkmate.ObjectModel;

namespace Access_GeoGo.Forms
{
    public partial class AuthenticatePage : Form
    {
        /// <summary>
        /// The authenticated Geotab API object, transferred to <see cref="Program.Api"/>
        /// </summary>
        private static API _api;

        /// <summary>
        /// (Optional) Geotab database name
        /// </summary>
        private string _database;

        /// <summary>
        /// Geotab Account Password
        /// </summary>
        private string _password;

        /// <summary>
        /// Geotab Account Username
        /// </summary>
        private string _username;

        public AuthenticatePage() => InitializeComponent();

        public delegate void AuthCompleteHandler(API authApi, bool authStatus);

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
            if (ModifierKeys != Keys.None || keyData != Keys.Escape) return base.ProcessDialogKey(keyData);
            Close();
            return true;
        }

        /// <summary>
        /// On the <see cref="AuthenticateButton"/> click, generate an <see cref="API"/> object authenticated with the entered username, password, and database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AuthenticateButton_Click(object sender, EventArgs e)
        {
            _username = Username_Input.Text;
            _password = Password_Input.Text;
            _database = Database_Input.Text;
            try
            {
                _api = new API(_username, _password, null, _database);
                await _api.AuthenticateAsync();
                MessageBox.Show("Successfully Authenticated\nSession Id:" + _api.SessionId);
                AuthComplete?.Invoke(_api, true);
            }
            catch (InvalidUserException err)
            {
                MessageBox.Show(err.Message, "Authentication Failed");
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
            _username = Username_Input.Text = Program.Config.UserConfig.GeotabAuth.Username;
            _password = Password_Input.Text = Program.Config.UserConfig.GeotabAuth.Password;
            _database = Database_Input.Text = Program.Config.UserConfig.GeotabAuth.Database;
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