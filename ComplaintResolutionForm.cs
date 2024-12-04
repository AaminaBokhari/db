using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class ComplaintResolutionForm : BaseForm
    {
        private DataGridView complaintsGridView;
        private TextBox responseTextBox;
        private Button resolveButton;
        private Button refreshButton;
        private ComboBox categoryComboBox;
        private ComboBox statusFilterComboBox;

        public ComplaintResolutionForm()
        {
            InitializeComponents();
            SetTitle("Complaint Resolution");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(1000, 700);

            complaintsGridView = new DataGridView
            {
                Location = new Point(50, 100),
                Size = new Size(this.ClientSize.Width - 100, this.ClientSize.Height - 350),
                Font = new Font("Arial", 12),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            complaintsGridView.Columns.Add("ID", "Complaint ID");
            complaintsGridView.Columns.Add("User", "User");
            complaintsGridView.Columns.Add("Event", "Event");
            complaintsGridView.Columns.Add("Category", "Category");
            complaintsGridView.Columns.Add("Description", "Description");
            complaintsGridView.Columns.Add("Status", "Status");

            LoadSampleData();

            categoryComboBox = new ComboBox
            {
                Location = new Point(50, 60),
                Size = new Size(150, 30),
                Font = new Font("Arial", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            categoryComboBox.Items.AddRange(new object[] { "All Categories", "Technical", "Customer Service", "Venue", "Other" });
            categoryComboBox.SelectedIndex = 0;
            categoryComboBox.SelectedIndexChanged += CategoryComboBox_SelectedIndexChanged;

            statusFilterComboBox = new ComboBox
            {
                Location = new Point(220, 60),
                Size = new Size(150, 30),
                Font = new Font("Arial", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            statusFilterComboBox.Items.AddRange(new object[] { "All Statuses", "Open", "Resolved" });
            statusFilterComboBox.SelectedIndex = 0;
            statusFilterComboBox.SelectedIndexChanged += StatusFilterComboBox_SelectedIndexChanged;

            Label responseLabel = new Label
            {
                Text = "Response:",
                Font = new Font("Arial", 12),
                Location = new Point(50, this.ClientSize.Height - 230),
                Size = new Size(100, 30)
            };

            responseTextBox = new TextBox
            {
                Font = new Font("Arial", 12),
                Location = new Point(50, this.ClientSize.Height - 200),
                Size = new Size(this.ClientSize.Width - 100, 80),
                Multiline = true
            };

            resolveButton = new Button
            {
                Text = "Resolve Complaint",
                Font = new Font("Arial", 12),
                Size = new Size(150, 40),
                Location = new Point(this.ClientSize.Width / 2 - 160, this.ClientSize.Height - 100),
                BackColor = Color.Green,
                ForeColor = Color.White
            };
            resolveButton.Click += ResolveButton_Click;

            refreshButton = new Button
            {
                Text = "Refresh",
                Font = new Font("Arial", 12),
                Size = new Size(150, 40),
                Location = new Point(this.ClientSize.Width / 2 + 10, this.ClientSize.Height - 100),
                BackColor = Color.Blue,
                ForeColor = Color.White
            };
            refreshButton.Click += RefreshButton_Click;

            this.Controls.Add(complaintsGridView);
            this.Controls.Add(categoryComboBox);
            this.Controls.Add(statusFilterComboBox);
            this.Controls.Add(responseLabel);
            this.Controls.Add(responseTextBox);
            this.Controls.Add(resolveButton);
            this.Controls.Add(refreshButton);
        }

        private void LoadSampleData()
        {
            complaintsGridView.Rows.Clear();
            complaintsGridView.Rows.Add("1", "John Doe", "Tech Conference 2024", "Technical", "Wi-Fi issues during the event", "Open");
            complaintsGridView.Rows.Add("2", "Jane Smith", "Music Festival", "Venue", "Sound quality was poor", "Open");
            complaintsGridView.Rows.Add("3", "Bob Johnson", "Business Workshop", "Customer Service", "Room was too cold", "Open");
            complaintsGridView.Rows.Add("4", "Alice Brown", "Art Exhibition", "Other", "Parking was insufficient", "Resolved");
            complaintsGridView.Rows.Add("5", "Charlie Davis", "Sports Tournament", "Technical", "Scoreboard malfunction", "Open");
        }

        private void ResolveButton_Click(object sender, EventArgs e)
        {
            if (complaintsGridView.SelectedRows.Count > 0 && !string.IsNullOrWhiteSpace(responseTextBox.Text))
            {
                complaintsGridView.SelectedRows[0].Cells["Status"].Value = "Resolved";
                MessageBox.Show("Complaint resolved successfully!", "Resolution Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                responseTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please select a complaint and provide a response.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadSampleData(); // In a real application, this would fetch fresh data from the database
            MessageBox.Show("Complaint list refreshed.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void StatusFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string categoryFilter = categoryComboBox.SelectedItem.ToString();
            string statusFilter = statusFilterComboBox.SelectedItem.ToString();

            foreach (DataGridViewRow row in complaintsGridView.Rows)
            {
                bool categoryMatch = categoryFilter == "All Categories" || row.Cells["Category"].Value.ToString() == categoryFilter;
                bool statusMatch = statusFilter == "All Statuses" || row.Cells["Status"].Value.ToString() == statusFilter;

                row.Visible = categoryMatch && statusMatch;
            }
        }
    }
}

