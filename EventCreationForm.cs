using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class EventCreationForm : BaseForm
    {
        private TextBox eventNameTextBox;
        private TextBox descriptionTextBox;
        private TextBox locationTextBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private ComboBox categoryComboBox;
        private NumericUpDown capacityNumericUpDown;
        private DataGridView ticketTiersDataGridView;
        private Button addTierButton;
        private Button removeButton;
        private CheckedListBox customFieldsCheckedListBox;
        private Button createButton;
        private Button backButton;

        public EventCreationForm()
        {
            InitializeComponents();
            SetTitle("Create New Event");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(600, 700); // Increased height to accommodate moved fields
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            Panel contentPanel = new Panel
            {
                AutoSize = true,
                AutoScroll = true,
                Dock = DockStyle.Fill
            };

            this.Controls.Add(contentPanel);

            // Title Label
            Label titleLabel = new Label
            {
                Text = "Create New Event",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(200, 30)
            };

            int startY = 100; // Increased from 70 to move fields down
            int labelX = 20;
            int controlX = 140;
            int spacing = 35;
            int controlWidth = 400;

            // Event Name
            Label eventNameLabel = new Label
            {
                Text = "Name:",
                Location = new Point(labelX, startY),
                Size = new Size(100, 20)
            };
            eventNameTextBox = new TextBox
            {
                Location = new Point(controlX, startY),
                Size = new Size(controlWidth, 20)
            };

            // Description
            Label descriptionLabel = new Label
            {
                Text = "Description:",
                Location = new Point(labelX, startY + spacing),
                Size = new Size(100, 20)
            };
            descriptionTextBox = new TextBox
            {
                Location = new Point(controlX, startY + spacing),
                Size = new Size(controlWidth, 60),
                Multiline = true
            };

            // Location
            Label locationLabel = new Label
            {
                Text = "Location:",
                Location = new Point(labelX, startY + spacing * 4),
                Size = new Size(100, 20)
            };
            locationTextBox = new TextBox
            {
                Location = new Point(controlX, startY + spacing * 4),
                Size = new Size(controlWidth, 20)
            };

            // Start Date
            Label startDateLabel = new Label
            {
                Text = "Start Date:",
                Location = new Point(labelX, startY + spacing * 5),
                Size = new Size(100, 20)
            };
            startDatePicker = new DateTimePicker
            {
                Location = new Point(controlX, startY + spacing * 5),
                Size = new Size(200, 20),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dddd, dd MMMM yyyy"
            };

            // End Date
            Label endDateLabel = new Label
            {
                Text = "End Date:",
                Location = new Point(labelX, startY + spacing * 6),
                Size = new Size(100, 20)
            };
            endDatePicker = new DateTimePicker
            {
                Location = new Point(controlX, startY + spacing * 6),
                Size = new Size(200, 20),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dddd, dd MMMM yyyy"
            };

            // Category
            Label categoryLabel = new Label
            {
                Text = "Category:",
                Location = new Point(labelX, startY + spacing * 7),
                Size = new Size(100, 20)
            };
            categoryComboBox = new ComboBox
            {
                Location = new Point(controlX, startY + spacing * 7),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            categoryComboBox.Items.AddRange(new object[] { "Conference", "Workshop", "Concert", "Sports", "Art Exhibition", "Networking" });

            // Capacity
            Label capacityLabel = new Label
            {
                Text = "Capacity:",
                Location = new Point(labelX, startY + spacing * 8),
                Size = new Size(100, 20)
            };
            capacityNumericUpDown = new NumericUpDown
            {
                Location = new Point(controlX, startY + spacing * 8),
                Size = new Size(80, 20),
                Minimum = 1,
                Maximum = 1000000
            };

            // Ticket Tiers
            Label ticketTiersLabel = new Label
            {
                Text = "Ticket Tiers:",
                Location = new Point(labelX, startY + spacing * 9),
                Size = new Size(100, 20)
            };
            ticketTiersDataGridView = new DataGridView
            {
                Location = new Point(controlX, startY + spacing * 9),
                Size = new Size(controlWidth, 150),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };
            ticketTiersDataGridView.Columns.Add("TierName", "Tier Name");
            ticketTiersDataGridView.Columns.Add("Price", "Price");
            ticketTiersDataGridView.Columns.Add("Quantity", "Quantity");

            addTierButton = new Button
            {
                Text = "Add Tier",
                Location = new Point(controlX, startY + spacing * 14),
                Size = new Size(100, 30),
                BackColor = SystemColors.Control
            };
            addTierButton.Click += AddTierButton_Click;

            removeButton = new Button
            {
                Text = "Remove",
                Location = new Point(controlX + 110, startY + spacing * 14),
                Size = new Size(100, 30),
                BackColor = SystemColors.Control
            };
            removeButton.Click += RemoveButton_Click;

            // Custom Fields
            Label customFieldsLabel = new Label
            {
                Text = "Custom Fields:",
                Location = new Point(labelX, startY + spacing * 15),
                Size = new Size(100, 20)
            };
            customFieldsCheckedListBox = new CheckedListBox
            {
                Location = new Point(controlX, startY + spacing * 15),
                Size = new Size(200, 80),
                BorderStyle = BorderStyle.FixedSingle
            };
            customFieldsCheckedListBox.Items.AddRange(new object[] { "Company", "Job Title", "Dietary Requirements", "T-Shirt Size" });

            // Buttons at the bottom
            backButton = new Button
            {
                Text = "Back",
                Location = new Point(labelX, startY + spacing * 19),
                Size = new Size(100, 30),
                BackColor = SystemColors.Control
            };
            backButton.Click += (sender, e) => this.Close();

            createButton = new Button
            {
                Text = "Create",
                Location = new Point(controlX, startY + spacing * 19),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            createButton.Click += CreateButton_Click;

            // Add controls to the content panel
            contentPanel.Controls.AddRange(new Control[] {
                titleLabel,
                eventNameLabel, eventNameTextBox,
                descriptionLabel, descriptionTextBox,
                locationLabel, locationTextBox,
                startDateLabel, startDatePicker,
                endDateLabel, endDatePicker,
                categoryLabel, categoryComboBox,
                capacityLabel, capacityNumericUpDown,
                ticketTiersLabel, ticketTiersDataGridView,
                addTierButton, removeButton,
                customFieldsLabel, customFieldsCheckedListBox,
                backButton, createButton
            });
        }

        private void AddTierButton_Click(object sender, EventArgs e)
        {
            ticketTiersDataGridView.Rows.Add("New Tier", "0", "0");
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (ticketTiersDataGridView.SelectedRows.Count > 0)
            {
                ticketTiersDataGridView.Rows.RemoveAt(ticketTiersDataGridView.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Please select a tier to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Event newEvent = CreateEventObject();
                SaveEvent(newEvent);
                MessageBox.Show("Event created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(eventNameTextBox.Text))
            {
                MessageBox.Show("Please enter an event name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(locationTextBox.Text))
            {
                MessageBox.Show("Please enter an event location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (startDatePicker.Value >= endDatePicker.Value)
            {
                MessageBox.Show("End date must be after start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (categoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private Event CreateEventObject()
        {
            return new Event
            {
                Name = eventNameTextBox.Text,
                Description = descriptionTextBox.Text,
                Location = locationTextBox.Text,
                StartDate = startDatePicker.Value,
                EndDate = endDatePicker.Value,
                Category = categoryComboBox.SelectedItem?.ToString(),
                Capacity = (int)capacityNumericUpDown.Value,
                TicketTiers = GetTicketTiersFromGrid(),
                CustomFields = GetSelectedCustomFields()
            };
        }

        private List<string> GetSelectedCustomFields()
        {
            List<string> fields = new List<string>();
            foreach (object item in customFieldsCheckedListBox.CheckedItems)
            {
                fields.Add(item.ToString());
            }
            return fields;
        }

        private List<TicketTier> GetTicketTiersFromGrid()
        {
            List<TicketTier> ticketTiers = new List<TicketTier>();
            foreach (DataGridViewRow row in ticketTiersDataGridView.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    ticketTiers.Add(new TicketTier
                    {
                        Name = row.Cells[0].Value.ToString(),
                        Price = decimal.Parse(row.Cells[1].Value?.ToString() ?? "0"),
                        Quantity = int.Parse(row.Cells[2].Value?.ToString() ?? "0")
                    });
                }
            }
            return ticketTiers;
        }

        private void SaveEvent(Event newEvent)
        {
            // TODO: Implement database saving logic
            System.Threading.Thread.Sleep(500);
        }
    }

    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public int Capacity { get; set; }
        public List<TicketTier> TicketTiers { get; set; }
        public List<string> CustomFields { get; set; }
    }

    public class TicketTier
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

