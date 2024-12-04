using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class AdminDashboardForm : BaseForm
    {
        private Label welcomeLabel;
        private TableLayoutPanel buttonPanel;
        private Button logoutButton;

        public AdminDashboardForm()
        {
            InitializeComponents();
            SetTitle("Admin Dashboard");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Welcome Label with title styling
            welcomeLabel = new Label
            {
                Text = "Admin Dashboard",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };

            // Create TableLayoutPanel for centered, organized buttons
            buttonPanel = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 8,
                Dock = DockStyle.None,
                AutoSize = true,
                Location = new Point(250, 100),
                Width = 300
            };

            // Add buttons to panel
            AddDashboardButton("User & Organizer Management", 0);
            AddDashboardButton("Event Approval", 1);
            AddDashboardButton("Reports Dashboard", 2);
            AddDashboardButton("Feedback Moderation", 3);
            AddDashboardButton("Complaint Resolution", 4);
            AddDashboardButton("Payment Management", 5);
            AddDashboardButton("Search Events", 6);

            // Add logout button
            logoutButton = new Button
            {
                Text = "Logout",
                Font = new Font("Segoe UI", 12),
                Size = new Size(120, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Location = new Point(this.ClientSize.Width - 140, 20)
            };
            logoutButton.Click += LogoutButton_Click;

            this.Controls.Add(welcomeLabel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(logoutButton);
        }

        private void AddDashboardButton(string text, int index)
        {
            Button button = new Button
            {
                Text = text,
                Font = new Font("Segoe UI", 12),
                Size = new Size(300, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                Margin = new Padding(0, 10, 0, 0)
            };
            button.Click += DashboardButton_Click;
            buttonPanel.Controls.Add(button);
        }

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Form formToShow = null;

                switch (clickedButton.Text)
                {
                    case "User & Organizer Management":
                        formToShow = new UserAndOrganizerManagementForm();
                        break;
                    case "Event Approval":
                        formToShow = new EventApprovalForm();
                        break;
                    case "Reports Dashboard":
                        formToShow = new ReportsDashboardForm();
                        break;
                    case "Feedback Moderation":
                        formToShow = new FeedbackModerationForm();
                        break;
                    case "Complaint Resolution":
                        formToShow = new ComplaintResolutionForm();
                        break;
                    case "Payment Management":
                        formToShow = new PaymentManagementForm();
                        break;
                    case "Search Events":
                        formToShow = new EventSearchForm();
                        break;
                }

                if (formToShow != null)
                {
                    formToShow.ShowDialog();
                }
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                // You might want to show the login form or return to the main form here
                // For example: new LoginForm().Show();
            }
        }
    }
}

