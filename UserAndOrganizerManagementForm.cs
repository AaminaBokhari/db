using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EventVerse
{
    public class UserAndOrganizerManagementForm : BaseForm
    {
        private TabControl tabControl;
        private DataGridView usersGridView;
        private DataGridView organizersGridView;
        private Button editUserButton;
        private Button deleteUserButton;
        private Button editOrganizerButton;
        private Button deleteOrganizerButton;

        public UserAndOrganizerManagementForm()
        {
            InitializeComponents();
            SetTitle("User and Organizer Management");
        }

        private void InitializeComponents()
        {
            // Set form properties
            this.Size = new Size(1024, 768);
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Add title label at the top with proper spacing
            Label titleLabel = new Label
            {
                Text = "User and Organizer Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(0, 20, 0, 0)
            };

            // Initialize TabControl with proper spacing
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(10, 10),
                Font = new Font("Arial", 12)
            };

            TabPage usersTab = new TabPage("Users");
            TabPage organizersTab = new TabPage("Organizers");

            // Configure the layout for tabs with additional top padding
            usersTab.Padding = new Padding(10, 20, 10, 10);
            organizersTab.Padding = new Padding(10, 20, 10, 10);

            // Create panel to hold buttons, grid, and user marking
            Panel usersPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            // Create button panel for users
            FlowLayoutPanel userButtonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Width = 160,
                Height = 100,
                Dock = DockStyle.Left,
                Padding = new Padding(0, 10, 10, 0)
            };

            editUserButton = CreateButton("Edit User");
            deleteUserButton = CreateButton("Delete User");
            userButtonPanel.Controls.AddRange(new Control[] { editUserButton, deleteUserButton });

            // Create and configure DataGridViews
            usersGridView = CreateDataGridView();
            usersGridView.Columns.Add("ID", "User ID");
            usersGridView.Columns.Add("Name", "Name");
            usersGridView.Columns.Add("Email", "Email");
            usersGridView.Columns.Add("JoinDate", "Join Date");
            LoadSampleUserData();

            organizersGridView = CreateDataGridView();
            organizersGridView.Columns.Add("ID", "Organizer ID");
            organizersGridView.Columns.Add("Name", "Name");
            organizersGridView.Columns.Add("Email", "Email");
            organizersGridView.Columns.Add("Company", "Company");
            LoadSampleOrganizerData();

            // Create panel for organizers
            Panel organizersPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            // Create button panel for organizers
            FlowLayoutPanel organizerButtonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Width = 160,
                Height = 100,
                Dock = DockStyle.Left,
                Padding = new Padding(0, 10, 10, 0)
            };

            editOrganizerButton = CreateButton("Edit Organizer");
            deleteOrganizerButton = CreateButton("Delete Organizer");
            organizerButtonPanel.Controls.AddRange(new Control[] { editOrganizerButton, deleteOrganizerButton });

            // Add controls to panels
            usersPanel.Controls.Add(userButtonPanel);
            usersPanel.Controls.Add(usersGridView);

            organizersPanel.Controls.Add(organizerButtonPanel);
            organizersPanel.Controls.Add(organizersGridView);

            // Add panels to tabs
            usersTab.Controls.Add(usersPanel);
            organizersTab.Controls.Add(organizersPanel);

            // Add tabs to TabControl
            tabControl.TabPages.Add(usersTab);
            tabControl.TabPages.Add(organizersTab);

            // Add controls to form
            this.Controls.Add(tabControl);
            this.Controls.Add(titleLabel);

            // Wire up event handlers
            editUserButton.Click += EditUserButton_Click;
            deleteUserButton.Click += DeleteUserButton_Click;
            editOrganizerButton.Click += EditOrganizerButton_Click;
            deleteOrganizerButton.Click += DeleteOrganizerButton_Click;
        }

        private DataGridView CreateDataGridView()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 12),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.None,
                RowHeadersWidth = 51,
                Margin = new Padding(0, 10, 0, 0)
            };
        }

        private Button CreateButton(string text)
        {
            return new Button
            {
                Text = text,
                Font = new Font("Arial", 12),
                Size = new Size(150, 40),
                Margin = new Padding(0, 0, 0, 10),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        private void LoadSampleUserData()
        {
            usersGridView.Rows.Add("1", "John Doe", "john@example.com", "2023-01-15");
            usersGridView.Rows.Add("2", "Jane Smith", "jane@example.com", "2023-02-20");
            usersGridView.Rows.Add("3", "Bob Johnson", "bob@example.com", "2023-03-10");
        }

        private void LoadSampleOrganizerData()
        {
            organizersGridView.Rows.Add("1", "Alice Brown", "alice@techorg.com", "TechOrg");
            organizersGridView.Rows.Add("2", "Charlie Davis", "charlie@musicco.com", "MusicCo");
            organizersGridView.Rows.Add("3", "Eve Wilson", "eve@bizgroup.com", "BizGroup");
        }

        private void EditUser(string userId)
        {
            using (var form = new Form())
            {
                form.Text = $"Edit User {userId}";
                form.Size = new Size(400, 250);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                TableLayoutPanel layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    RowCount = 4,
                    ColumnCount = 2
                };

                var nameLabel = new Label { Text = "Name:", Dock = DockStyle.Fill };
                var nameTextBox = new TextBox
                {
                    Text = usersGridView.SelectedRows[0].Cells["Name"].Value.ToString(),
                    Dock = DockStyle.Fill
                };

                var emailLabel = new Label { Text = "Email:", Dock = DockStyle.Fill };
                var emailTextBox = new TextBox
                {
                    Text = usersGridView.SelectedRows[0].Cells["Email"].Value.ToString(),
                    Dock = DockStyle.Fill
                };

                var saveButton = new Button
                {
                    Text = "Save",
                    DialogResult = DialogResult.OK,
                    Dock = DockStyle.Right
                };

                layout.Controls.Add(nameLabel, 0, 0);
                layout.Controls.Add(nameTextBox, 1, 0);
                layout.Controls.Add(emailLabel, 0, 1);
                layout.Controls.Add(emailTextBox, 1, 1);
                layout.Controls.Add(saveButton, 1, 3);

                form.Controls.Add(layout);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(nameTextBox.Text) && !string.IsNullOrEmpty(emailTextBox.Text))
                    {
                        usersGridView.SelectedRows[0].Cells["Name"].Value = nameTextBox.Text;
                        usersGridView.SelectedRows[0].Cells["Email"].Value = emailTextBox.Text;
                        MessageBox.Show($"User {userId} updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Name and Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void EditOrganizer(string organizerId)
        {
            using (var form = new Form())
            {
                form.Text = $"Edit Organizer {organizerId}";
                form.Size = new Size(400, 300);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                TableLayoutPanel layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    RowCount = 5,
                    ColumnCount = 2
                };

                var nameLabel = new Label { Text = "Name:", Dock = DockStyle.Fill };
                var nameTextBox = new TextBox
                {
                    Text = organizersGridView.SelectedRows[0].Cells["Name"].Value.ToString(),
                    Dock = DockStyle.Fill
                };

                var emailLabel = new Label { Text = "Email:", Dock = DockStyle.Fill };
                var emailTextBox = new TextBox
                {
                    Text = organizersGridView.SelectedRows[0].Cells["Email"].Value.ToString(),
                    Dock = DockStyle.Fill
                };

                var companyLabel = new Label { Text = "Company:", Dock = DockStyle.Fill };
                var companyTextBox = new TextBox
                {
                    Text = organizersGridView.SelectedRows[0].Cells["Company"].Value.ToString(),
                    Dock = DockStyle.Fill
                };

                var saveButton = new Button
                {
                    Text = "Save",
                    DialogResult = DialogResult.OK,
                    Dock = DockStyle.Right
                };

                layout.Controls.Add(nameLabel, 0, 0);
                layout.Controls.Add(nameTextBox, 1, 0);
                layout.Controls.Add(emailLabel, 0, 1);
                layout.Controls.Add(emailTextBox, 1, 1);
                layout.Controls.Add(companyLabel, 0, 2);
                layout.Controls.Add(companyTextBox, 1, 2);
                layout.Controls.Add(saveButton, 1, 4);

                form.Controls.Add(layout);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(nameTextBox.Text) &&
                        !string.IsNullOrEmpty(emailTextBox.Text) &&
                        !string.IsNullOrEmpty(companyTextBox.Text))
                    {
                        organizersGridView.SelectedRows[0].Cells["Name"].Value = nameTextBox.Text;
                        organizersGridView.SelectedRows[0].Cells["Email"].Value = emailTextBox.Text;
                        organizersGridView.SelectedRows[0].Cells["Company"].Value = companyTextBox.Text;
                        MessageBox.Show($"Organizer {organizerId} updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void EditUserButton_Click(object sender, EventArgs e)
        {
            if (usersGridView.SelectedRows.Count > 0)
            {
                string userId = usersGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                EditUser(userId);
            }
            else
            {
                MessageBox.Show("Please select a user to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            if (usersGridView.SelectedRows.Count > 0)
            {
                string userId = usersGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                if (MessageBox.Show($"Are you sure you want to delete user with ID: {userId}?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    usersGridView.Rows.RemoveAt(usersGridView.SelectedRows[0].Index);
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditOrganizerButton_Click(object sender, EventArgs e)
        {
            if (organizersGridView.SelectedRows.Count > 0)
            {
                string organizerId = organizersGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                EditOrganizer(organizerId);
            }
            else
            {
                MessageBox.Show("Please select an organizer to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteOrganizerButton_Click(object sender, EventArgs e)
        {
            if (organizersGridView.SelectedRows.Count > 0)
            {
                string organizerId = organizersGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                if (MessageBox.Show($"Are you sure you want to delete organizer with ID: {organizerId}?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    organizersGridView.Rows.RemoveAt(organizersGridView.SelectedRows[0].Index);
                    MessageBox.Show("Organizer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select an organizer to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

