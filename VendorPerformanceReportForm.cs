using System;
using System.Data;
using System.Windows.Forms;

namespace EventVerse
{
    public class VendorPerformanceReportForm : BaseReportForm
    {
        private ComboBox vendorComboBox;

        public VendorPerformanceReportForm()
        {
            InitializeComponents();
            SetTitle("Vendor Performance Report");
        }

        private void InitializeComponents()
        {
            Label vendorLabel = new Label
            {
                Text = "Select Vendor:",
                Location = new System.Drawing.Point(50, 20),
                Size = new System.Drawing.Size(100, 20)
            };
            this.Controls.Add(vendorLabel);

            vendorComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(160, 20),
                Size = new System.Drawing.Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            vendorComboBox.Items.AddRange(new object[] { "Catering Co.", "Sound Systems Inc.", "Decor Masters" });
            this.Controls.Add(vendorComboBox);
        }

        protected override void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (vendorComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a vendor.");
                return;
            }

            DataTable reportData = new DataTable();
            reportData.Columns.Add("Metric", typeof(string));
            reportData.Columns.Add("Value", typeof(string));

            reportData.Rows.Add("Total Events Serviced", "25");
            reportData.Rows.Add("Average Rating", "4.7/5");
            reportData.Rows.Add("On-Time Delivery Rate", "98%");
            reportData.Rows.Add("Total Revenue Generated", "$150,000");
            reportData.Rows.Add("Customer Satisfaction Score", "92%");
            reportData.Rows.Add("Repeat Business Rate", "75%");

            reportDataGridView.DataSource = reportData;

            MessageBox.Show("Vendor Performance Report generated successfully!");
        }
    }
}