using Access_GeoGo.Data.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace Access_GeoGo.Forms
{
    public partial class DbParamsPage : Form
    {
        public DbParamsPage()
        {
            InitializeComponent();
            _db = DBFileNameBox.Text = Program.Config.UserConfig.Database;
            _columnData = new Dictionary<ComboBox, string>
            {
                [IndexComboBox] = DbConfig.Columns.Id,
                [GTStatusComboBox] = DbConfig.Columns.GtStatus,
                [TimeComboBox] = DbConfig.Columns.Time,
                [VehicleComboBox] = DbConfig.Columns.Vehicle,
                [OdometerComboBox] = DbConfig.Columns.Odometer,
                [EngineHrsComboBox] = DbConfig.Columns.EngineHrs,
                [LatitudeComboBox] = DbConfig.Columns.Latitude,
                [LongitudeComboBox] = DbConfig.Columns.Longitude,
                [DriverComboBox] = DbConfig.Columns.Driver
            };
            _columnCBoxes = _columnData.Keys.ToList();
            LoadTables();
            EnableControls();
            if (Program.Config.UserConfig.Name != "Hannah") ClearBtn.Enabled = ClearBtn.Visible = false;
        }

        private string _db;
        private string _constr;
        private OleDbConnection _con;
        private static readonly GeoGoUserConfig.DbConfig DbConfig = Program.Config.UserConfig.Db_Config;
        private readonly Dictionary<ComboBox, string> _columnData;
        private readonly List<ComboBox> _columnCBoxes;

        private void EnableControls()
        {
            DoneBtn.Enabled = !string.IsNullOrWhiteSpace(DBFileNameBox.Text)
                && !string.IsNullOrWhiteSpace(TableComboBox.Text)
                && !string.IsNullOrWhiteSpace(GTSValueComboBox.Text)
                && _columnCBoxes.All(cBox => !string.IsNullOrWhiteSpace(cBox.Text));
            ClearBtn.Enabled = !string.IsNullOrWhiteSpace(DBFileNameBox.Text)
                && !string.IsNullOrWhiteSpace(TableComboBox.Text)
                && !string.IsNullOrWhiteSpace(GTStatusComboBox.Text)
                && !string.IsNullOrWhiteSpace(GTSValueComboBox.Text);
            TableComboBox.Enabled = !string.IsNullOrWhiteSpace(DBFileNameBox.Text);
            foreach (ComboBox cBox in _columnCBoxes) cBox.Enabled = !string.IsNullOrWhiteSpace(TableComboBox.Text);
        }

        private void ClearSelection()
        {
            foreach (ComboBox cBox in _columnCBoxes)
            {
                cBox.Items.Clear();
                cBox.Text = null;
            }
            EnableControls();
        }

        private void SetDefaultColumns()
        {
            foreach (KeyValuePair<ComboBox, string> column in _columnData)
                column.Key.SelectedIndex = column.Key.FindStringExact(column.Value);
            GTSValueComboBox.SelectedIndex = GTSValueComboBox.FindStringExact(DbConfig.GtStatusDefault);
        }

        private void LoadTables()
        {
            if (_db == null) return;
            _constr = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + _db + "; Persist Security Info = False";
            using (_con = new OleDbConnection(_constr))
            {
                try
                {
                    _con.Open();
                    DataTable dt = _con.GetSchema("Tables");
                    _con.Close();

                    foreach (DataRow table in dt.Rows)
                        TableComboBox.Items.Add(table[2].ToString());
                }
                catch (Exception err)
                {
                    if (_con.State == ConnectionState.Open) _con.Close();
                    Program.ShowError(err);
                }
            }
            TableComboBox.SelectedIndex = TableComboBox.FindStringExact(DbConfig.Table);
            EnableControls();
        }

        private void LoadColumns()
        {
            using (_con = new OleDbConnection(_constr))
            {
                try
                {
                    string[] restrictions = new string[3];
                    restrictions[2] = TableComboBox.Text;

                    _con.Open();
                    DataTable dt = _con.GetSchema("Columns", restrictions);
                    _con.Close();

                    foreach ((string columnName, ComboBox cBox) in from DataRow column in dt.Rows
                                                       let columnName = column[3].ToString()
                                                       from ComboBox cBox in _columnCBoxes
                                                       select (columnName, cBox))
                        cBox.Items.Add(columnName);
                }
                catch (Exception err)
                {
                    if (_con.State == ConnectionState.Open) _con.Close();
                    Program.ShowError(err);
                }
            }
            SetDefaultColumns();
            EnableControls();
        }

        private void FindDBButton_Click(object sender, EventArgs e) => FindDBDialog.ShowDialog();

        private void FindDBDialog_FileOk(object sender, CancelEventArgs e)
        {
            ClearSelection();
            TableComboBox.Items.Clear();
            TableComboBox.Text = null;

            _db = DBFileNameBox.Text = FindDBDialog.FileName;
            LoadTables();
        }

        private void TableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearSelection();
            LoadColumns();
        }

        private void CheckEnableControls(object sender, EventArgs e) => EnableControls();

        public string File;
        public decimal Limit;
        public string Table;
        public string Id;
        public string GtStatus;
        public string GtsValue;
        public string Time;
        public string Vehicle;
        public string Odometer;
        public string EngineHrs;
        public string Latitude;
        public string Longitude;
        public string Driver;

        private void DoneBtn_Click(object sender, EventArgs e)
        {
            File = DBFileNameBox.Text;
            Limit = LimitEntriesBox.Value;
            Table = TableComboBox.Text;
            Id = IndexComboBox.Text;
            GtStatus = GTStatusComboBox.Text;
            GtsValue = GTSValueComboBox.Text;
            Time = TimeComboBox.Text;
            Vehicle = VehicleComboBox.Text;
            Odometer = OdometerComboBox.Text;
            EngineHrs = EngineHrsComboBox.Text;
            Latitude = LatitudeComboBox.Text;
            Longitude = LongitudeComboBox.Text;
            Driver = DriverComboBox.Text;
            string msg = $"Table: {Table}\nGTS Record Type: {GtsValue}\n\nId: {Id}\nGT Status: {GtStatus}\nTime: {Time}\nVehicle: {Vehicle}\nOdometer: {Odometer}\nEngine Hrs: {EngineHrs}\nLatitude: {Latitude}\nLongitude: {Longitude}\nDriver: {Driver}";

            DialogResult selectionOk = MessageBox.Show(msg, "Verify Selections", MessageBoxButtons.OKCancel);
            if (!Program.CheckAuth() || selectionOk == DialogResult.Cancel) return;
            new FuelTransPage(this).Show();
        }

        /// <summary>
        /// FOR TESTING PURPOSES: resets/clears all data from the selected Odometer, Location, and Driver fields
        /// </summary>
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            DialogResult selectionOk = MessageBox.Show($"Set [{DbConfig.Columns.GtStatus}] to '{GTSValueComboBox.Text}' for ALL records?", "Confirm Action", MessageBoxButtons.OKCancel);
            if (selectionOk == DialogResult.Cancel) return;

            string query = $"UPDATE [{TableComboBox.Text}] SET [{DbConfig.Columns.GtStatus}] = '{GTSValueComboBox.Text}';";
            using (OleDbCommand cmd = new OleDbCommand(query, _con = new OleDbConnection(_constr)))
            {
                try
                {
                    _con.Open();
                    int updated = cmd.ExecuteNonQuery();
                    _con.Close();

                    MessageBox.Show($"{updated} entries updated successfully.", "DB Update Complete");
                }
                catch (Exception err)
                {
                    if (_con.State == ConnectionState.Open) _con.Close();
                    Program.ShowError(err);
                }
            }
            /*
            List<string> msg = new List<string>();
            List<string> updateSQL = new List<string>();

            Dictionary<ComboBox, string> gtFields = new Dictionary<ComboBox, string>{
                { OdometerComboBox, "0" },
                { EngineHrsComboBox, "0" },
                { LatitudeComboBox, "" },
                { LongitudeComboBox, "" },
                { DriverComboBox, "" }
            };
            foreach (KeyValuePair<ComboBox, string> field in gtFields)
            {
                if (!string.IsNullOrWhiteSpace(field.Key.Text))
                {
                    updateSQL.Add($"[{field.Key.Text}] = '{field.Value}'");
                    msg.Add(field.Key.Text);
                }
            }

            DialogResult selectionOK = MessageBox.Show(string.Join("\n", msg), "Clear All Data from Selected Fields", MessageBoxButtons.OKCancel);
            if (selectionOK == DialogResult.Cancel) return;

            string query = $"UPDATE [{TableComboBox.Text}] SET " + string.Join(" ,", updateSQL) + ";";
            con = new OleDbConnection(constr);
            using (OleDbCommand cmd = new OleDbCommand(query, con))
            {
                try
                {
                    con.Open();
                    int updated = cmd.ExecuteNonQuery();
                    MessageBox.Show($"{updated} entries updated successfully.", "DB Update Complete");
                    con.Close();
                }
                catch (Exception err)
                {
                    Program.ShowError(err);
                }
            }*/
        }
    }
}