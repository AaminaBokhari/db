using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public abstract partial class BaseReportForm : BaseForm
    {
        protected DataGridView reportDataGridView;
        protected Button generateReportButton;
        protected DateTimePicker startDatePicker;
        protected DateTimePicker endDatePicker;

        public BaseReportForm()
        {
            InitializeBaseComponents();
        }

        private void InitializeBaseComponents()
        {
            this.Size = new Size(800, 600);

            Label startDateLabel = new Label
            {
                Text = "Start Date:",
                Location = new Point(50, 50),
                Size = new Size(100, 20)
            };
            this.Controls.Add(startDateLabel);

            startDatePicker = new DateTimePicker
            {
                Location = new Point(160, 50),
                Size = new Size(200, 20)
            };
            this.Controls.Add(startDatePicker);

            Label endDateLabel = new Label
            {
                Text = "End Date:",
                Location = new Point(400, 50),
                Size = new Size(100, 20)
            };
            this.Controls.Add(endDateLabel);

            endDatePicker = new DateTimePicker
            {
                Location = new Point(510, 50),
                Size = new Size(200, 20)
            };
            this.Controls.Add(endDatePicker);

            generateReportButton = new Button
            {
                Text = "Generate Report",
                Location = new Point(50, 100),
                Size = new Size(150, 30),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            generateReportButton.Click += GenerateReportButton_Click;
            this.Controls.Add(generateReportButton);

            reportDataGridView = new DataGridView
            {
                Location = new Point(50, 150),
                Size = new Size(700, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            this.Controls.Add(reportDataGridView);
        }

        protected abstract void GenerateReportButton_Click(object sender, EventArgs e);
    }
}