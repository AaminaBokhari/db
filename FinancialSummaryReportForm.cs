using System;
using System.Data;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class FinancialSummaryReportForm : BaseReportForm
    {
        public FinancialSummaryReportForm()
        {
            InitializeComponents();
            SetTitle("Financial Summary Report");
        }

        private void InitializeComponents()
        {
            // No additional components needed for this report
        }

        protected override void GenerateReportButton_Click(object sender, EventArgs e)
        {
            // In a real application, you would fetch this data from a database
            DataTable reportData = new DataTable();
            reportData.Columns.Add("Category", typeof(string));
            reportData.Columns.Add("Amount", typeof(string));

            reportData.Rows.Add("Total Revenue", "$500,000");
            reportData.Rows.Add("Total Expenses", "$300,000");
            reportData.Rows.Add("Net Profit", "$200,000");
            reportData.Rows.Add("Ticket Sales", "$400,000");
            reportData.Rows.Add("Sponsorship Revenue", "$100,000");
            reportData.Rows.Add("Vendor Fees", "$50,000");

            reportDataGridView.DataSource = reportData;

            MessageBox.Show("Financial Summary Report generated successfully!");
        }
    }
}