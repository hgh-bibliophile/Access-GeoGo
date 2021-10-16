using Access_GeoGo.Data;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Access_GeoGo.Forms
{
    public partial class FuelTransPage : Form
    {
        public DBParamsPage DBP;
        private static string db;
        private CancellationTokenSource CT;
        private GeoGoQuery GeoGoQuery;

        public FuelTransPage(DBParamsPage dbp)
        {
            DBP = dbp;
            db = DBP.File;
            InitializeComponent();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private async Task ExecuteQuery()
        {
            using (CT = new CancellationTokenSource())
            {
                try
                {
                    if (GeoGoQuery == null) GeoGoQuery = new GeoGoQuery(this, CT.Token);
                    else GeoGoQuery.UpdateCT(CT.Token);
                    await GeoGoQuery.GeoGoQueryAsync();
                }
                catch (OperationCanceledException) { }
                catch (Exception err)
                {
                    Program.ShowError(err);
                }
            }
            CT = null;
        }

        /// <summary>
        /// On load, run the GeoTab query
        /// </summary>
        private async void FuelTransPage_Load(object sender, EventArgs e) => await ExecuteQuery();

        private void GeoGoDataView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ClientSize = new Size(Math.Max(GeoGoDataView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 20, ClientSize.Width), ClientSize.Height);
            MinimumSize = SizeFromClientSize(ClientSize);
        }

        private void GeoGoPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void GeoGoPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(CT is null))
            {
                CT.Cancel();
                CT.Dispose();
            }
            GeoGoQuery.Dispose();
        }

        private void InsertBtn_Click(object sender, EventArgs e) => GeoGoQuery.UpdateAccessDB();

        private async void NextBtn_Click(object sender, EventArgs e) => await ExecuteQuery();

        private void Preview_Click(object sender, EventArgs e) => Process.Start(@db);
    }
}