namespace Access_GeoGo.Forms
{
    partial class AuthenticatePage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthenticatePage));
            this.Username_Input = new System.Windows.Forms.TextBox();
            this.Username_Label = new System.Windows.Forms.Label();
            this.Password_Label = new System.Windows.Forms.Label();
            this.Password_Input = new System.Windows.Forms.TextBox();
            this.Database_Label = new System.Windows.Forms.Label();
            this.Database_Input = new System.Windows.Forms.TextBox();
            this.AuthenticateButton = new System.Windows.Forms.Button();
            this.AuthenticatePage_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Username_Input
            // 
            this.Username_Input.HideSelection = false;
            this.Username_Input.Location = new System.Drawing.Point(127, 55);
            this.Username_Input.Name = "Username_Input";
            this.Username_Input.Size = new System.Drawing.Size(212, 21);
            this.Username_Input.TabIndex = 6;
            this.Username_Input.TextChanged += new System.EventHandler(this.Input_TextChanged);
            // 
            // Username_Label
            // 
            this.Username_Label.Location = new System.Drawing.Point(10, 50);
            this.Username_Label.Name = "Username_Label";
            this.Username_Label.Size = new System.Drawing.Size(110, 32);
            this.Username_Label.TabIndex = 7;
            this.Username_Label.Text = "Username / Email";
            this.Username_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Password_Label
            // 
            this.Password_Label.Location = new System.Drawing.Point(10, 82);
            this.Password_Label.Name = "Password_Label";
            this.Password_Label.Size = new System.Drawing.Size(110, 32);
            this.Password_Label.TabIndex = 9;
            this.Password_Label.Text = "Password";
            this.Password_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Password_Input
            // 
            this.Password_Input.Location = new System.Drawing.Point(127, 87);
            this.Password_Input.Name = "Password_Input";
            this.Password_Input.PasswordChar = '*';
            this.Password_Input.ShortcutsEnabled = false;
            this.Password_Input.Size = new System.Drawing.Size(212, 21);
            this.Password_Input.TabIndex = 8;
            this.Password_Input.UseSystemPasswordChar = true;
            this.Password_Input.TextChanged += new System.EventHandler(this.Input_TextChanged);
            // 
            // Database_Label
            // 
            this.Database_Label.Location = new System.Drawing.Point(10, 114);
            this.Database_Label.Name = "Database_Label";
            this.Database_Label.Size = new System.Drawing.Size(110, 32);
            this.Database_Label.TabIndex = 11;
            this.Database_Label.Text = "Database";
            this.Database_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Database_Input
            // 
            this.Database_Input.Location = new System.Drawing.Point(127, 119);
            this.Database_Input.Name = "Database_Input";
            this.Database_Input.ShortcutsEnabled = false;
            this.Database_Input.Size = new System.Drawing.Size(212, 21);
            this.Database_Input.TabIndex = 10;
            // 
            // AuthenticateButton
            // 
            this.AuthenticateButton.Enabled = false;
            this.AuthenticateButton.Location = new System.Drawing.Point(14, 148);
            this.AuthenticateButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AuthenticateButton.Name = "AuthenticateButton";
            this.AuthenticateButton.Size = new System.Drawing.Size(325, 27);
            this.AuthenticateButton.TabIndex = 12;
            this.AuthenticateButton.Text = "Authenticate";
            this.AuthenticateButton.UseVisualStyleBackColor = true;
            this.AuthenticateButton.Click += new System.EventHandler(this.AuthenticateButton_Click);
            // 
            // AuthenticatePage_Label
            // 
            this.AuthenticatePage_Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AuthenticatePage_Label.Location = new System.Drawing.Point(14, 9);
            this.AuthenticatePage_Label.Name = "AuthenticatePage_Label";
            this.AuthenticatePage_Label.Size = new System.Drawing.Size(325, 32);
            this.AuthenticatePage_Label.TabIndex = 13;
            this.AuthenticatePage_Label.Text = "Enter your Geotab credentials and optional database\r\nname to authenticate this se" +
    "ssion.";
            this.AuthenticatePage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AuthenticatePage
            // 
            this.AcceptButton = this.AuthenticateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(353, 189);
            this.Controls.Add(this.AuthenticatePage_Label);
            this.Controls.Add(this.AuthenticateButton);
            this.Controls.Add(this.Database_Label);
            this.Controls.Add(this.Database_Input);
            this.Controls.Add(this.Password_Label);
            this.Controls.Add(this.Password_Input);
            this.Controls.Add(this.Username_Label);
            this.Controls.Add(this.Username_Input);
            this.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(369, 228);
            this.Name = "AuthenticatePage";
            this.Text = "Geotab Authentication";
            this.Load += new System.EventHandler(this.AuthenticatePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Username_Input;
        private System.Windows.Forms.Label Username_Label;
        private System.Windows.Forms.Label Password_Label;
        private System.Windows.Forms.TextBox Password_Input;
        private System.Windows.Forms.Label Database_Label;
        private System.Windows.Forms.TextBox Database_Input;
        private System.Windows.Forms.Button AuthenticateButton;
        private System.Windows.Forms.Label AuthenticatePage_Label;
    }
}

