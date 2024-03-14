## Extended Login Options

Showing any form prior to making the main form visible can be tricky because that first form is going effectively become the main application window which is pathological. I have answers explaining this for a [Login Form](https://stackoverflow.com/a/74736871/5438626) and a [Splash Screen](https://stackoverflow.com/a/75534137/5438626). Let's adapt the first answer to your situation where a mock flow would be:

**Login**

[![login][1]][1]

___

**Dashboard (main application window)**

[![dashboard][2]][2]

___

**Simulated change password (new)**

[![change password][3]][3]

___

To answer your question "hide two forms at once" first look at the response to `MessageBox.Yes`. It's going to take that response and make it the `DialogResult` of the password change form which closes it.

```
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
``` 

___

Meanwhile, the `DashboardForm` has been waiting the dialog result of the `ChangePasswordForm` (in the response to clicking the [Change Password] button.

```
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
```
This changes the `IsLoggedIn` bool wich controls the visibility of the `Dashboard`, giving you the behavior you describe.
```

    bool _isLoggedIn = false;
    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set
        {
            if (!Equals(_isLoggedIn, value))
            {
                _isLoggedIn = value;
                onIsLoggedInChanged();
            }
        }
    }

    private void onIsLoggedInChanged()
    {
        if (IsLoggedIn)
        {
            WindowState = FormWindowState.Maximized;
            Text = $"DASHBOARD - Welcome {_loginForm.UserName}";
            Visible = true;
        }
        else execLoginFlow();
    }
    .
    .
    .
}
```

  [1]: https://i.stack.imgur.com/k0j4v.png
  [2]: https://i.stack.imgur.com/dcrCD.png
  [3]: https://i.stack.imgur.com/0uNIS.png