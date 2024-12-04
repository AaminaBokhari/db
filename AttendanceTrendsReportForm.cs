using System;
using System.Data;
using System.Windows.Forms;

namespace EventVerse
{
    public class AttendanceTrendsReportForm : BaseReportForm
    {
        public AttendanceTrendsReportForm()
        {
            InitializeComponents();
            SetTitle("Attendance Trends Report");
        }

        private void InitializeComponents()
        {
            // No additional components needed for this report
        }

        protected override void GenerateReportButton_Click(object sender, EventArgs e)
        {
            DataTable reportData = new DataTable();
            reportData.Columns.Add("Month", typeof(string));
            reportData.Columns.Add("Total Attendees", typeof(int));
            reportData.Columns.Add("Year-over-Year Growth", typeof(string));

            reportData.Rows.Add("January", 5000, "+10%");
            reportData.Rows.Add("February", 5500, "+15%");
            reportData.Rows.Add("March", 6000, "+20%");
            reportData.Rows.Add("April", 5800, "+18%");
            reportData.Rows.Add("May", 6200, "+22%");
            reportData.Rows.Add("June", 6500, "+25%");

            reportDataGridView.DataSource = reportData;

            MessageBox.Show("Attendance Trends Report generated successfully!");
        }
    }
}