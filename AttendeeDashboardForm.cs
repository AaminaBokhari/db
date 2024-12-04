using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class AttendeeDashboardForm : Form
    {
        private const int FORM_PADDING = 20;
        private const int CONTROL_SPACING = 10;
        private const int BUTTON_HEIGHT = 40;
        private const int BUTTON_WIDTH = 140;
        private const int HEADER_HEIGHT = 60;
        private const int BACK_BUTTON_WIDTH = 100;
        private const int BACK_BUTTON_HEIGHT = 30;
        private const int BACK_BUTTON_MARGIN = 15;

        private Panel headerPanel;
        private Label titleLabel;
        private Button backButton;
        private Panel mainContainer;
        private Panel filterPanel;
        private Label filterLabel;
        private ComboBox filterComboBox;
        private DataGridView eventsGridView;
        private FlowLayoutPanel buttonPanel;
        private DataTable fullEventData;

        private Button[] navigationButtons;
        private string[] buttonTexts = new string[]
        {
            "Profile",
            "Tickets",
            "Feedback",
            "Check In",
            "Purchase Tickets",
            "Search Events",
            "Notifications",
            "Post Query"
        };

        public AttendeeDashboardForm()
        {
            InitializeForm();
            LoadEventData();
            AttachEventHandlers();
        }

        private void InitializeForm()
        {
            this.Text = "EventVerse - Attendee Dashboard";
            this.Size = new Size(1024, 768);
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            InitializeComponents();
            SetupLayout();
        }

        private void InitializeComponents()
        {
            // Header Panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = HEADER_HEIGHT,
                BackColor = Color.FromArgb(0, 102, 204),
                Padding = new Padding(BACK_BUTTON_MARGIN, 0, BACK_BUTTON_MARGIN, 0)
            };

            // Create a container for the title
            Panel titleContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            // Back Button
            backButton = new Button
            {
                Text = "? Back",
                Size = new Size(BACK_BUTTON_WIDTH, BACK_BUTTON_HEIGHT),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Left,
                Margin = new Padding(0, (HEADER_HEIGHT - BACK_BUTTON_HEIGHT) / 2, 0, 0)
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Click += BackButton_Click;

            // Hover effects for back button
            backButton.MouseEnter += (s, e) => { backButton.BackColor = Color.FromArgb(0, 122, 224); };
            backButton.MouseLeave += (s, e) => { backButton.BackColor = Color.FromArgb(0, 102, 204); };

            titleLabel = new Label
            {
                Text = "Attendee Dashboard",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            // Add title to its container
            titleContainer.Controls.Add(titleLabel);

            // Add controls to header panel
            headerPanel.Controls.Add(titleContainer);
            headerPanel.Controls.Add(backButton);

            // Main Container Panel
            mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(FORM_PADDING)
            };

            // Filter Panel
            filterPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                Padding = new Padding(0)
            };

            filterLabel = new Label
            {
                Text = "Filter Events:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };

            filterComboBox = new ComboBox
            {
                Width = 200,
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            filterComboBox.Items.AddRange(new string[] { "All Events", "Registered Events", "Upcoming Events" });
            filterComboBox.SelectedIndex = 0;

            filterPanel.Controls.AddRange(new Control[] { filterLabel, filterComboBox });

            // Events Grid
            eventsGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(0, CONTROL_SPACING, 0, CONTROL_SPACING)
            };

            SetupDataGridViewStyle();

            // Button Panel
            buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                Padding = new Padding(FORM_PADDING),
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            // Create Navigation Buttons
            navigationButtons = new Button[buttonTexts.Length];
            for (int i = 0; i < buttonTexts.Length; i++)
            {
                navigationButtons[i] = CreateNavigationButton(buttonTexts[i]);
                buttonPanel.Controls.Add(navigationButtons[i]);
            }

            // Add components to main container in correct order
            mainContainer.Controls.Add(eventsGridView);
            mainContainer.Controls.Add(buttonPanel);
            mainContainer.Controls.Add(filterPanel);

            // Add main components to form
            this.Controls.Add(mainContainer);
            this.Controls.Add(headerPanel);
        }

        private void SetupDataGridViewStyle()
        {
            eventsGridView.EnableHeadersVisualStyles = false;
            eventsGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            eventsGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            eventsGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            eventsGridView.ColumnHeadersHeight = 40;

            eventsGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            eventsGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            eventsGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 140, 220);
            eventsGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            eventsGridView.RowTemplate.Height = 35;
        }

        private Button CreateNavigationButton(string text)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(5)
            };

            button.FlatAppearance.BorderSize = 0;
            button.Click += (s, e) => OpenForm(text);

            // Hover effects
            button.MouseEnter += (s, e) => { button.BackColor = Color.FromArgb(0, 122, 224); };
            button.MouseLeave += (s, e) => { button.BackColor = Color.FromArgb(0, 102, 204); };

            return button;
        }

        private void LoadEventData()
        {
            fullEventData = new DataTable();
            fullEventData.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Event Name", typeof(string)),
                new DataColumn("Date", typeof(DateTime)),
                new DataColumn("Location", typeof(string)),
                new DataColumn("Status", typeof(string))
            });

            // Add sample data
            fullEventData.Rows.Add("Tech Conference 2024", new DateTime(2024, 6, 15), "New York", "Registered");
            fullEventData.Rows.Add("Music Festival", new DateTime(2024, 7, 20), "Los Angeles", "Attended");
            fullEventData.Rows.Add("Business Workshop", new DateTime(2024, 8, 5), "Chicago", "Upcoming");
            fullEventData.Rows.Add("Art Exhibition", new DateTime(2024, 5, 10), "San Francisco", "Registered");
            fullEventData.Rows.Add("Food Festival", new DateTime(2024, 9, 1), "Miami", "Upcoming");
            fullEventData.Rows.Add("Tech Meetup", new DateTime(2024, 4, 30), "Boston", "Attended");

            eventsGridView.DataSource = fullEventData;

            // Format the Date column to show only the date part
            eventsGridView.Columns["Date"].DefaultCellStyle.Format = "d";
        }

        private void AttachEventHandlers()
        {
            this.Resize += AttendeeDashboardForm_Resize;
            filterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;
        }

        private void SetupLayout()
        {
            filterLabel.Location = new Point(0, (filterPanel.Height - filterLabel.Height) / 2);
            filterComboBox.Location = new Point(
                filterLabel.Right + CONTROL_SPACING,
                (filterPanel.Height - filterComboBox.Height) / 2
            );
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to go back? Any unsaved changes will be lost.",
                "Confirm Navigation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void AttendeeDashboardForm_Resize(object sender, EventArgs e)
        {
            SetupLayout();
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable filteredData = fullEventData.Clone();
            string selectedFilter = filterComboBox.SelectedItem.ToString();

            foreach (DataRow row in fullEventData.Rows)
            {
                bool includeRow = false;

                switch (selectedFilter)
                {
                    case "All Events":
                        includeRow = true;
                        break;
                    case "Registered Events":
                        includeRow = row["Status"].ToString() == "Registered";
                        break;
                    case "Upcoming Events":
                        includeRow = Convert.ToDateTime(row["Date"]) >= DateTime.Now;
                        break;
                }

                if (includeRow)
                {
                    filteredData.ImportRow(row);
                }
            }

            eventsGridView.DataSource = filteredData;
        }

        private void OpenForm(string formName)
        {
            Form form = null;

            // Helper function to get selected event name
            string GetSelectedEventName()
            {
                if (eventsGridView.SelectedRows.Count > 0)
                {
                    string selectedEvent = eventsGridView.SelectedRows[0].Cells["Event Name"].Value?.ToString();
                    if (!string.IsNullOrEmpty(selectedEvent))
                    {
                        return selectedEvent;
                    }
                    MessageBox.Show("The selected event is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                MessageBox.Show("Please select an event first.", "Select Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            switch (formName)
            {
                case "Profile":
                    form = new ProfileManagementForm();
                    break;
                case "Tickets":
                    form = new TicketManagementForm();
                    break;
                case "Feedback":
                    string feedbackEvent = GetSelectedEventName();
                    if (feedbackEvent != null)
                    {
                        form = new FeedbackForm(feedbackEvent);
                    }
                    break;
                case "Check In":
                    string checkInEvent = GetSelectedEventName();
                    if (checkInEvent != null)
                    {
                        form = new CheckInForm(checkInEvent);
                    }
                    break;
                case "Purchase Tickets":
                    form = new EventBookingForm();
                    break;
                case "Search Events":
                    form = new EventSearchForm();
                    break;
                case "Notifications":
                    form = new NotificationCenterForm();
                    break;
                case "Post Query":
                    string selectedEvent = GetSelectedEventName();
                    if (selectedEvent != null)
                    {
                        OpenPostQueryForm(selectedEvent);
                    }
                    break;
                default:
                    MessageBox.Show("The specified form is not recognized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            form?.ShowDialog();
        }

        private void OpenPostQueryForm(string eventName)
        {
            using (var form = new PostQueryForm(eventName))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Your query has been submitted successfully.", "Query Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}