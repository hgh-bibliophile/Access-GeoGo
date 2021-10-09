using Access_GeoGo.Data.Configuration;
using System.Configuration;

namespace Access_GeoGo.Forms
{
    partial class DBParamsPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBParamsPage));
            this.FindDBDialog = new System.Windows.Forms.OpenFileDialog();
            this.FindDBButton = new System.Windows.Forms.Button();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.DBTableLabel = new System.Windows.Forms.Label();
            this.IndexColumnLabel = new System.Windows.Forms.Label();
            this.TableComboBox = new System.Windows.Forms.ComboBox();
            this.IndexComboBox = new System.Windows.Forms.ComboBox();
            this.OdometerComboBox = new System.Windows.Forms.ComboBox();
            this.OdometerColumnLabel = new System.Windows.Forms.Label();
            this.VehicleComboBox = new System.Windows.Forms.ComboBox();
            this.VehicleColumnLabel = new System.Windows.Forms.Label();
            this.TimeComboBox = new System.Windows.Forms.ComboBox();
            this.TimestampColumnLabel = new System.Windows.Forms.Label();
            this.DoneButton = new System.Windows.Forms.Button();
            this.LimitEntriesBox = new System.Windows.Forms.NumericUpDown();
            this.NumEntriesLabel = new System.Windows.Forms.Label();
            this.MaxEntriesLabel = new System.Windows.Forms.Label();
            this.TableBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.IndexBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.TimeBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.VehicleBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.OdometerBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DBFileNameBox = new System.Windows.Forms.TextBox();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.LocationComboBox = new System.Windows.Forms.ComboBox();
            this.LocationColumnLabel = new System.Windows.Forms.Label();
            this.DriverComboBox = new System.Windows.Forms.ComboBox();
            this.DriverColumnLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LimitEntriesBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FindDBDialog
            // 
            this.FindDBDialog.DefaultExt = "accdb";
            this.FindDBDialog.Filter = "Access Database (*.accdb)|*.accdb";
            this.FindDBDialog.Title = "Open Access Database";
            this.FindDBDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.FindDBDialog_FileOk);
            // 
            // FindDBButton
            // 
            this.FindDBButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindDBButton.Location = new System.Drawing.Point(265, 12);
            this.FindDBButton.Name = "FindDBButton";
            this.FindDBButton.Size = new System.Drawing.Size(67, 22);
            this.FindDBButton.TabIndex = 1;
            this.FindDBButton.Text = "Open DB";
            this.FindDBButton.UseVisualStyleBackColor = true;
            this.FindDBButton.Click += new System.EventHandler(this.FindDBButton_Click);
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.Location = new System.Drawing.Point(12, 13);
            this.DatabaseLabel.Name = "DatabaseLabel";
            this.DatabaseLabel.Size = new System.Drawing.Size(103, 20);
            this.DatabaseLabel.TabIndex = 3;
            this.DatabaseLabel.Text = "Database";
            this.DatabaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DBTableLabel
            // 
            this.DBTableLabel.Location = new System.Drawing.Point(12, 39);
            this.DBTableLabel.Name = "DBTableLabel";
            this.DBTableLabel.Size = new System.Drawing.Size(103, 20);
            this.DBTableLabel.TabIndex = 6;
            this.DBTableLabel.Text = "Table";
            this.DBTableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IndexColumnLabel
            // 
            this.IndexColumnLabel.Location = new System.Drawing.Point(12, 66);
            this.IndexColumnLabel.Name = "IndexColumnLabel";
            this.IndexColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.IndexColumnLabel.TabIndex = 9;
            this.IndexColumnLabel.Text = "Index Column";
            this.IndexColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TableComboBox
            // 
            this.TableComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TableComboBox.FormattingEnabled = true;
            this.TableComboBox.Location = new System.Drawing.Point(121, 38);
            this.TableComboBox.Name = "TableComboBox";
            this.TableComboBox.Size = new System.Drawing.Size(138, 21);
            this.TableComboBox.TabIndex = 10;
            this.TableBoxToolTip.SetToolTip(this.TableComboBox, "Transaction");
            this.TableComboBox.SelectionChangeCommitted += new System.EventHandler(this.TableComboBox_SelectionChangeCommitted);
            // 
            // IndexComboBox
            // 
            this.IndexComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IndexComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IndexComboBox.FormattingEnabled = true;
            this.IndexComboBox.Location = new System.Drawing.Point(121, 65);
            this.IndexComboBox.Name = "IndexComboBox";
            this.IndexComboBox.Size = new System.Drawing.Size(138, 21);
            this.IndexComboBox.TabIndex = 11;
            this.IndexBoxToolTip.SetToolTip(this.IndexComboBox, "Transaction_ID");
            // 
            // OdometerComboBox
            // 
            this.OdometerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OdometerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OdometerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OdometerComboBox.FormattingEnabled = true;
            this.OdometerComboBox.Location = new System.Drawing.Point(121, 145);
            this.OdometerComboBox.Name = "OdometerComboBox";
            this.OdometerComboBox.Size = new System.Drawing.Size(138, 21);
            this.OdometerComboBox.TabIndex = 14;
            this.OdometerBoxToolTip.SetToolTip(this.OdometerComboBox, "Odometer");
            // 
            // OdometerColumnLabel
            // 
            this.OdometerColumnLabel.Location = new System.Drawing.Point(12, 146);
            this.OdometerColumnLabel.Name = "OdometerColumnLabel";
            this.OdometerColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.OdometerColumnLabel.TabIndex = 12;
            this.OdometerColumnLabel.Text = "Odometer Column";
            this.OdometerColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VehicleComboBox
            // 
            this.VehicleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VehicleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VehicleComboBox.FormattingEnabled = true;
            this.VehicleComboBox.Location = new System.Drawing.Point(121, 118);
            this.VehicleComboBox.Name = "VehicleComboBox";
            this.VehicleComboBox.Size = new System.Drawing.Size(138, 21);
            this.VehicleComboBox.TabIndex = 13;
            this.VehicleBoxToolTip.SetToolTip(this.VehicleComboBox, "Vehicle");
            // 
            // VehicleColumnLabel
            // 
            this.VehicleColumnLabel.Location = new System.Drawing.Point(12, 119);
            this.VehicleColumnLabel.Name = "VehicleColumnLabel";
            this.VehicleColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.VehicleColumnLabel.TabIndex = 14;
            this.VehicleColumnLabel.Text = "Vehicle Column";
            this.VehicleColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TimeComboBox
            // 
            this.TimeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimeComboBox.FormattingEnabled = true;
            this.TimeComboBox.Location = new System.Drawing.Point(121, 91);
            this.TimeComboBox.Name = "TimeComboBox";
            this.TimeComboBox.Size = new System.Drawing.Size(138, 21);
            this.TimeComboBox.TabIndex = 12;
            this.TimeBoxToolTip.SetToolTip(this.TimeComboBox, "TxTimestamp");
            // 
            // TimestampColumnLabel
            // 
            this.TimestampColumnLabel.Location = new System.Drawing.Point(12, 92);
            this.TimestampColumnLabel.Name = "TimestampColumnLabel";
            this.TimestampColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.TimestampColumnLabel.TabIndex = 16;
            this.TimestampColumnLabel.Text = "Timestamp Column";
            this.TimestampColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.Location = new System.Drawing.Point(12, 257);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(320, 41);
            this.DoneButton.TabIndex = 15;
            this.DoneButton.Text = "Query Data";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // LimitEntriesBox
            // 
            this.LimitEntriesBox.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.LimitEntriesBox.Location = new System.Drawing.Point(121, 229);
            this.LimitEntriesBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.LimitEntriesBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LimitEntriesBox.Name = "LimitEntriesBox";
            this.LimitEntriesBox.Size = new System.Drawing.Size(48, 20);
            this.LimitEntriesBox.TabIndex = 17;
            this.LimitEntriesBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // NumEntriesLabel
            // 
            this.NumEntriesLabel.Location = new System.Drawing.Point(12, 227);
            this.NumEntriesLabel.Name = "NumEntriesLabel";
            this.NumEntriesLabel.Size = new System.Drawing.Size(103, 20);
            this.NumEntriesLabel.TabIndex = 18;
            this.NumEntriesLabel.Text = "Limit Entries";
            this.NumEntriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MaxEntriesLabel
            // 
            this.MaxEntriesLabel.Location = new System.Drawing.Point(175, 227);
            this.MaxEntriesLabel.Name = "MaxEntriesLabel";
            this.MaxEntriesLabel.Size = new System.Drawing.Size(157, 20);
            this.MaxEntriesLabel.TabIndex = 19;
            this.MaxEntriesLabel.Text = "Max 1000; 500 or less preferred";
            this.MaxEntriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TableBoxToolTip
            // 
            this.TableBoxToolTip.ToolTipTitle = "Default";
            // 
            // IndexBoxToolTip
            // 
            this.IndexBoxToolTip.ToolTipTitle = "Default";
            // 
            // TimeBoxToolTip
            // 
            this.TimeBoxToolTip.ToolTipTitle = "Default";
            // 
            // VehicleBoxToolTip
            // 
            this.VehicleBoxToolTip.ToolTipTitle = "Default";
            // 
            // OdometerBoxToolTip
            // 
            this.OdometerBoxToolTip.ToolTipTitle = "Default";
            // 
            // DBFileNameBox
            // 
            this.DBFileNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DBFileNameBox.Location = new System.Drawing.Point(121, 13);
            this.DBFileNameBox.Name = "DBFileNameBox";
            this.DBFileNameBox.Size = new System.Drawing.Size(138, 20);
            this.DBFileNameBox.TabIndex = 2;
            // 
            // ClearBtn
            // 
            this.ClearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearBtn.Location = new System.Drawing.Point(265, 37);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(67, 22);
            this.ClearBtn.TabIndex = 20;
            this.ClearBtn.Text = "Clear Odometer";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // LocationComboBox
            // 
            this.LocationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LocationComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LocationComboBox.FormattingEnabled = true;
            this.LocationComboBox.Location = new System.Drawing.Point(121, 172);
            this.LocationComboBox.Name = "LocationComboBox";
            this.LocationComboBox.Size = new System.Drawing.Size(138, 21);
            this.LocationComboBox.TabIndex = 22;
            this.OdometerBoxToolTip.SetToolTip(this.LocationComboBox, "Odometer");
            // 
            // LocationColumnLabel
            // 
            this.LocationColumnLabel.Location = new System.Drawing.Point(12, 173);
            this.LocationColumnLabel.Name = "LocationColumnLabel";
            this.LocationColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.LocationColumnLabel.TabIndex = 21;
            this.LocationColumnLabel.Text = "Location Column";
            this.LocationColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DriverComboBox
            // 
            this.DriverComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DriverComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DriverComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DriverComboBox.FormattingEnabled = true;
            this.DriverComboBox.Location = new System.Drawing.Point(121, 199);
            this.DriverComboBox.Name = "DriverComboBox";
            this.DriverComboBox.Size = new System.Drawing.Size(138, 21);
            this.DriverComboBox.TabIndex = 24;
            this.OdometerBoxToolTip.SetToolTip(this.DriverComboBox, "Odometer");
            // 
            // DriverColumnLabel
            // 
            this.DriverColumnLabel.Location = new System.Drawing.Point(12, 200);
            this.DriverColumnLabel.Name = "DriverColumnLabel";
            this.DriverColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.DriverColumnLabel.TabIndex = 23;
            this.DriverColumnLabel.Text = "Driver Column";
            this.DriverColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DBParamsPage
            // 
            this.AcceptButton = this.DoneButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(344, 310);
            this.Controls.Add(this.DriverComboBox);
            this.Controls.Add(this.DriverColumnLabel);
            this.Controls.Add(this.LocationComboBox);
            this.Controls.Add(this.LocationColumnLabel);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.LimitEntriesBox);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.TimeComboBox);
            this.Controls.Add(this.TimestampColumnLabel);
            this.Controls.Add(this.VehicleComboBox);
            this.Controls.Add(this.VehicleColumnLabel);
            this.Controls.Add(this.OdometerComboBox);
            this.Controls.Add(this.OdometerColumnLabel);
            this.Controls.Add(this.IndexComboBox);
            this.Controls.Add(this.TableComboBox);
            this.Controls.Add(this.IndexColumnLabel);
            this.Controls.Add(this.DBTableLabel);
            this.Controls.Add(this.DatabaseLabel);
            this.Controls.Add(this.DBFileNameBox);
            this.Controls.Add(this.FindDBButton);
            this.Controls.Add(this.NumEntriesLabel);
            this.Controls.Add(this.MaxEntriesLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBParamsPage";
            this.Text = "Database Selection";
            ((System.ComponentModel.ISupportInitialize)(this.LimitEntriesBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog FindDBDialog;
        private System.Windows.Forms.Button FindDBButton;
        private System.Windows.Forms.TextBox DBFileNameBox;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.Label DBTableLabel;
        private System.Windows.Forms.Label IndexColumnLabel;
        private System.Windows.Forms.ComboBox TableComboBox;
        private System.Windows.Forms.ComboBox IndexComboBox;
        private System.Windows.Forms.ComboBox OdometerComboBox;
        private System.Windows.Forms.Label OdometerColumnLabel;
        private System.Windows.Forms.ComboBox VehicleComboBox;
        private System.Windows.Forms.Label VehicleColumnLabel;
        private System.Windows.Forms.ComboBox TimeComboBox;
        private System.Windows.Forms.Label TimestampColumnLabel;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.NumericUpDown LimitEntriesBox;
        private System.Windows.Forms.Label NumEntriesLabel;
        private System.Windows.Forms.Label MaxEntriesLabel;
        private System.Windows.Forms.ToolTip TableBoxToolTip;
        private System.Windows.Forms.ToolTip IndexBoxToolTip;
        private System.Windows.Forms.ToolTip TimeBoxToolTip;
        private System.Windows.Forms.ToolTip VehicleBoxToolTip;
        private System.Windows.Forms.ToolTip OdometerBoxToolTip;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.ComboBox LocationComboBox;
        private System.Windows.Forms.Label LocationColumnLabel;
        private System.Windows.Forms.ComboBox DriverComboBox;
        private System.Windows.Forms.Label DriverColumnLabel;
    }
}