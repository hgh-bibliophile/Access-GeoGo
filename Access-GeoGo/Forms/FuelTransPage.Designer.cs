namespace Access_GeoGo.Forms
{
    partial class FuelTransPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FuelTransPage));
            this.GeoGoDataView = new System.Windows.Forms.DataGridView();
            this.GeoGoDataPanel = new System.Windows.Forms.Panel();
            this.NextBtn = new System.Windows.Forms.Button();
            this.Preview = new System.Windows.Forms.Button();
            this.InsertBtn = new System.Windows.Forms.Button();
            this.QueryStatusBar = new System.Windows.Forms.StatusStrip();
            this.ResultsFoundLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.QueryLoadingBar = new System.Windows.Forms.ToolStripProgressBar();
            this.QueryLoadingLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.GeoGoDataView)).BeginInit();
            this.GeoGoDataPanel.SuspendLayout();
            this.QueryStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // GeoGoDataView
            // 
            this.GeoGoDataView.AllowUserToAddRows = false;
            this.GeoGoDataView.AllowUserToDeleteRows = false;
            this.GeoGoDataView.AllowUserToResizeColumns = false;
            this.GeoGoDataView.AllowUserToResizeRows = false;
            this.GeoGoDataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GeoGoDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GeoGoDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeoGoDataView.Location = new System.Drawing.Point(0, 40);
            this.GeoGoDataView.Name = "GeoGoDataView";
            this.GeoGoDataView.ReadOnly = true;
            this.GeoGoDataView.RowHeadersVisible = false;
            this.GeoGoDataView.RowHeadersWidth = 4;
            this.GeoGoDataView.Size = new System.Drawing.Size(434, 399);
            this.GeoGoDataView.TabIndex = 0;
            this.GeoGoDataView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GeoGoDataView_DataBindingComplete);
            // 
            // GeoGoDataPanel
            // 
            this.GeoGoDataPanel.Controls.Add(this.NextBtn);
            this.GeoGoDataPanel.Controls.Add(this.Preview);
            this.GeoGoDataPanel.Controls.Add(this.InsertBtn);
            this.GeoGoDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.GeoGoDataPanel.Location = new System.Drawing.Point(0, 0);
            this.GeoGoDataPanel.Name = "GeoGoDataPanel";
            this.GeoGoDataPanel.Size = new System.Drawing.Size(434, 40);
            this.GeoGoDataPanel.TabIndex = 1;
            // 
            // NextBtn
            // 
            this.NextBtn.Location = new System.Drawing.Point(86, 5);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new System.Drawing.Size(75, 30);
            this.NextBtn.TabIndex = 1;
            this.NextBtn.Text = "Query Next";
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // Preview
            // 
            this.Preview.Location = new System.Drawing.Point(167, 5);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(75, 30);
            this.Preview.TabIndex = 2;
            this.Preview.Text = "Open DB";
            this.Preview.UseVisualStyleBackColor = true;
            this.Preview.Click += new System.EventHandler(this.Preview_Click);
            // 
            // InsertBtn
            // 
            this.InsertBtn.Enabled = false;
            this.InsertBtn.Location = new System.Drawing.Point(5, 5);
            this.InsertBtn.Name = "InsertBtn";
            this.InsertBtn.Size = new System.Drawing.Size(75, 30);
            this.InsertBtn.TabIndex = 0;
            this.InsertBtn.Text = "Update DB";
            this.InsertBtn.UseVisualStyleBackColor = true;
            this.InsertBtn.Click += new System.EventHandler(this.InsertBtn_Click);
            // 
            // QueryStatusBar
            // 
            this.QueryStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResultsFoundLabel,
            this.QueryLoadingBar,
            this.QueryLoadingLabel});
            this.QueryStatusBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.QueryStatusBar.Location = new System.Drawing.Point(0, 439);
            this.QueryStatusBar.Name = "QueryStatusBar";
            this.QueryStatusBar.Size = new System.Drawing.Size(434, 22);
            this.QueryStatusBar.SizingGrip = false;
            this.QueryStatusBar.TabIndex = 2;
            this.QueryStatusBar.Text = "Query Results Status";
            // 
            // ResultsFoundLabel
            // 
            this.ResultsFoundLabel.Name = "ResultsFoundLabel";
            this.ResultsFoundLabel.Size = new System.Drawing.Size(87, 17);
            this.ResultsFoundLabel.Text = "Results Found: ";
            // 
            // QueryLoadingBar
            // 
            this.QueryLoadingBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.QueryLoadingBar.Name = "QueryLoadingBar";
            this.QueryLoadingBar.Size = new System.Drawing.Size(100, 16);
            this.QueryLoadingBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // QueryLoadingLabel
            // 
            this.QueryLoadingLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.QueryLoadingLabel.Name = "QueryLoadingLabel";
            this.QueryLoadingLabel.Size = new System.Drawing.Size(55, 17);
            this.QueryLoadingLabel.Text = "Progress:";
            // 
            // GeoGoPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.ClientSize = new System.Drawing.Size(434, 461);
            this.Controls.Add(this.GeoGoDataView);
            this.Controls.Add(this.GeoGoDataPanel);
            this.Controls.Add(this.QueryStatusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(450, 500);
            this.Name = "GeoGoPage";
            this.Text = "Results";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeoGoPage_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GeoGoPage_FormClosed);
            this.Load += new System.EventHandler(this.FuelTransPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GeoGoDataView)).EndInit();
            this.GeoGoDataPanel.ResumeLayout(false);
            this.QueryStatusBar.ResumeLayout(false);
            this.QueryStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView GeoGoDataView;
        public System.Windows.Forms.Panel GeoGoDataPanel;
        public System.Windows.Forms.Button NextBtn;
        public System.Windows.Forms.Button Preview;
        public System.Windows.Forms.Button InsertBtn;
        public System.Windows.Forms.StatusStrip QueryStatusBar;
        public System.Windows.Forms.ToolStripStatusLabel ResultsFoundLabel;
        public System.Windows.Forms.ToolStripProgressBar QueryLoadingBar;
        public System.Windows.Forms.ToolStripStatusLabel QueryLoadingLabel;
    }
}