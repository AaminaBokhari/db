using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class NotificationCenterForm : BaseForm
    {
        private ListView notificationListView;
        private Button markAsReadButton;
        private Button deleteButton;
        private Button refreshButton;

        public NotificationCenterForm()
        {
            InitializeComponents();
            SetTitle("Notification Center");
            LoadNotifications();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            notificationListView = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new Point(20, 120), // Changed Y coordinate from 80 to 120
                Size = new Size(540, 250), // Adjusted height from 290 to 250
                Font = new Font("Arial", 10)
            };
            notificationListView.Columns.Add("Type", 100);
            notificationListView.Columns.Add("Message", 340);
            notificationListView.Columns.Add("Date", 100);

            markAsReadButton = new Button
            {
                Text = "Mark as Read",
                Location = new Point(20, 380),
                Size = new Size(120, 30),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            markAsReadButton.Click += MarkAsReadButton_Click;

            deleteButton = new Button
            {
                Text = "Delete",
                Location = new Point(150, 380),
                Size = new Size(120, 30),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            deleteButton.Click += DeleteButton_Click;

            refreshButton = new Button
            {
                Text = "Refresh",
                Location = new Point(280, 380),
                Size = new Size(120, 30),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            refreshButton.Click += RefreshButton_Click;

            this.Controls.Add(notificationListView);
            this.Controls.Add(markAsReadButton);
            this.Controls.Add(deleteButton);
            this.Controls.Add(refreshButton);
        }

        private void LoadNotifications()
        {
            List<Notification> notifications = GetNotificationsFromDatabase();
            notificationListView.Items.Clear();

            foreach (var notification in notifications)
            {
                ListViewItem item = new ListViewItem(notification.Type);
                item.SubItems.Add(notification.Message);
                item.SubItems.Add(notification.Date.ToString("MM/dd/yyyy HH:mm"));
                item.Tag = notification;

                if (!notification.IsRead)
                {
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }

                notificationListView.Items.Add(item);
            }
        }

        private List<Notification> GetNotificationsFromDatabase()
        {
            // Simulating database fetch
            return new List<Notification>
            {
                new Notification { Type = "Update", Message = "The venue for 'Tech Conference 2024' has changed.", Date = DateTime.Now.AddDays(-1), IsRead = false },
                new Notification { Type = "Reminder", Message = "Your event 'Music Festival' starts in 2 days.", Date = DateTime.Now.AddHours(-5), IsRead = false },
                new Notification { Type = "Update", Message = "New speaker added to 'Business Workshop'.", Date = DateTime.Now.AddDays(-3), IsRead = true },
                new Notification { Type = "Reminder", Message = "Don't forget to complete your profile for better event recommendations.", Date = DateTime.Now.AddDays(-7), IsRead = true }
            };
        }

        private void MarkAsReadButton_Click(object sender, EventArgs e)
        {
            if (notificationListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in notificationListView.SelectedItems)
                {
                    Notification notification = (Notification)item.Tag;
                    notification.IsRead = true;
                    item.Font = new Font(item.Font, FontStyle.Regular);
                }
                MessageBox.Show("Selected notifications marked as read.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select at least one notification to mark as read.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (notificationListView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete the selected notification(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in notificationListView.SelectedItems)
                    {
                        notificationListView.Items.Remove(item);
                        // In a real application, you would also delete from the database here
                    }
                    MessageBox.Show("Selected notifications deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select at least one notification to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadNotifications();
            MessageBox.Show("Notifications refreshed.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class Notification
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}