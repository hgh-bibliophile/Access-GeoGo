using Geotab.Checkmate;
using System;
using System.Windows.Forms;

namespace Access_GeoGo.Forms
{
    public partial class MenuPage : Form
    {
        private AuthenticatePage auth;

        public MenuPage() => InitializeComponent();

        private void AuthenticatePageButton_Click(object sender, EventArgs e)
        {
            auth = new AuthenticatePage();
            auth.AuthComplete += new AuthenticatePage.AuthCompleteHandler(FinishAuth);
            auth.Show();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAuth()) return;
            new DBParamsPage().Show();
        }

        public void FinishAuth(API authAPI, bool authStatus)
        {
            auth.Close();
            Autenticated_CheckBox.Checked = true;
            Userame_Label.Text = "Username:\n - " + authAPI.UserName;
            Program.API = authAPI;
            Program.AuthStatus = authStatus;
        }

        private void TestPageButton_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAuth()) return;
            /*var test = new FaultCodesPage();
            test.Show();*/
        }

        private void FaultCodesBtn_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAuth()) return;
            new FaultCodesPage().Show();
        }
    }
}