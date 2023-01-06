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
        public DbParamsPage Dbp;
        private readonly string _db;
        private CancellationTokenSource _ct;
        private GeoGoQuery _geoGoQuery;

        public FuelTransPage(DbParamsPage dbp)
        {
            Dbp = dbp;
            _db = Dbp.File;
            InitializeComponent();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys != Keys.None || keyData != Keys.Escape) return base.ProcessDialogKey(keyData);
            Close();
            return true;
        }

        private async Task ExecuteQuery()
        {
            using (_ct = new CancellationTokenSource())
            {
                try
                {
                    if (_geoGoQuery == null) _geoGoQuery = new GeoGoQuery(this, _ct.Token);
                    else _geoGoQuery.UpdateCt(_ct.Token);
                    await _geoGoQuery.GeoGoQueryAsync();
                }
                catch (OperationCanceledException) { }
                catch (Exception err)
                {
                    Program.ShowError(err);
                }
            }
            _ct = null;
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
            if (!(_ct is null))
            {
                _ct.Cancel();
                _ct.Dispose();
            }
            _geoGoQuery.Dispose();
        }

        private void InsertBtn_Click(object sender, EventArgs e) => _geoGoQuery.UpdateAccessDb();

        private async void NextBtn_Click(object sender, EventArgs e)
        {
            if (!_geoGoQuery.DbUpdated)
            {
                DialogResult selectionOk = MessageBox.Show("Current result have not been saved to the database, would you like to do so now?", "Current Results Not Saved", MessageBoxButtons.OKCancel);
                if (selectionOk == DialogResult.OK) _geoGoQuery.UpdateAccessDb();
            }
            await ExecuteQuery();
        }

        private void Preview_Click(object sender, EventArgs e) => Process.Start(_db);
    }
}