namespace login_options
{
    partial class DashboardForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonSignOut = new Button();
            buttonChangePassword = new Button();
            SuspendLayout();
            // 
            // buttonSignOut
            // 
            buttonSignOut.Location = new Point(22, 33);
            buttonSignOut.Name = "buttonSignOut";
            buttonSignOut.Size = new Size(161, 34);
            buttonSignOut.TabIndex = 0;
            buttonSignOut.Text = "Sign Out";
            buttonSignOut.UseVisualStyleBackColor = true;
            // 
            // buttonChangePassword
            // 
            buttonChangePassword.Location = new Point(22, 73);
            buttonChangePassword.Name = "buttonChangePassword";
            buttonChangePassword.Size = new Size(161, 34);
            buttonChangePassword.TabIndex = 0;
            buttonChangePassword.Text = "Change Password";
            buttonChangePassword.UseVisualStyleBackColor = true;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 244);
            Controls.Add(buttonChangePassword);
            Controls.Add(buttonSignOut);
            Name = "DashboardForm";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button buttonSignOut;
        private Button buttonChangePassword;
    }
}
