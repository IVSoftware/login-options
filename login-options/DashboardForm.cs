namespace login_options
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            // Ordinarily we don't get the handle until
            // window is shown. But we want it now.
            _ = Handle;
            // Call BeginInvoke on the new handle so as not to block the CTor.
            BeginInvoke(new Action(() => execLoginFlow()));
            // Ensure final disposal of login form. Failure to properly dispose of window 
            // handles is the leading cause of the kind of exit hang you describe.
            Disposed += (sender, e) =>
            {
                _loginForm.Dispose();
                _changePasswordForm.Dispose();
            };
            buttonSignOut.Click += (sender, e) => IsLoggedIn = false;
            buttonChangePassword.Click += (sender, e) =>
            {
                switch (_changePasswordForm.ShowDialog(this))
                {
                    case DialogResult.Yes: IsLoggedIn = false; break;
                    case DialogResult.No: /* N O O P */ break;
                }
            };
        }
        private LoginForm _loginForm = new LoginForm();
        private ChangePasswordForm _changePasswordForm = new ChangePasswordForm();
        protected override void SetVisibleCore(bool value) =>
            base.SetVisibleCore(value && IsLoggedIn);

        bool _isLoggedIn = false;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                if (!Equals(_isLoggedIn, value))
                {
                    _isLoggedIn = value;
                    OnIsLoggedInChanged();
                }
            }
        }

        protected virtual void OnIsLoggedInChanged()
        {
            if (IsLoggedIn)
            {
                WindowState = FormWindowState.Maximized;
                Text = $"DASHBOARD - Welcome {_loginForm.UserName}";
                Visible = true;
            }
            else execLoginFlow();
        }

        private void execLoginFlow()
        {
            Visible = false;
            while (!IsLoggedIn)
            {
                _loginForm.StartPosition = FormStartPosition.CenterScreen;
                if (DialogResult.Cancel == _loginForm.ShowDialog(this))
                {
                    switch (MessageBox.Show(
                        this,
                        "Invalid Credentials",
                        "Error",
                        buttons: MessageBoxButtons.RetryCancel))
                    {
                        case DialogResult.Cancel: Application.Exit(); return;
                        case DialogResult.Retry: break;
                    }
                }
                else
                {
                    IsLoggedIn = true;
                }
            }
        }
    }
}
