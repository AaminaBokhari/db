using System;
using System.Data;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class EventPerformanceReportForm : BaseReportForm
    {
        private ComboBox eventComboBox;

        public EventPerformanceReportForm()
        {
            InitializeComponents();
            SetTitle("Event Performance Report");
        }

        private void InitializeComponents()
        {
            Label eventLabel = new Label
            {
                Text = "Select Event:",
                Location = new System.Drawing.Point(50, 20),
                Size = new System.Drawing.Size(100, 20)
            };
            this.Controls.Add(eventLabel);

            eventComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(160, 20),
                Size = new System.Drawing.Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            eventComboBox.Items.AddRange(new object[] { "Tech Conference 2024", "Music Festival", "Business Workshop" });
            this.Controls.Add(eventComboBox);
        }

        protected override void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an event.");
                return;
            }

            // In a real application, you would fetch this data from a database
            // Here, we're using mock data for demonstration purposes
            DataTable reportData = new DataTable();
            reportData.Columns.Add("Metric", typeof(string));
            reportData.Columns.Add("Value", typeof(string));

            reportData.Rows.Add("Total Attendees", "500");
            reportData.Rows.Add("Ticket Sales", "$25,000");
            reportData.Rows.Add("Average Rating", "4.5/5");
            reportData.Rows.Add("Total Revenue", "$30,000");
            reportData.Rows.Add("Total Expenses", "$20,000");
            reportData.Rows.Add("Net Profit", "$10,000");

            reportDataGridView.DataSource = reportData;

            MessageBox.Show("Report generated successfully!");
        }
    }
}