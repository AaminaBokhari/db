using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;

namespace EventVerse
{
    public class ForgotPasswordForm : BaseForm
    {
        private TextBox emailTextBox;
        private Button submitButton;
        private Label instructionLabel;
        private Button backButton;

        public ForgotPasswordForm()
        {
            InitializeComponents();
            SetTitle("Forgot Password");
        }

        private void InitializeComponents()
        {
            // Set form size and position
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create a panel to hold and center the content
            Panel contentPanel = new Panel
            {
                Size = new Size(300, 200),
                Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 200) / 2)
            };
            this.Controls.Add(contentPanel);

            // Add title label
            Label titleLabel = new Label
            {
                Text = "Forgot Password",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top
            };
            contentPanel.Controls.Add(titleLabel);

            instructionLabel = new Label
            {
                Text = "Enter your email address to reset your password:",
                Size = new Size(300, 40),
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top
            };
            contentPanel.Controls.Add(instructionLabel);

            emailTextBox = new TextBox
            {
                Size = new Size(280, 25),
                Font = new Font("Arial", 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            contentPanel.Controls.Add(emailTextBox);
            emailTextBox.Location = new Point((contentPanel.Width - emailTextBox.Width) / 2, 80);

            submitButton = new Button
            {
                Text = "Reset Password",
                Size = new Size(150, 35),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            submitButton.Click += SubmitButton_Click;
            contentPanel.Controls.Add(submitButton);
            submitButton.Location = new Point((contentPanel.Width - submitButton.Width) / 2, 120);

            backButton = new Button
            {
                Text = "Back",
                Size = new Size(80, 30),
                Font = new Font("Arial", 10),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            backButton.Click += (s, e) => this.Close();
            contentPanel.Controls.Add(backButton);
            backButton.Location = new Point(0, contentPanel.Height - backButton.Height);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                MessageBox.Show("Please enter your email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(emailTextBox.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // In a real application, this is where you would send a password reset email
            MessageBox.Show($"A password reset link has been sent to {emailTextBox.Text}. Please check your email.",
                          "Password Reset",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);

            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

