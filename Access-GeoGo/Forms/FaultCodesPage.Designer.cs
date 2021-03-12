namespace Access_GeoGo.Forms
{
    partial class FaultCodesPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaultCodesPage));
            this.FaultCodeView = new System.Windows.Forms.DataGridView();
            this.FaultCodePanel = new System.Windows.Forms.Panel();
            this.LimitLabel = new System.Windows.Forms.Label();
            this.LimitSelection = new System.Windows.Forms.NumericUpDown();
            this.GetFeedBtn = new System.Windows.Forms.Button();
            this.DeviceComboBox = new System.Windows.Forms.ComboBox();
            this.DeviceLabel = new System.Windows.Forms.Label();
            this.ToDatePicker = new System.Windows.Forms.DateTimePicker();
            this.ToDateLabel = new System.Windows.Forms.Label();
            this.FromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.FromDateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FaultCodeView)).BeginInit();
            this.FaultCodePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimitSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // FaultCodeView
            // 
            this.FaultCodeView.AllowUserToAddRows = false;
            this.FaultCodeView.AllowUserToDeleteRows = false;
            this.FaultCodeView.AllowUserToResizeColumns = false;
            this.FaultCodeView.AllowUserToResizeRows = false;
            this.FaultCodeView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.FaultCodeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FaultCodeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FaultCodeView.Location = new System.Drawing.Point(0, 40);
            this.FaultCodeView.Name = "FaultCodeView";
            this.FaultCodeView.ReadOnly = true;
            this.FaultCodeView.RowHeadersVisible = false;
            this.FaultCodeView.RowHeadersWidth = 4;
            this.FaultCodeView.Size = new System.Drawing.Size(884, 421);
            this.FaultCodeView.TabIndex = 1;
            this.FaultCodeView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.FaultCodeView_DataBindingComplete);
            // 
            // FaultCodePanel
            // 
            this.FaultCodePanel.Controls.Add(this.LimitLabel);
            this.FaultCodePanel.Controls.Add(this.LimitSelection);
            this.FaultCodePanel.Controls.Add(this.GetFeedBtn);
            this.FaultCodePanel.Controls.Add(this.DeviceComboBox);
            this.FaultCodePanel.Controls.Add(this.DeviceLabel);
            this.FaultCodePanel.Controls.Add(this.ToDatePicker);
            this.FaultCodePanel.Controls.Add(this.ToDateLabel);
            this.FaultCodePanel.Controls.Add(this.FromDatePicker);
            this.FaultCodePanel.Controls.Add(this.SearchBtn);
            this.FaultCodePanel.Controls.Add(this.FromDateLabel);
            this.FaultCodePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FaultCodePanel.Location = new System.Drawing.Point(0, 0);
            this.FaultCodePanel.Name = "FaultCodePanel";
            this.FaultCodePanel.Size = new System.Drawing.Size(884, 40);
            this.FaultCodePanel.TabIndex = 2;
            // 
            // LimitLabel
            // 
            this.LimitLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LimitLabel.AutoSize = true;
            this.LimitLabel.Location = new System.Drawing.Point(165, 14);
            this.LimitLabel.Name = "LimitLabel";
            this.LimitLabel.Size = new System.Drawing.Size(31, 13);
            this.LimitLabel.TabIndex = 13;
            this.LimitLabel.Text = "Limit:";
            // 
            // LimitSelection
            // 
            this.LimitSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LimitSelection.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.LimitSelection.Location = new System.Drawing.Point(202, 10);
            this.LimitSelection.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.LimitSelection.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.LimitSelection.Name = "LimitSelection";
            this.LimitSelection.Size = new System.Drawing.Size(52, 20);
            this.LimitSelection.TabIndex = 3;
            this.LimitSelection.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // GetFeedBtn
            // 
            this.GetFeedBtn.Enabled = false;
            this.GetFeedBtn.Location = new System.Drawing.Point(5, 5);
            this.GetFeedBtn.Name = "GetFeedBtn";
            this.GetFeedBtn.Size = new System.Drawing.Size(75, 30);
            this.GetFeedBtn.TabIndex = 1;
            this.GetFeedBtn.Text = "Get Feed";
            this.GetFeedBtn.UseVisualStyleBackColor = true;
            this.GetFeedBtn.Click += new System.EventHandler(this.GetFeedBtn_Click);
            // 
            // DeviceComboBox
            // 
            this.DeviceComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceComboBox.FormattingEnabled = true;
            this.DeviceComboBox.Location = new System.Drawing.Point(310, 10);
            this.DeviceComboBox.Name = "DeviceComboBox";
            this.DeviceComboBox.Size = new System.Drawing.Size(100, 21);
            this.DeviceComboBox.TabIndex = 4;
            // 
            // DeviceLabel
            // 
            this.DeviceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceLabel.AutoSize = true;
            this.DeviceLabel.Location = new System.Drawing.Point(260, 14);
            this.DeviceLabel.Name = "DeviceLabel";
            this.DeviceLabel.Size = new System.Drawing.Size(44, 13);
            this.DeviceLabel.TabIndex = 9;
            this.DeviceLabel.Text = "Device:";
            // 
            // ToDatePicker
            // 
            this.ToDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ToDatePicker.Checked = false;
            this.ToDatePicker.CustomFormat = "M/dd/yyyy hh:mm tt  ";
            this.ToDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDatePicker.Location = new System.Drawing.Point(707, 10);
            this.ToDatePicker.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.ToDatePicker.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.ToDatePicker.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.ToDatePicker.Name = "ToDatePicker";
            this.ToDatePicker.ShowCheckBox = true;
            this.ToDatePicker.Size = new System.Drawing.Size(165, 20);
            this.ToDatePicker.TabIndex = 6;
            this.ToDatePicker.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // ToDateLabel
            // 
            this.ToDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ToDateLabel.AutoSize = true;
            this.ToDateLabel.Location = new System.Drawing.Point(652, 14);
            this.ToDateLabel.Name = "ToDateLabel";
            this.ToDateLabel.Size = new System.Drawing.Size(49, 13);
            this.ToDateLabel.TabIndex = 7;
            this.ToDateLabel.Text = "To Date:";
            // 
            // FromDatePicker
            // 
            this.FromDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FromDatePicker.CustomFormat = "M/dd/yyyy hh:mm tt  ";
            this.FromDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDatePicker.Location = new System.Drawing.Point(481, 10);
            this.FromDatePicker.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.FromDatePicker.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.FromDatePicker.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.FromDatePicker.Name = "FromDatePicker";
            this.FromDatePicker.ShowCheckBox = true;
            this.FromDatePicker.Size = new System.Drawing.Size(165, 20);
            this.FromDatePicker.TabIndex = 5;
            this.FromDatePicker.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Enabled = false;
            this.SearchBtn.Location = new System.Drawing.Point(85, 5);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(75, 30);
            this.SearchBtn.TabIndex = 2;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.ResultsBtn_Click);
            // 
            // FromDateLabel
            // 
            this.FromDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FromDateLabel.AutoSize = true;
            this.FromDateLabel.Location = new System.Drawing.Point(416, 14);
            this.FromDateLabel.Name = "FromDateLabel";
            this.FromDateLabel.Size = new System.Drawing.Size(59, 13);
            this.FromDateLabel.TabIndex = 5;
            this.FromDateLabel.Text = "From Date:";
            // 
            // FaultCodesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.FaultCodeView);
            this.Controls.Add(this.FaultCodePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FaultCodesPage";
            this.Text = "Fault Codes Import";
            this.Load += new System.EventHandler(this.FaultCodesPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FaultCodeView)).EndInit();
            this.FaultCodePanel.ResumeLayout(false);
            this.FaultCodePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimitSelection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView FaultCodeView;
        private System.Windows.Forms.Panel FaultCodePanel;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Label FromDateLabel;
        private System.Windows.Forms.DateTimePicker FromDatePicker;
        private System.Windows.Forms.DateTimePicker ToDatePicker;
        private System.Windows.Forms.Label ToDateLabel;
        private System.Windows.Forms.Label DeviceLabel;
        private System.Windows.Forms.ComboBox DeviceComboBox;
        private System.Windows.Forms.Button GetFeedBtn;
        private System.Windows.Forms.NumericUpDown LimitSelection;
        private System.Windows.Forms.Label LimitLabel;
    }
}