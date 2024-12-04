using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class EmailVerificationForm : BaseForm
    {
        private Label messageLabel;
        private Button proceedButton;
        private Button resendButton;

        public EmailVerificationForm(bool isVerified, string email)
        {
            InitializeComponents(isVerified, email);
            SetTitle("Email Verification");
        }

        private void InitializeComponents(bool isVerified, string email)
        {
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            messageLabel = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(360, 100),
                Location = new Point(20, 50),
                Font = new Font("Arial", 12)
            };

            proceedButton = new Button
            {
                Text = "Proceed to Login",
                Size = new Size(200, 40),
                Location = new Point(100, 170),
                Font = new Font("Arial", 12),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            proceedButton.Click += ProceedButton_Click;

            resendButton = new Button
            {
                Text = "Resend Verification Email",
                Size = new Size(200, 40),
                Location = new Point(100, 220),
                Font = new Font("Arial", 12),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            resendButton.Click += ResendButton_Click;

            if (isVerified)
            {
                messageLabel.Text = $"Congratulations! Your email ({email}) has been successfully verified. You can now proceed to login.";
                proceedButton.Enabled = true;
            }
            else
            {
                messageLabel.Text = $"We're sorry, but we couldn't verify your email ({email}). The verification link may be invalid or expired.";
                proceedButton.Enabled = false;
                resendButton.Visible = true;
            }

            this.Controls.Add(messageLabel);
            this.Controls.Add(proceedButton);
            this.Controls.Add(resendButton);
        }

        private void ProceedButton_Click(object sender, EventArgs e)
        {
            this.Close();
            AttendeeLoginForm loginForm = new AttendeeLoginForm();
            loginForm.Show();
        }

        private void ResendButton_Click(object sender, EventArgs e)
        {
            // In a real application, this would trigger the email resend process
            MessageBox.Show("A new verification email has been sent. Please check your inbox.", 
                            "Email Sent", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
        }
    }
}

