using System;
using System.Data;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class FeedbackAnalysisReportForm : BaseReportForm
    {
        public FeedbackAnalysisReportForm()
        {
            InitializeComponents();
            SetTitle("Feedback Analysis Report");
        }

        private void InitializeComponents()
        {
            // No additional components needed for this report
        }

        protected override void GenerateReportButton_Click(object sender, EventArgs e)
        {
            DataTable reportData = new DataTable();
            reportData.Columns.Add("Category", typeof(string));
            reportData.Columns.Add("Average Rating", typeof(double));
            reportData.Columns.Add("Total Responses", typeof(int));

            reportData.Rows.Add("Overall Satisfaction", 4.5, 1000);
            reportData.Rows.Add("Event Organization", 4.3, 950);
            reportData.Rows.Add("Venue", 4.2, 980);
            reportData.Rows.Add("Content/Programming", 4.7, 900);
            reportData.Rows.Add("Staff Helpfulness", 4.6, 850);
            reportData.Rows.Add("Value for Money", 4.1, 920);

            reportDataGridView.DataSource = reportData;

            MessageBox.Show("Feedback Analysis Report generated successfully!");
        }
    }
}