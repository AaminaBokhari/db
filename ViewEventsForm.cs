using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class ViewEventsForm : BaseForm
    {
        private DataGridView eventsGridView;
        private Button refreshButton;
        private Button viewDetailsButton;
        private ComboBox filterStatusComboBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;

        public ViewEventsForm()
        {
            InitializeComponents();
            SetTitle("View Events");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Available Events",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Location = new Point(50, 30),
                Size = new Size(800, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label filterStatusLabel = new Label
            {
                Text = "Filter by Status:",
                Location = new Point(50, 80),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            filterStatusComboBox = new ComboBox
            {
                Location = new Point(160, 80),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            filterStatusComboBox.Items.AddRange(new object[] { "All", "Open for bids", "Bidding closed", "Upcoming" });
            filterStatusComboBox.SelectedIndex = 0;
            filterStatusComboBox.SelectedIndexChanged += FilterStatusComboBox_SelectedIndexChanged;

            Label startDateLabel = new Label
            {
                Text = "Start Date:",
                Location = new Point(320, 80),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            startDatePicker = new DateTimePicker
            {
                Location = new Point(410, 80),
                Size = new Size(150, 25),
                Format = DateTimePickerFormat.Short
            };
            startDatePicker.ValueChanged += DatePicker_ValueChanged;

            Label endDateLabel = new Label
            {
                Text = "End Date:",
                Location = new Point(570, 80),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            endDatePicker = new DateTimePicker
            {
                Location = new Point(660, 80),
                Size = new Size(150, 25),
                Format = DateTimePickerFormat.Short
            };
            endDatePicker.ValueChanged += DatePicker_ValueChanged;

            eventsGridView = new DataGridView
            {
                Location = new Point(50, 120),
                Size = new Size(800, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            eventsGridView.Columns.Add("EventName", "Event Name");
            eventsGridView.Columns.Add("Date", "Date");
            eventsGridView.Columns.Add("Location", "Location");
            eventsGridView.Columns.Add("Status", "Status");
            eventsGridView.Columns.Add("Category", "Category");

            refreshButton = new Button
            {
                Text = "Refresh",
                Location = new Point(50, 540),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            refreshButton.Click += RefreshButton_Click;

            viewDetailsButton = new Button
            {
                Text = "View Event Details",
                Location = new Point(220, 540),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            viewDetailsButton.Click += ViewDetailsButton_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(filterStatusLabel);
            this.Controls.Add(filterStatusComboBox);
            this.Controls.Add(startDateLabel);
            this.Controls.Add(startDatePicker);
            this.Controls.Add(endDateLabel);
            this.Controls.Add(endDatePicker);
            this.Controls.Add(eventsGridView);
            this.Controls.Add(refreshButton);
            this.Controls.Add(viewDetailsButton);

            LoadEvents();
        }

        private void LoadEvents(string statusFilter = "All")
        {
            eventsGridView.Rows.Clear();
            // In a real application, this would fetch data from a database
            var events = new[]
            {
                new { Name = "Tech Conference 2024", Date = new DateTime(2024, 6, 15), Location = "New York", Status = "Open for bids", Category = "Technology" },
                new { Name = "Music Festival", Date = new DateTime(2024, 7, 20), Location = "Los Angeles", Status = "Bidding closed", Category = "Entertainment" },
                new { Name = "Business Workshop", Date = new DateTime(2024, 8, 5), Location = "Chicago", Status = "Open for bids", Category = "Business" },
                new { Name = "Sports Event", Date = new DateTime(2024, 9, 10), Location = "Miami", Status = "Upcoming", Category = "Sports" }
            };

            foreach (var evt in events)
            {
                if (statusFilter == "All" || evt.Status == statusFilter)
                {
                    if (evt.Date >= startDatePicker.Value.Date && evt.Date <= endDatePicker.Value.Date)
                    {
                        eventsGridView.Rows.Add(evt.Name, evt.Date.ToShortDateString(), evt.Location, evt.Status, evt.Category);
                    }
                }
            }
        }

        private void FilterStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEvents(filterStatusComboBox.SelectedItem.ToString());
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            LoadEvents(filterStatusComboBox.SelectedItem.ToString());
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadEvents(filterStatusComboBox.SelectedItem.ToString());
            MessageBox.Show("Events list refreshed.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ViewDetailsButton_Click(object sender, EventArgs e)
        {
            if (eventsGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = eventsGridView.SelectedRows[0];
                string details = $"Event: {selectedRow.Cells["EventName"].Value}\n" +
                                 $"Date: {selectedRow.Cells["Date"].Value}\n" +
                                 $"Location: {selectedRow.Cells["Location"].Value}\n" +
                                 $"Status: {selectedRow.Cells["Status"].Value}\n" +
                                 $"Category: {selectedRow.Cells["Category"].Value}";

                MessageBox.Show(details, "Event Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an event to view details.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}