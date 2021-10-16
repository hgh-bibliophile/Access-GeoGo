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
    public partial class DBParamsPage : Form
    {
        public DBParamsPage()
        {
            InitializeComponent();
            db = DBFileNameBox.Text = Program.CONFIG.UserConfig.Database;
            _columnData = new Dictionary<ComboBox, string>
            {
                [IndexComboBox] = dbConfig.Columns.Id,
                [GTStatusComboBox] = dbConfig.Columns.GTStatus,
                [TimeComboBox] = dbConfig.Columns.Time,
                [VehicleComboBox] = dbConfig.Columns.Vehicle,
                [OdometerComboBox] = dbConfig.Columns.Odometer,
                [EngineHrsComboBox] = dbConfig.Columns.EngineHrs,
                [LatitudeComboBox] = dbConfig.Columns.Latitude,
                [LongitudeComboBox] = dbConfig.Columns.Longitude,
                [DriverComboBox] = dbConfig.Columns.Driver
            };
            _columnCBoxes = _columnData.Keys.ToList();
            LoadTables(db);
            EnableControls();
            if (Program.CONFIG.UserConfig.Name != "Hannah") ClearBtn.Enabled = ClearBtn.Visible = false;
        }

        private static string db;
        private static string constr;
        private static OleDbConnection con;
        private readonly GeoGo_UserConfig.DBConfig dbConfig = Program.CONFIG.UserConfig.DB_Config;
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
            GTSValueComboBox.SelectedIndex = GTSValueComboBox.FindStringExact(dbConfig.GTStatus_Default);
        }

        private void LoadTables(string db)
        {
            if (db == null) return;
            constr = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + db + "; Persist Security Info = False";
            con = new OleDbConnection(constr);
            using (con)
            {
                try
                {
                    con.Open();
                    DataTable dt = con.GetSchema("Tables");
                    con.Close();

                    foreach (DataRow table in dt.Rows)
                        TableComboBox.Items.Add(table[2].ToString());
                }
                catch (Exception err)
                {
                    Program.ShowError(err);
                }
            }
            TableComboBox.SelectedIndex = TableComboBox.FindStringExact(dbConfig.Table);
            EnableControls();
        }

        private void LoadColumns()
        {
            con = new OleDbConnection(constr);
            using (con)
            {
                try
                {
                    con.Open();
                    string[] restrictions = new string[3];
                    restrictions[2] = TableComboBox.Text;
                    DataTable dt = con.GetSchema("Columns", restrictions);
                    con.Close();

                    foreach (var (columnName, cBox) in from DataRow column in dt.Rows
                                                       let columnName = column[3].ToString()
                                                       from ComboBox cBox in _columnCBoxes
                                                       select (columnName, cBox))
                        cBox.Items.Add(columnName);
                }
                catch (Exception err)
                {
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

            db = DBFileNameBox.Text = FindDBDialog.FileName;
            LoadTables(db);
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
        public string GTStatus;
        public string GTS_Value;
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
            GTStatus = GTStatusComboBox.Text;
            GTS_Value = GTSValueComboBox.Text;
            Time = TimeComboBox.Text;
            Vehicle = VehicleComboBox.Text;
            Odometer = OdometerComboBox.Text;
            EngineHrs = EngineHrsComboBox.Text;
            Latitude = LatitudeComboBox.Text;
            Longitude = LongitudeComboBox.Text;
            Driver = DriverComboBox.Text;
            string msg = $"Table: {Table}\nGTS Record Type: {GTS_Value}\n\nId: {Id}\nGT Status: {GTStatus}\nTime: {Time}\nVehicle: {Vehicle}\nOdometer: {Odometer}\nEngine Hrs: {EngineHrs}\nLatitude: {Latitude}\nLongitude: {Longitude}\nDriver: {Driver}";

            DialogResult selectionOK = MessageBox.Show(msg, "Verify Selections", MessageBoxButtons.OKCancel);
            if (!Program.CheckAuth() || selectionOK == DialogResult.Cancel) return;
            new FuelTransPage(this).Show();
        }

        /// <summary>
        /// FOR TESTING PURPOSES: resets/clears all data from the selected Odometer, Location, and Driver fields
        /// </summary>
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            DialogResult selectionOK = MessageBox.Show($"Set [{dbConfig.Columns.GTStatus}] to '{GTSValueComboBox.Text}' for ALL records?", "Confirm Action", MessageBoxButtons.OKCancel);
            if (selectionOK == DialogResult.Cancel) return;

            string query = $"UPDATE [{TableComboBox.Text}] SET [{dbConfig.Columns.GTStatus}] = '{GTSValueComboBox.Text}';";
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