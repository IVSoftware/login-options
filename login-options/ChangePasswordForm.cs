using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login_options
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
            buttonOK.Click += (sender, e) =>
            {
                switch (MessageBox.Show(
                    this,
                    "Return to Login Screen?",
                    "Login Successful!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question))
                {
                    // Close THIS form by copying the MessageBox result
                    case DialogResult.Yes:  DialogResult = DialogResult.Yes;  break;
                    case DialogResult.No:  DialogResult = DialogResult.No;  break;
                }
            };
            buttonCancel.Click += (sender, e) => DialogResult = DialogResult.No;
        }
    }
}
