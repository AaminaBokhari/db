using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Linq;

namespace EventVerse
{
    public class AttendeeManagementForm : BaseForm
    {
        private ComboBox eventComboBox;
        private DataGridView attendeesGridView;
        private Button exportButton;
        private Button messageButton;
        private Button refreshButton;
        private TextBox searchTextBox;
        private Button searchButton;
        private Label totalAttendeesLabel;
        private Label checkedInLabel;

        public AttendeeManagementForm()
        {
            InitializeComponents();
            SetTitle("Attendee Management");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Attendee Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(300, 30)
            };

            Label eventLabel = new Label
            {
                Text = "Select Event:",
                Location = new Point(20, 100),
                Size = new Size(100, 20)
            };

            eventComboBox = new ComboBox
            {
                Location = new Point(130, 100),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            eventComboBox.Items.AddRange(new object[] { "Tech Conference 2024", "Music Festival", "Business Workshop" });
            eventComboBox.SelectedIndexChanged += EventComboBox_SelectedIndexChanged;

            searchTextBox = new TextBox
            {
                Location = new Point(350, 100),
                Size = new Size(200, 20),
                Text = "Search attendees...",
                ForeColor = SystemColors.GrayText
            };

            searchTextBox.GotFocus += (s, e) => {
                if (searchTextBox.Text == "Search attendees...")
                {
                    searchTextBox.Text = "";
                    searchTextBox.ForeColor = SystemColors.WindowText;
                }
            };

            searchTextBox.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(searchTextBox.Text))
                {
                    searchTextBox.Text = "Search attendees...";
                    searchTextBox.ForeColor = SystemColors.GrayText;
                }
            };

            searchButton = new Button
            {
                Text = "Search",
                Location = new Point(560, 100),
                Size = new Size(80, 25)
            };
            searchButton.Click += SearchButton_Click;

            attendeesGridView = new DataGridView
            {
                Location = new Point(20, 140),
                Size = new Size(760, 310),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            attendeesGridView.Columns.Add("Name", "Name");
            attendeesGridView.Columns.Add("Email", "Email");
            attendeesGridView.Columns.Add("TicketType", "Ticket Type");
            attendeesGridView.Columns.Add("CheckedIn", "Checked In");

            exportButton = new Button
            {
                Text = "Export",
                Location = new Point(20, 470),
                Size = new Size(100, 30)
            };
            exportButton.Click += ExportButton_Click;

            messageButton = new Button
            {
                Text = "Message",
                Location = new Point(130, 470),
                Size = new Size(100, 30)
            };
            messageButton.Click += MessageButton_Click;

            refreshButton = new Button
            {
                Text = "Refresh",
                Location = new Point(240, 470),
                Size = new Size(100, 30)
            };
            refreshButton.Click += RefreshButton_Click;

            totalAttendeesLabel = new Label
            {
                Text = "Total Attendees: 0",
                Location = new Point(20, 520),
                Size = new Size(200, 20)
            };

            checkedInLabel = new Label
            {
                Text = "Checked In: 0",
                Location = new Point(230, 520),
                Size = new Size(200, 20)
            };

            this.Controls.AddRange(new Control[] {
                titleLabel, eventLabel, eventComboBox, searchTextBox, searchButton,
                attendeesGridView, exportButton, messageButton, refreshButton,
                totalAttendeesLabel, checkedInLabel
            });

            LoadAttendees();
        }

        private void EventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAttendees();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            // Implement search functionality
            MessageBox.Show("Search functionality to be implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            // Implement export functionality
            MessageBox.Show("Export functionality to be implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MessageButton_Click(object sender, EventArgs e)
        {
            if (attendeesGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an attendee to message.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string attendeeName = attendeesGridView.SelectedRows[0].Cells["Name"].Value.ToString();
            string attendeeEmail = attendeesGridView.SelectedRows[0].Cells["Email"].Value.ToString();

            using (var messageForm = new AttendeeMessageForm(attendeeName, attendeeEmail))
            {
                messageForm.ShowDialog();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadAttendees();
        }

        private void LoadAttendees()
        {
            // Clear existing rows
            attendeesGridView.Rows.Clear();

            // Add sample data
            attendeesGridView.Rows.Add("John Doe", "john@example.com", "VIP", "Yes");
            attendeesGridView.Rows.Add("Jane Smith", "jane@example.com", "Regular", "No");
            attendeesGridView.Rows.Add("Bob Johnson", "bob@example.com", "VIP", "Yes");

            // Update summary labels
            totalAttendeesLabel.Text = $"Total Attendees: {attendeesGridView.Rows.Count}";
            int checkedIn = attendeesGridView.Rows.Cast<DataGridViewRow>()
                .Count(r => r.Cells["CheckedIn"].Value.ToString() == "Yes");
            checkedInLabel.Text = $"Checked In: {checkedIn}";
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
            // AttendeeManagementForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "AttendeeManagementForm";
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
        }

        private void logoBox_Click(object sender, EventArgs e)
        {
            // Implement logo click functionality if needed
        }
    }

    public class AttendeeMessageForm : Form
    {
        private TextBox subjectTextBox;
        private RichTextBox messageTextBox;
        private Button sendButton;
        private Button cancelButton;

        public AttendeeMessageForm(string attendeeName, string attendeeEmail)
        {
            InitializeComponents(attendeeName, attendeeEmail);
        }

        private void InitializeComponents(string attendeeName, string attendeeEmail)
        {
            this.Text = $"Message to {attendeeName}";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;

            Label recipientLabel = new Label
            {
                Text = $"To: {attendeeName} ({attendeeEmail})",
                Location = new Point(10, 10),
                Size = new Size(380, 20)
            };

            Label subjectLabel = new Label
            {
                Text = "Subject:",
                Location = new Point(10, 40),
                Size = new Size(60, 20)
            };

            subjectTextBox = new TextBox
            {
                Location = new Point(70, 40),
                Size = new Size(300, 20)
            };

            Label messageLabel = new Label
            {
                Text = "Message:",
                Location = new Point(10, 70),
                Size = new Size(60, 20)
            };

            messageTextBox = new RichTextBox
            {
                Location = new Point(10, 100),
                Size = new Size(360, 120)
            };

            sendButton = new Button
            {
                Text = "Send",
                Location = new Point(200, 230),
                Size = new Size(80, 30)
            };
            sendButton.Click += SendButton_Click;

            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(290, 230),
                Size = new Size(80, 30)
            };
            cancelButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] {
                recipientLabel, subjectLabel, subjectTextBox,
                messageLabel, messageTextBox, sendButton, cancelButton
            });
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(subjectTextBox.Text) || string.IsNullOrWhiteSpace(messageTextBox.Text))
            {
                MessageBox.Show("Please enter both subject and message.", "Incomplete Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Here you would typically implement the logic to send the message
            MessageBox.Show("Message sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}