using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

namespace EventVerse
{
    public class AnalyticsDashboardForm : BaseForm
    {
        private ComboBox eventComboBox;
        private LiveCharts.WinForms.CartesianChart ticketSalesChart;
        private LiveCharts.WinForms.PieChart attendeeEngagementChart;
        private Label totalRevenueLabel;
        private Label totalAttendeesLabel;

        public AnalyticsDashboardForm()
        {
            InitializeComponents();
            SetTitle("Analytics Dashboard");
            this.Size = new Size(950, 600); // Set appropriate form size
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponents()
        {
            // Title Label
            Label titleLabel = new Label
            {
                Text = "Event Analytics Dashboard",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(50, 50),
                Size = new Size(300, 30)
            };

            Label eventLabel = new Label
            {
                Text = "Select Event:",
                Font = new Font("Arial", 12),
                Location = new Point(50, 100),
                Size = new Size(120, 30)
            };

            eventComboBox = new ComboBox
            {
                Font = new Font("Arial", 12),
                Location = new Point(180, 100),
                Size = new Size(300, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            eventComboBox.Items.AddRange(new object[] { "Tech Conference 2024", "Music Festival", "Business Workshop" });
            eventComboBox.SelectedIndex = 0; // Set default selection
            eventComboBox.SelectedIndexChanged += EventComboBox_SelectedIndexChanged;

            // Charts Title Labels
            Label salesChartLabel = new Label
            {
                Text = "Ticket Sales Trend",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(50, 150),
                Size = new Size(200, 30)
            };

            Label engagementChartLabel = new Label
            {
                Text = "Attendee Engagement",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(500, 150),
                Size = new Size(200, 30)
            };

            ticketSalesChart = new LiveCharts.WinForms.CartesianChart
            {
                Location = new Point(50, 180),
                Size = new Size(400, 300),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
            };

            attendeeEngagementChart = new LiveCharts.WinForms.PieChart
            {
                Location = new Point(500, 180),
                Size = new Size(400, 300),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };

            totalRevenueLabel = new Label
            {
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(50, 500),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            totalAttendeesLabel = new Label
            {
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(500, 500),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Add controls in order
            this.Controls.Add(titleLabel);
            this.Controls.Add(eventLabel);
            this.Controls.Add(eventComboBox);
            this.Controls.Add(salesChartLabel);
            this.Controls.Add(engagementChartLabel);
            this.Controls.Add(ticketSalesChart);
            this.Controls.Add(attendeeEngagementChart);
            this.Controls.Add(totalRevenueLabel);
            this.Controls.Add(totalAttendeesLabel);

            // Set up chart styles
            ConfigureChartStyles();

            // Load initial data
            LoadAnalyticsData();
        }

        private void ConfigureChartStyles()
        {
            // Configure Ticket Sales Chart
            ticketSalesChart.BackColor = Color.White;
            ticketSalesChart.DataTooltip = new DefaultTooltip { SelectionMode = TooltipSelectionMode.SharedYValues };

            // Configure Attendee Engagement Chart
            attendeeEngagementChart.BackColor = Color.White;
            attendeeEngagementChart.LegendLocation = LegendLocation.Right;
        }
        private void EventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAnalyticsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading analytics data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAnalyticsData()
        {
            if (eventComboBox.SelectedItem == null)
                return;

            string selectedEvent = eventComboBox.SelectedItem.ToString();

            double[] salesData;
            switch (selectedEvent)
            {
                case "Music Festival":
                    salesData = new double[] { 200, 450, 700, 1000 };
                    break;
                case "Business Workshop":
                    salesData = new double[] { 50, 100, 150, 200 };
                    break;
                default:
                    salesData = new double[] { 100, 250, 400, 600 };
                    break;
            }

            // Update Ticket Sales Chart
            ticketSalesChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(salesData),
                    Title = "Ticket Sales",
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    LineSmoothness = 0
                }
            };

            ticketSalesChart.AxisX.Clear();
            ticketSalesChart.AxisX.Add(new Axis
            {
                Title = "Week",
                Labels = new[] { "Week 1", "Week 2", "Week 3", "Week 4" },
                Separator = new Separator { Step = 1 }
            });

            ticketSalesChart.AxisY.Clear();
            ticketSalesChart.AxisY.Add(new Axis
            {
                Title = "Number of Tickets",
                LabelFormatter = value => value.ToString("N0"),
                Separator = new Separator { Step = 200 }
            });

            // Update Engagement Chart
            double[] engagementData = { 60, 30, 10 };
            var colors = new[] { "#28a745", "#ffc107", "#dc3545" }; // Green, Yellow, Red
            attendeeEngagementChart.Series = new SeriesCollection();

            for (int i = 0; i < engagementData.Length; i++)
            {
                attendeeEngagementChart.Series.Add(new PieSeries
                {
                    Values = new ChartValues<double> { engagementData[i] },
                    Title = i == 0 ? "High" : i == 1 ? "Medium" : "Low",
                    DataLabels = true,
                    LabelPoint = point => $"{point.Y}%",
                    Fill = new System.Windows.Media.SolidColorBrush(
                        (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colors[i]))
                });
            }

            // Update summary labels
            int totalRevenue = (int)salesData.Sum();
            totalRevenueLabel.Text = $"Total Revenue: ${totalRevenue * 100:N0}";
            totalAttendeesLabel.Text = $"Total Attendees: {totalRevenue:N0}";
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Update chart sizes on form resize
            if (ticketSalesChart != null && attendeeEngagementChart != null)
            {
                ticketSalesChart.Size = new Size((this.ClientSize.Width / 2) - 75, 300);
                attendeeEngagementChart.Location = new Point(this.ClientSize.Width / 2 + 25, attendeeEngagementChart.Location.Y);
                attendeeEngagementChart.Size = new Size((this.ClientSize.Width / 2) - 75, 300);
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoBox
            // 
            this.logoBox.Click += new System.EventHandler(this.logoBox_Click);
            // 
            // AnalyticsDashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "AnalyticsDashboardForm";
            this.Load += new System.EventHandler(this.AnalyticsDashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);

        }

        private void logoBox_Click(object sender, EventArgs e)
        {

        }

        private void AnalyticsDashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}