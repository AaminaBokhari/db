using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EventVerse
{
    public class EventApprovalForm : BaseForm
    {
        private DataGridView eventsGridView;
        private Button approveButton;
        private Button rejectButton;
        private Button refreshButton;
        private ComboBox filterComboBox;
        private ComboBox categoryComboBox;
        private Button assignCategoryButton;

        public EventApprovalForm()
        {
            InitializeComponents();
            SetTitle("Event Approval");
        }

        private void InitializeComponents()
        {
            const int verticalOffset = 50; // Offset to move everything down
            this.Size = new Size(1000, 600);

            eventsGridView = new DataGridView
            {
                Location = new Point(50, 100 + verticalOffset),
                Size = new Size(this.ClientSize.Width - 100, this.ClientSize.Height - 250),
                Font = new Font("Arial", 12),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            eventsGridView.Columns.Add("EventName", "Event Name");
            eventsGridView.Columns.Add("Organizer", "Organizer");
            eventsGridView.Columns.Add("Date", "Date");
            eventsGridView.Columns.Add("Location", "Location");
            eventsGridView.Columns.Add("Category", "Category");
            eventsGridView.Columns.Add("Status", "Status");

            LoadSampleData();

            filterComboBox = new ComboBox
            {
                Location = new Point(50, 60 + verticalOffset),
                Size = new Size(150, 30),
                Font = new Font("Arial", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            filterComboBox.Items.AddRange(new object[] { "All", "Pending", "Approved", "Rejected" });
            filterComboBox.SelectedIndex = 0;
            filterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;

            categoryComboBox = new ComboBox
            {
                Location = new Point(220, 60 + verticalOffset),
                Size = new Size(150, 30),
                Font = new Font("Arial", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            LoadCategories();

            assignCategoryButton = new Button
            {
                Text = "Assign Category",
                Font = new Font("Arial", 12),
                Size = new Size(150, 30),
                Location = new Point(390, 60 + verticalOffset),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            assignCategoryButton.Click += AssignCategoryButton_Click;

            approveButton = new Button
            {
                Text = "Approve",
                Font = new Font("Arial", 12),
                Size = new Size(120, 40),
                Location = new Point(this.ClientSize.Width / 2 - 190, this.ClientSize.Height - 80 + verticalOffset),
                BackColor = Color.Green,
                ForeColor = Color.White
            };
            approveButton.Click += ApproveButton_Click;

            rejectButton = new Button
            {
                Text = "Reject",
                Font = new Font("Arial", 12),
                Size = new Size(120, 40),
                Location = new Point(this.ClientSize.Width / 2 - 60, this.ClientSize.Height - 80 + verticalOffset),
                BackColor = Color.Red,
                ForeColor = Color.White
            };
            rejectButton.Click += RejectButton_Click;

            refreshButton = new Button
            {
                Text = "Refresh",
                Font = new Font("Arial", 12),
                Size = new Size(120, 40),
                Location = new Point(this.ClientSize.Width / 2 + 70, this.ClientSize.Height - 80 + verticalOffset),
                BackColor = Color.Blue,
                ForeColor = Color.White
            };
            refreshButton.Click += RefreshButton_Click;

            this.Controls.Add(eventsGridView);
            this.Controls.Add(filterComboBox);
            this.Controls.Add(categoryComboBox);
            this.Controls.Add(assignCategoryButton);
            this.Controls.Add(approveButton);
            this.Controls.Add(rejectButton);
            this.Controls.Add(refreshButton);
        }

        private void LoadSampleData()
        {
            eventsGridView.Rows.Clear();
            eventsGridView.Rows.Add("Tech Conference 2024", "TechOrg", "2024-06-15", "New York", "Technology", "Pending");
            eventsGridView.Rows.Add("Music Festival", "MusicCo", "2024-07-20", "Los Angeles", "Music", "Pending");
            eventsGridView.Rows.Add("Business Workshop", "BizGroup", "2024-08-05", "Chicago", "Business", "Pending");
            eventsGridView.Rows.Add("Art Exhibition", "ArtistsCo", "2024-09-10", "San Francisco", "Art", "Approved");
            eventsGridView.Rows.Add("Sports Tournament", "SportsOrg", "2024-10-01", "Miami", "Sports", "Rejected");
        }

        private void LoadCategories()
        {
            // In a real application, this would load categories from a database
            List<string> categories = new List<string> { "Music", "Technology", "Business", "Sports", "Art" };
            categoryComboBox.Items.AddRange(categories.ToArray());
        }

        private void ApproveButton_Click(object sender, EventArgs e)
        {
            if (eventsGridView.SelectedRows.Count > 0)
            {
                eventsGridView.SelectedRows[0].Cells["Status"].Value = "Approved";
                MessageBox.Show("Event approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an event to approve.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            if (eventsGridView.SelectedRows.Count > 0)
            {
                eventsGridView.SelectedRows[0].Cells["Status"].Value = "Rejected";
                MessageBox.Show("Event rejected.", "Action Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an event to reject.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadSampleData();
            MessageBox.Show("Event list refreshed.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterStatus = filterComboBox.SelectedItem.ToString();
            foreach (DataGridViewRow row in eventsGridView.Rows)
            {
                row.Visible = filterStatus == "All" || row.Cells["Status"].Value.ToString() == filterStatus;
            }
        }

        private void AssignCategoryButton_Click(object sender, EventArgs e)
        {
            if (eventsGridView.SelectedRows.Count > 0 && categoryComboBox.SelectedIndex != -1)
            {
                eventsGridView.SelectedRows[0].Cells["Category"].Value = categoryComboBox.SelectedItem.ToString();
                MessageBox.Show("Category assigned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an event and a category to assign.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
