namespace Access_GeoGo.Forms
{
    partial class MenuPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPage));
            this.FuelTransBtn = new System.Windows.Forms.Button();
            this.AuthenticatePageButton = new System.Windows.Forms.Button();
            this.MenuPage_Header = new System.Windows.Forms.Label();
            this.Autenticated_CheckBox = new System.Windows.Forms.CheckBox();
            this.Userame_Label = new System.Windows.Forms.Label();
            this.TestPageButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.FaultCodesBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FuelTransBtn
            // 
            this.FuelTransBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FuelTransBtn.Location = new System.Drawing.Point(11, 152);
            this.FuelTransBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.FuelTransBtn.Name = "FuelTransBtn";
            this.FuelTransBtn.Size = new System.Drawing.Size(163, 49);
            this.FuelTransBtn.TabIndex = 2;
            this.FuelTransBtn.Text = "Fuel Transaction Data";
            this.FuelTransBtn.UseVisualStyleBackColor = true;
            this.FuelTransBtn.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // AuthenticatePageButton
            // 
            this.AuthenticatePageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AuthenticatePageButton.Location = new System.Drawing.Point(11, 93);
            this.AuthenticatePageButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AuthenticatePageButton.Name = "AuthenticatePageButton";
            this.AuthenticatePageButton.Size = new System.Drawing.Size(163, 49);
            this.AuthenticatePageButton.TabIndex = 1;
            this.AuthenticatePageButton.Text = "Authenticate";
            this.AuthenticatePageButton.UseVisualStyleBackColor = true;
            this.AuthenticatePageButton.Click += new System.EventHandler(this.AuthenticatePageButton_Click);
            // 
            // MenuPage_Header
            // 
            this.MenuPage_Header.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this.MenuPage_Header.Location = new System.Drawing.Point(12, 9);
            this.MenuPage_Header.Name = "MenuPage_Header";
            this.MenuPage_Header.Size = new System.Drawing.Size(163, 21);
            this.MenuPage_Header.TabIndex = 9;
            this.MenuPage_Header.Text = "Geotab Data Api Import";
            this.MenuPage_Header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Autenticated_CheckBox
            // 
            this.Autenticated_CheckBox.AutoCheck = false;
            this.Autenticated_CheckBox.AutoSize = true;
            this.Autenticated_CheckBox.Location = new System.Drawing.Point(15, 33);
            this.Autenticated_CheckBox.Name = "Autenticated_CheckBox";
            this.Autenticated_CheckBox.Size = new System.Drawing.Size(106, 20);
            this.Autenticated_CheckBox.TabIndex = 11;
            this.Autenticated_CheckBox.Text = "Authenticated";
            this.Autenticated_CheckBox.UseVisualStyleBackColor = true;
            this.Autenticated_CheckBox.Click += new System.EventHandler(this.AuthenticatePageButton_Click);
            // 
            // Userame_Label
            // 
            this.Userame_Label.AutoSize = true;
            this.Userame_Label.Location = new System.Drawing.Point(12, 56);
            this.Userame_Label.Name = "Userame_Label";
            this.Userame_Label.Size = new System.Drawing.Size(65, 16);
            this.Userame_Label.TabIndex = 12;
            this.Userame_Label.Text = "Username:";
            // 
            // TestPageButton
            // 
            this.TestPageButton.Enabled = false;
            this.TestPageButton.Location = new System.Drawing.Point(95, 53);
            this.TestPageButton.Name = "TestPageButton";
            this.TestPageButton.Size = new System.Drawing.Size(80, 23);
            this.TestPageButton.TabIndex = 13;
            this.TestPageButton.Text = "Test Feature";
            this.TestPageButton.UseVisualStyleBackColor = true;
            this.TestPageButton.Visible = false;
            this.TestPageButton.Click += new System.EventHandler(this.TestPageButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Font = new System.Drawing.Font("Century Gothic", 7F);
            this.VersionLabel.Location = new System.Drawing.Point(11, 267);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(168, 15);
            this.VersionLabel.TabIndex = 14;
            this.VersionLabel.Text = "GeoGo Version: 4.0 | © 10.2021";
            // 
            // FaultCodesBtn
            // 
            this.FaultCodesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FaultCodesBtn.Location = new System.Drawing.Point(12, 211);
            this.FaultCodesBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.FaultCodesBtn.Name = "FaultCodesBtn";
            this.FaultCodesBtn.Size = new System.Drawing.Size(163, 49);
            this.FaultCodesBtn.TabIndex = 3;
            this.FaultCodesBtn.Text = "Fault Codes";
            this.FaultCodesBtn.UseVisualStyleBackColor = true;
            this.FaultCodesBtn.Click += new System.EventHandler(this.FaultCodesBtn_Click);
            // 
            // MenuPage
            // 
            this.AcceptButton = this.AuthenticatePageButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(187, 291);
            this.Controls.Add(this.FaultCodesBtn);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.TestPageButton);
            this.Controls.Add(this.Userame_Label);
            this.Controls.Add(this.Autenticated_CheckBox);
            this.Controls.Add(this.MenuPage_Header);
            this.Controls.Add(this.AuthenticatePageButton);
            this.Controls.Add(this.FuelTransBtn);
            this.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(203, 330);
            this.Name = "MenuPage";
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button FuelTransBtn;
        private System.Windows.Forms.Button AuthenticatePageButton;
        private System.Windows.Forms.Label MenuPage_Header;
        private System.Windows.Forms.CheckBox Autenticated_CheckBox;
        private System.Windows.Forms.Label Userame_Label;
        private System.Windows.Forms.Button TestPageButton;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Button FaultCodesBtn;
    }
}