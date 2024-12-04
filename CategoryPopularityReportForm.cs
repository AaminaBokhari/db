using System;
using System.Data;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class CategoryPopularityReportForm : BaseReportForm
    {
        public CategoryPopularityReportForm()
        {
            InitializeComponents();
            SetTitle("Category Popularity Report");
        }

        private void InitializeComponents()
        {
            // No additional components needed for this report
        }

        protected override void GenerateReportButton_Click(object sender, EventArgs e)
        {
            DataTable reportData = new DataTable();
            reportData.Columns.Add("Category", typeof(string));
            reportData.Columns.Add("Total Events", typeof(int));
            reportData.Columns.Add("Total Attendees", typeof(int));
            reportData.Columns.Add("Average Rating", typeof(double));

            reportData.Rows.Add("Technology", 50, 15000, 4.7);
            reportData.Rows.Add("Business", 40, 12000, 4.5);
            reportData.Rows.Add("Music", 30, 20000, 4.8);
            reportData.Rows.Add("Art", 25, 8000, 4.6);
            reportData.Rows.Add("Sports", 35, 18000, 4.9);
            reportData.Rows.Add("Food & Drink", 20, 10000, 4.7);

            reportDataGridView.DataSource = reportData;

            MessageBox.Show("Category Popularity Report generated successfully!");
        }
    }
}