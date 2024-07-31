using Geotab.Checkmate;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Access_GeoGo.Forms
{
    public partial class MenuPage : Form
    {
        private AuthenticatePage _auth;

        public MenuPage() => InitializeComponent();

        private void FinishAuth(API authApi, bool authStatus)
        {
            _auth.Close();
            Autenticated_CheckBox.Checked = true;
            Userame_Label.Text = "Username:\n - " + authApi.UserName;
            Program.Api = authApi;
            Program.AuthStatus = authStatus;
        }

        private void AuthenticatePageButton_Click(object sender, EventArgs e)
        {
            _auth = new AuthenticatePage();
            _auth.AuthComplete += FinishAuth;
            _auth.Show();
        }

        private void FaultCodesBtn_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAuth()) return;
            new FaultCodesPage().Show();
        }

        private void TestPageButton_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAuth()) return;
            /*var test = new FaultCodesPage();
            test.Show();*/
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAuth()) return;
            new DbParamsPage().Show();
        }

        private void MenuPage_Load(object sender, EventArgs e)
        {
            //set version info
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            VersionLabel.Text = string.Format(VersionLabel.Text, versionInfo.ProductVersion, versionInfo.LegalCopyright);
        }

        private void VersionLabel_Click(object sender, EventArgs e)
        {
        }
    }
}