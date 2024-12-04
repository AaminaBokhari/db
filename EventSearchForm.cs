using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EventVerse
{
    public class EventSearchForm : BaseForm
    {
        private TextBox keywordTextBox;
        private ComboBox categoryComboBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private TextBox locationTextBox;
        private ComboBox priceRangeComboBox;
        private CheckBox freeEventsCheckBox;
        private Button searchButton;
        private DataGridView resultsGridView;

        public EventSearchForm()
        {
            InitializeComponents();
            SetTitle("Event Search");
        }

        private void InitializeComponents()
        {
            // Keyword search
            Label keywordLabel = new Label
            {
                Text = "Keyword:",
                Location = new Point(50, 100),
                Size = new Size(100, 20)
            };

            keywordTextBox = new TextBox
            {
                Location = new Point(160, 100),
                Size = new Size(200, 20)
            };

            // Category filter
            Label categoryLabel = new Label
            {
                Text = "Category:",
                Location = new Point(50, 140),
                Size = new Size(100, 20)
            };

            categoryComboBox = new ComboBox
            {
                Location = new Point(160, 140),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            categoryComboBox.Items.AddRange(new object[] { "All Categories", "Conference", "Workshop", "Concert", "Sports", "Art Exhibition", "Networking" });
            categoryComboBox.SelectedIndex = 0;

            // Date range
            Label dateRangeLabel = new Label
            {
                Text = "Date Range:",
                Location = new Point(50, 180),
                Size = new Size(100, 20)
            };

            startDatePicker = new DateTimePicker
            {
                Location = new Point(160, 180),
                Size = new Size(120, 20),
                Format = DateTimePickerFormat.Short
            };

            Label toLabel = new Label
            {
                Text = "to",
                Location = new Point(290, 180),
                Size = new Size(30, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            endDatePicker = new DateTimePicker
            {
                Location = new Point(330, 180),
                Size = new Size(120, 20),
                Format = DateTimePickerFormat.Short
            };

            // Location
            Label locationLabel = new Label
            {
                Text = "Location:",
                Location = new Point(50, 220),
                Size = new Size(100, 20)
            };

            locationTextBox = new TextBox
            {
                Location = new Point(160, 220),
                Size = new Size(200, 20)
            };

            // Price range
            Label priceRangeLabel = new Label
            {
                Text = "Price Range:",
                Location = new Point(50, 260),
                Size = new Size(100, 20)
            };

            priceRangeComboBox = new ComboBox
            {
                Location = new Point(160, 260),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            priceRangeComboBox.Items.AddRange(new object[] { "All Prices", "Under $25", "$25 - $50", "$50 - $100", "$100 - $250", "Over $250" });
            priceRangeComboBox.SelectedIndex = 0;

            // Free events checkbox
            freeEventsCheckBox = new CheckBox
            {
                Text = "Free Events Only",
                Location = new Point(160, 300),
                Size = new Size(200, 20)
            };

            // Search button
            searchButton = new Button
            {
                Text = "Search",
                Location = new Point(160, 340),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            searchButton.Click += SearchButton_Click;

            // Results grid
            resultsGridView = new DataGridView
            {
                Location = new Point(50, 390),
                Size = new Size(this.ClientSize.Width - 100, this.ClientSize.Height - 450),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            resultsGridView.Columns.Add("EventName", "Event Name");
            resultsGridView.Columns.Add("Date", "Date");
            resultsGridView.Columns.Add("Location", "Location");
            resultsGridView.Columns.Add("Category", "Category");
            resultsGridView.Columns.Add("Price", "Price");

            // Add controls to the form
            this.Controls.Add(keywordLabel);
            this.Controls.Add(keywordTextBox);
            this.Controls.Add(categoryLabel);
            this.Controls.Add(categoryComboBox);
            this.Controls.Add(dateRangeLabel);
            this.Controls.Add(startDatePicker);
            this.Controls.Add(toLabel);
            this.Controls.Add(endDatePicker);
            this.Controls.Add(locationLabel);
            this.Controls.Add(locationTextBox);
            this.Controls.Add(priceRangeLabel);
            this.Controls.Add(priceRangeComboBox);
            this.Controls.Add(freeEventsCheckBox);
            this.Controls.Add(searchButton);
            this.Controls.Add(resultsGridView);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            // Clear existing results
            resultsGridView.Rows.Clear();

            // Get sample data
            var allEvents = GetSampleEventData();

            // Apply filters
            var filteredEvents = allEvents.AsEnumerable().Where(row =>
                (string.IsNullOrEmpty(keywordTextBox.Text) || row.Field<string>("Event Name").IndexOf(keywordTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (categoryComboBox.SelectedIndex == 0 || row.Field<string>("Category") == categoryComboBox.SelectedItem.ToString()) &&
                (row.Field<DateTime>("Date") >= startDatePicker.Value.Date && row.Field<DateTime>("Date") <= endDatePicker.Value.Date) &&
                (string.IsNullOrEmpty(locationTextBox.Text) || row.Field<string>("Location").IndexOf(locationTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (!freeEventsCheckBox.Checked || row.Field<decimal>("Price") == 0)
            );

            // Apply price range filter
            if (priceRangeComboBox.SelectedIndex > 0)
            {
                decimal minPrice = 0, maxPrice = decimal.MaxValue;
                switch (priceRangeComboBox.SelectedIndex)
                {
                    case 1: maxPrice = 25; break;
                    case 2: minPrice = 25; maxPrice = 50; break;
                    case 3: minPrice = 50; maxPrice = 100; break;
                    case 4: minPrice = 100; maxPrice = 250; break;
                    case 5: minPrice = 250; break;
                }
                filteredEvents = filteredEvents.Where(row =>
                    row.Field<decimal>("Price") >= minPrice && row.Field<decimal>("Price") < maxPrice
                );
            }

            // Check if there are any results
            if (filteredEvents.Any())
            {
                // Display filtered results
                foreach (var row in filteredEvents)
                {
                    resultsGridView.Rows.Add(
                        row.Field<string>("Event Name"),
                        row.Field<DateTime>("Date").ToShortDateString(),
                        row.Field<string>("Location"),
                        row.Field<string>("Category"),
                        row.Field<decimal>("Price").ToString("C")
                    );
                }
            }
            else
            {
                MessageBox.Show("No events found matching your search criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DataTable GetSampleEventData()
        {
            var table = new DataTable();
            table.Columns.Add("Event Name", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Price", typeof(decimal));

            table.Rows.Add("Tech Conference 2024", new DateTime(2024, 6, 15), "New York", "Conference", 199.99m);
            table.Rows.Add("Rock Concert", new DateTime(2024, 7, 20), "Los Angeles", "Concert", 89.99m);
            table.Rows.Add("Business Workshop", new DateTime(2024, 8, 5), "Chicago", "Workshop", 149.99m);
            table.Rows.Add("Local Art Exhibition", new DateTime(2024, 5, 10), "San Francisco", "Art Exhibition", 0m);
            table.Rows.Add("Startup Networking Event", new DateTime(2024, 9, 1), "Boston", "Networking", 25m);

            return table;
        }
    }
}

