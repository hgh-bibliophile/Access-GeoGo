﻿using Access_GeoGo.Data.Configuration;
using System.Configuration;

namespace Access_GeoGo.Forms
{
    partial class DbParamsPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbParamsPage));
            this.FindDBDialog = new System.Windows.Forms.OpenFileDialog();
            this.FindBtn = new System.Windows.Forms.Button();
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
            this.DoneBtn = new System.Windows.Forms.Button();
            this.LimitEntriesBox = new System.Windows.Forms.NumericUpDown();
            this.NumEntriesLabel = new System.Windows.Forms.Label();
            this.MaxEntriesLabel = new System.Windows.Forms.Label();
            this.LatitudeComboBox = new System.Windows.Forms.ComboBox();
            this.DriverComboBox = new System.Windows.Forms.ComboBox();
            this.LongitudeComboBox = new System.Windows.Forms.ComboBox();
            this.EngineHrsComboBox = new System.Windows.Forms.ComboBox();
            this.DBFileNameBox = new System.Windows.Forms.TextBox();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.LatitudeColumnLabel = new System.Windows.Forms.Label();
            this.DriverColumnLabel = new System.Windows.Forms.Label();
            this.LongitudeColumnLabel = new System.Windows.Forms.Label();
            this.EngineHrsColumnLabel = new System.Windows.Forms.Label();
            this.GTStatusComboBox = new System.Windows.Forms.ComboBox();
            this.GTStatusColumnLabel = new System.Windows.Forms.Label();
            this.GTSValueComboBox = new System.Windows.Forms.ComboBox();
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
            // FindBtn
            // 
            this.FindBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindBtn.Location = new System.Drawing.Point(265, 12);
            this.FindBtn.Name = "FindBtn";
            this.FindBtn.Size = new System.Drawing.Size(67, 22);
            this.FindBtn.TabIndex = 1;
            this.FindBtn.Text = "Find DB";
            this.FindBtn.UseVisualStyleBackColor = true;
            this.FindBtn.Click += new System.EventHandler(this.FindDBButton_Click);
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
            this.TableComboBox.TabIndex = 3;
            this.TableComboBox.SelectedIndexChanged += new System.EventHandler(this.TableComboBox_SelectedIndexChanged);
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
            this.IndexComboBox.TabIndex = 4;
            this.IndexComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // OdometerComboBox
            // 
            this.OdometerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OdometerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OdometerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OdometerComboBox.FormattingEnabled = true;
            this.OdometerComboBox.Location = new System.Drawing.Point(121, 173);
            this.OdometerComboBox.Name = "OdometerComboBox";
            this.OdometerComboBox.Size = new System.Drawing.Size(138, 21);
            this.OdometerComboBox.TabIndex = 7;
            this.OdometerComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // OdometerColumnLabel
            // 
            this.OdometerColumnLabel.Location = new System.Drawing.Point(12, 174);
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
            this.VehicleComboBox.Location = new System.Drawing.Point(121, 146);
            this.VehicleComboBox.Name = "VehicleComboBox";
            this.VehicleComboBox.Size = new System.Drawing.Size(138, 21);
            this.VehicleComboBox.TabIndex = 6;
            this.VehicleComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // VehicleColumnLabel
            // 
            this.VehicleColumnLabel.Location = new System.Drawing.Point(12, 147);
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
            this.TimeComboBox.Location = new System.Drawing.Point(121, 119);
            this.TimeComboBox.Name = "TimeComboBox";
            this.TimeComboBox.Size = new System.Drawing.Size(138, 21);
            this.TimeComboBox.TabIndex = 5;
            this.TimeComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // TimestampColumnLabel
            // 
            this.TimestampColumnLabel.Location = new System.Drawing.Point(12, 120);
            this.TimestampColumnLabel.Name = "TimestampColumnLabel";
            this.TimestampColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.TimestampColumnLabel.TabIndex = 16;
            this.TimestampColumnLabel.Text = "Timestamp Column";
            this.TimestampColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DoneBtn
            // 
            this.DoneBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneBtn.Location = new System.Drawing.Point(12, 339);
            this.DoneBtn.Name = "DoneBtn";
            this.DoneBtn.Size = new System.Drawing.Size(320, 41);
            this.DoneBtn.TabIndex = 14;
            this.DoneBtn.Text = "Query Data";
            this.DoneBtn.UseVisualStyleBackColor = true;
            this.DoneBtn.Click += new System.EventHandler(this.DoneBtn_Click);
            // 
            // LimitEntriesBox
            // 
            this.LimitEntriesBox.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.LimitEntriesBox.Location = new System.Drawing.Point(121, 311);
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
            this.LimitEntriesBox.TabIndex = 12;
            this.LimitEntriesBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // NumEntriesLabel
            // 
            this.NumEntriesLabel.Location = new System.Drawing.Point(12, 309);
            this.NumEntriesLabel.Name = "NumEntriesLabel";
            this.NumEntriesLabel.Size = new System.Drawing.Size(103, 20);
            this.NumEntriesLabel.TabIndex = 18;
            this.NumEntriesLabel.Text = "Limit Entries";
            this.NumEntriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MaxEntriesLabel
            // 
            this.MaxEntriesLabel.Location = new System.Drawing.Point(175, 309);
            this.MaxEntriesLabel.Name = "MaxEntriesLabel";
            this.MaxEntriesLabel.Size = new System.Drawing.Size(157, 20);
            this.MaxEntriesLabel.TabIndex = 19;
            this.MaxEntriesLabel.Text = "Max 1000; 500 or less preferred";
            this.MaxEntriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LatitudeComboBox
            // 
            this.LatitudeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LatitudeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LatitudeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LatitudeComboBox.FormattingEnabled = true;
            this.LatitudeComboBox.Location = new System.Drawing.Point(121, 227);
            this.LatitudeComboBox.Name = "LatitudeComboBox";
            this.LatitudeComboBox.Size = new System.Drawing.Size(138, 21);
            this.LatitudeComboBox.TabIndex = 9;
            this.LatitudeComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // DriverComboBox
            // 
            this.DriverComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DriverComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DriverComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DriverComboBox.FormattingEnabled = true;
            this.DriverComboBox.Location = new System.Drawing.Point(121, 281);
            this.DriverComboBox.Name = "DriverComboBox";
            this.DriverComboBox.Size = new System.Drawing.Size(138, 21);
            this.DriverComboBox.TabIndex = 11;
            this.DriverComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // LongitudeComboBox
            // 
            this.LongitudeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LongitudeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LongitudeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LongitudeComboBox.FormattingEnabled = true;
            this.LongitudeComboBox.Location = new System.Drawing.Point(121, 254);
            this.LongitudeComboBox.Name = "LongitudeComboBox";
            this.LongitudeComboBox.Size = new System.Drawing.Size(138, 21);
            this.LongitudeComboBox.TabIndex = 10;
            this.LongitudeComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // EngineHrsComboBox
            // 
            this.EngineHrsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EngineHrsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EngineHrsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.EngineHrsComboBox.FormattingEnabled = true;
            this.EngineHrsComboBox.Location = new System.Drawing.Point(121, 200);
            this.EngineHrsComboBox.Name = "EngineHrsComboBox";
            this.EngineHrsComboBox.Size = new System.Drawing.Size(138, 21);
            this.EngineHrsComboBox.TabIndex = 8;
            this.EngineHrsComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
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
            this.ClearBtn.Size = new System.Drawing.Size(67, 49);
            this.ClearBtn.TabIndex = 13;
            this.ClearBtn.Text = "Reset GT Status";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // LatitudeColumnLabel
            // 
            this.LatitudeColumnLabel.Location = new System.Drawing.Point(12, 228);
            this.LatitudeColumnLabel.Name = "LatitudeColumnLabel";
            this.LatitudeColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.LatitudeColumnLabel.TabIndex = 21;
            this.LatitudeColumnLabel.Text = "Latitude Column";
            this.LatitudeColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DriverColumnLabel
            // 
            this.DriverColumnLabel.Location = new System.Drawing.Point(12, 282);
            this.DriverColumnLabel.Name = "DriverColumnLabel";
            this.DriverColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.DriverColumnLabel.TabIndex = 23;
            this.DriverColumnLabel.Text = "Driver Column";
            this.DriverColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LongitudeColumnLabel
            // 
            this.LongitudeColumnLabel.Location = new System.Drawing.Point(12, 255);
            this.LongitudeColumnLabel.Name = "LongitudeColumnLabel";
            this.LongitudeColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.LongitudeColumnLabel.TabIndex = 25;
            this.LongitudeColumnLabel.Text = "Longitude Column";
            this.LongitudeColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EngineHrsColumnLabel
            // 
            this.EngineHrsColumnLabel.Location = new System.Drawing.Point(12, 201);
            this.EngineHrsColumnLabel.Name = "EngineHrsColumnLabel";
            this.EngineHrsColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.EngineHrsColumnLabel.TabIndex = 27;
            this.EngineHrsColumnLabel.Text = "Engine Hrs Column";
            this.EngineHrsColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GTStatusComboBox
            // 
            this.GTStatusComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GTStatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GTStatusComboBox.FormattingEnabled = true;
            this.GTStatusComboBox.Location = new System.Drawing.Point(121, 92);
            this.GTStatusComboBox.Name = "GTStatusComboBox";
            this.GTStatusComboBox.Size = new System.Drawing.Size(138, 21);
            this.GTStatusComboBox.TabIndex = 28;
            this.GTStatusComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // GTStatusColumnLabel
            // 
            this.GTStatusColumnLabel.Location = new System.Drawing.Point(12, 93);
            this.GTStatusColumnLabel.Name = "GTStatusColumnLabel";
            this.GTStatusColumnLabel.Size = new System.Drawing.Size(103, 20);
            this.GTStatusColumnLabel.TabIndex = 29;
            this.GTStatusColumnLabel.Text = "GT Status Column";
            this.GTStatusColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GTSValueComboBox
            // 
            this.GTSValueComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GTSValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GTSValueComboBox.FormattingEnabled = true;
            this.GTSValueComboBox.Items.AddRange(new object[] {
            "New",
            "Test"});
            this.GTSValueComboBox.Location = new System.Drawing.Point(265, 92);
            this.GTSValueComboBox.Name = "GTSValueComboBox";
            this.GTSValueComboBox.Size = new System.Drawing.Size(67, 21);
            this.GTSValueComboBox.TabIndex = 30;
            this.GTSValueComboBox.SelectedIndexChanged += new System.EventHandler(this.CheckEnableControls);
            // 
            // DBParamsPage
            // 
            this.AcceptButton = this.DoneBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(344, 392);
            this.Controls.Add(this.GTSValueComboBox);
            this.Controls.Add(this.GTStatusComboBox);
            this.Controls.Add(this.GTStatusColumnLabel);
            this.Controls.Add(this.EngineHrsComboBox);
            this.Controls.Add(this.EngineHrsColumnLabel);
            this.Controls.Add(this.LongitudeComboBox);
            this.Controls.Add(this.LongitudeColumnLabel);
            this.Controls.Add(this.DriverComboBox);
            this.Controls.Add(this.DriverColumnLabel);
            this.Controls.Add(this.LatitudeComboBox);
            this.Controls.Add(this.LatitudeColumnLabel);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.LimitEntriesBox);
            this.Controls.Add(this.DoneBtn);
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
            this.Controls.Add(this.FindBtn);
            this.Controls.Add(this.NumEntriesLabel);
            this.Controls.Add(this.MaxEntriesLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DbParamsPage";
            this.Text = "Database Selection";
            ((System.ComponentModel.ISupportInitialize)(this.LimitEntriesBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog FindDBDialog;
        private System.Windows.Forms.Button FindBtn;
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
        private System.Windows.Forms.Button DoneBtn;
        private System.Windows.Forms.NumericUpDown LimitEntriesBox;
        private System.Windows.Forms.Label NumEntriesLabel;
        private System.Windows.Forms.Label MaxEntriesLabel;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.ComboBox LatitudeComboBox;
        private System.Windows.Forms.Label LatitudeColumnLabel;
        private System.Windows.Forms.ComboBox DriverComboBox;
        private System.Windows.Forms.Label DriverColumnLabel;
        private System.Windows.Forms.ComboBox LongitudeComboBox;
        private System.Windows.Forms.Label LongitudeColumnLabel;
        private System.Windows.Forms.ComboBox EngineHrsComboBox;
        private System.Windows.Forms.Label EngineHrsColumnLabel;
        private System.Windows.Forms.ComboBox GTStatusComboBox;
        private System.Windows.Forms.Label GTStatusColumnLabel;
        private System.Windows.Forms.ComboBox GTSValueComboBox;
    }
}