using Access_GeoGo.Data.Configuration;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Access_GeoGo.Forms
{
    public partial class DBParamsPage : Form
    {
        public DBParamsPage()
        {
            InitializeComponent();
            DBFileNameBox.Text = Program.GeoGoCONFIG.Databases[Program.GeoGoCONFIG.Databases.CurrentUser].File;
        }
        static string db;
        static string constr;
        static OleDbConnection con;
        private void FindDBButton_Click(object sender, EventArgs e)
        {
            FindDBDialog.ShowDialog();
        }
        private void FindDBDialog_FileOk(object sender, CancelEventArgs e)
        {
            ClearSelection();
            TableComboBox.Items.Clear();
            TableComboBox.Text = null;

            db = DBFileNameBox.Text = FindDBDialog.FileName;
            constr = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + db + "; Persist Security Info = False";
            con = new OleDbConnection(constr);
            using (con)
            {
                try
                {
                    con.Open();
                    DataTable dt = con.GetSchema("Tables");
                    con.Close();

                    for (int i = 0; i < dt.Rows.Count; i++)
                        TableComboBox.Items.Add(dt.Rows[i][2].ToString());
                }
                catch (Exception err)
                {
                    Program.ShowError(err);
                }
            }
        }
        private void ClearSelection()
        {            
            IndexComboBox.Items.Clear();
            TimeComboBox.Items.Clear();
            VehicleComboBox.Items.Clear();
            OdometerComboBox.Items.Clear();
            IndexComboBox.Text = null;
            TimeComboBox.Text = null;
            VehicleComboBox.Text = null;
            OdometerComboBox.Text = null;
        }
        private void TableComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ClearSelection();
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

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IndexComboBox.Items.Add(dt.Rows[i][3].ToString());
                        TimeComboBox.Items.Add(dt.Rows[i][3].ToString());
                        VehicleComboBox.Items.Add(dt.Rows[i][3].ToString());
                        OdometerComboBox.Items.Add(dt.Rows[i][3].ToString());
                    }
                }
                catch (Exception err)
                {
                    Program.ShowError(err);
                }
            }
        }
        public string File;
        public decimal Limit;
        public string Table = "Transaction";
        public string Id = "Transaction_ID";
        public string Time = "TxTimestamp";
        public string Vehicle = "Vehicle";
        public string Odometer = "Odometer";
        private void DoneButton_Click(object sender, EventArgs e)
        {
            File = DBFileNameBox.Text;
            Limit = LimitEntriesBox.Value;
            Table = string.IsNullOrEmpty(TableComboBox.Text) ? "Transaction" : TableComboBox.Text;
            Id = string.IsNullOrEmpty(IndexComboBox.Text) ? "Transaction_ID" : IndexComboBox.Text;
            Time = string.IsNullOrEmpty(TimeComboBox.Text) ? "TxTimestamp" : TimeComboBox.Text;
            Vehicle = string.IsNullOrEmpty(VehicleComboBox.Text) ? "Vehicle" : VehicleComboBox.Text;
            Odometer = string.IsNullOrEmpty(OdometerComboBox.Text) ? "Odometer" : OdometerComboBox.Text;
            var selectionOK = MessageBox.Show($"Table: {Table}\nId: {Id}\nTime: {Time}\nVehicle: {Vehicle}\nOdometer: {Odometer}", "Verify Selections", MessageBoxButtons.OKCancel);
            if (!Program.CheckAuth()) return;
            if (selectionOK == DialogResult.Cancel) return;
            var results = new FuelTransPage(this);
            results.Show();
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Table = string.IsNullOrEmpty(TableComboBox.Text) ? "Transaction" : TableComboBox.Text;
            Odometer = string.IsNullOrEmpty(OdometerComboBox.Text) ? "Odometer" : OdometerComboBox.Text;
            db = DBFileNameBox.Text = FindDBDialog.FileName;
            constr = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + db + "; Persist Security Info = False";
            string query = $"UPDATE [{Table}] SET [{Odometer}] = '0';";
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
        }
    }
}
