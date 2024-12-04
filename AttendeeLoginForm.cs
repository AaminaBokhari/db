using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace EventVerse
{
    public class AttendeeLoginForm : BaseForm
    {
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button registerButton;
        private LinkLabel forgotPasswordLink;

        public AttendeeLoginForm()
        {
            InitializeComponents();
            SetTitle("Attendee Login");
        }

        private void InitializeComponents()
        {
            usernameTextBox = CreateTextBox("Username", 0);
            passwordTextBox = CreateTextBox("Password", 1);
            passwordTextBox.UseSystemPasswordChar = true;

            loginButton = new Button
            {
                Text = "Login",
                Font = new Font("Arial", 14),
                Size = new Size(200, 50),
                Location = new Point(this.ClientSize.Width / 2 - 100, 300),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            loginButton.Click += LoginButton_Click;

            registerButton = new Button
            {
                Text = "Register",
                Font = new Font("Arial", 14),
                Size = new Size(200, 50),
                Location = new Point(this.ClientSize.Width / 2 - 100, 360),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(102, 102, 102),
                ForeColor = Color.White
            };
            registerButton.Click += RegisterButton_Click;

            forgotPasswordLink = new LinkLabel
            {
                Text = "Forgot?",
                Font = new Font("Arial", 12),
                Location = new Point(this.ClientSize.Width / 2 - 60, 420),
                Size = new Size(120, 30)
            };
            forgotPasswordLink.Click += ForgotPasswordLink_Click;

            this.Controls.Add(usernameTextBox);
            this.Controls.Add(passwordTextBox);
            this.Controls.Add(loginButton);
            this.Controls.Add(registerButton);
            this.Controls.Add(forgotPasswordLink);
        }

        private TextBox CreateTextBox(string placeholder, int index)
        {
            TextBox textBox = new TextBox
            {
                Font = new Font("Arial", 14),
                Size = new Size(300, 30),
                Location = new Point(this.ClientSize.Width / 2 - 150, 200 + index * 60)
            };
            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == placeholder) textBox.Text = "";
            };
            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text)) textBox.Text = placeholder;
            };
            textBox.Text = placeholder;
            return textBox;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                AttendeeDashboardForm dashboard = new AttendeeDashboardForm();
                this.Hide();
                dashboard.FormClosed += (s, args) => this.Close();
                dashboard.Show();
            }
        }

        private bool ValidateLogin()
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || username == "Username")
            {
                MessageBox.Show("Please enter a valid username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(password) || password == "Password")
            {
                MessageBox.Show("Please enter a valid password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
            {
                MessageBox.Show("Password must contain at least one uppercase letter, one lowercase letter, and one digit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            AttendeeRegistrationForm registrationForm = new AttendeeRegistrationForm();
            registrationForm.ShowDialog();
        }

        private void ForgotPasswordLink_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotPasswordForm = new ForgotPasswordForm();
            forgotPasswordForm.ShowDialog();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoBox
            // 
            this.logoBox.Click += new System.EventHandler(this.logoBox_Click);
            // 
            // AttendeeLoginForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "AttendeeLoginForm";
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
        }

        private void logoBox_Click(object sender, EventArgs e)
        {
            // TODO: Implement logo click functionality if needed
        }
    }
}


