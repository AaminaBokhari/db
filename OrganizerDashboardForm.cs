using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class OrganizerDashboardForm : BaseForm
    {
        private Label welcomeLabel;
        private DataGridView eventsGridView;
        private Button createEventButton;
        private Button manageTicketsButton;
        private Button manageAttendeesButton;
        private Button manageResourcesButton;
        private Button eventAnalyticsButton;
        private Button managePaymentsButton;
        private Button searchEventsButton;
        private Button manageProfileButton;
        private Button eventQueriesButton;
        private Button vendorManagementButton;
        private Button attendeeFeedbackButton;

        public OrganizerDashboardForm()
        {
            InitializeComponents();
            SetTitle("Organizer Dashboard");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(800, 600);

            welcomeLabel = new Label
            {
                Text = "Welcome, Organizer!",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(50, 50),
                Size = new Size(300, 40)
            };

            eventsGridView = new DataGridView
            {
                Location = new Point(50, 100),
                Size = new Size(700, 200),
                Font = new Font("Arial", 12),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            eventsGridView.Columns.Add("EventName", "Event Name");
            eventsGridView.Columns.Add("Date", "Date");
            eventsGridView.Columns.Add("Location", "Location");
            eventsGridView.Columns.Add("Status", "Status");
            LoadEvents();

            int buttonWidth = 150;
            int buttonHeight = 40;
            int buttonSpacing = 20;
            int startX = 50;
            int startY = 320;

            createEventButton = CreateDashboardButton("Create Event", startX, startY);
            manageTicketsButton = CreateDashboardButton("Manage Tickets", startX + buttonWidth + buttonSpacing, startY);
            manageAttendeesButton = CreateDashboardButton("Manage Attendees", startX + (buttonWidth + buttonSpacing) * 2, startY);
            manageResourcesButton = CreateDashboardButton("Manage Resources", startX + (buttonWidth + buttonSpacing) * 3, startY);

            eventAnalyticsButton = CreateDashboardButton("Event Analytics", startX, startY + buttonHeight + buttonSpacing);
            managePaymentsButton = CreateDashboardButton("Manage Payments", startX + buttonWidth + buttonSpacing, startY + buttonHeight + buttonSpacing);
            searchEventsButton = CreateDashboardButton("Search Events", startX + (buttonWidth + buttonSpacing) * 2, startY + buttonHeight + buttonSpacing);
            manageProfileButton = CreateDashboardButton("Manage Profile", startX + (buttonWidth + buttonSpacing) * 3, startY + buttonHeight + buttonSpacing);

            eventQueriesButton = CreateDashboardButton("Event Queries", startX, startY + (buttonHeight + buttonSpacing) * 2);
            vendorManagementButton = CreateDashboardButton("Vendor Management", startX + buttonWidth + buttonSpacing, startY + (buttonHeight + buttonSpacing) * 2);
            attendeeFeedbackButton = CreateDashboardButton("Attendee Feedback", startX + (buttonWidth + buttonSpacing) * 2, startY + (buttonHeight + buttonSpacing) * 2);

            this.Controls.AddRange(new Control[]
            {
                welcomeLabel, eventsGridView, createEventButton, manageTicketsButton, manageAttendeesButton,
                manageResourcesButton, eventAnalyticsButton, managePaymentsButton, searchEventsButton,
                manageProfileButton, eventQueriesButton, vendorManagementButton, attendeeFeedbackButton
            });
        }

        private Button CreateDashboardButton(string text, int x, int y)
        {
            Button button = new Button
            {
                Text = text,
                Font = new Font("Arial", 12),
                Size = new Size(150, 40),
                Location = new Point(x, y),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            button.Click += DashboardButton_Click;
            return button;
        }

        private void LoadEvents()
        {
            // This is where you would typically load data from a database
            // For now, we'll just add some sample data
            eventsGridView.Rows.Add("Tech Conference 2024", "2024-06-15", "New York", "Upcoming");
            eventsGridView.Rows.Add("Music Festival", "2024-07-20", "Los Angeles", "Planning");
            eventsGridView.Rows.Add("Business Workshop", "2024-08-05", "Chicago", "Tickets on Sale");
        }

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                switch (clickedButton.Text)
                {
                    case "Create Event":
                        new EventCreationForm().ShowDialog();
                        break;
                    case "Manage Tickets":
                        new TicketingManagementForm().ShowDialog();
                        break;
                    case "Manage Attendees":
                        new AttendeeManagementForm().ShowDialog();
                        break;
                    case "Manage Resources":
                        new ResourceManagementForm().ShowDialog();
                        break;
                    case "Event Analytics":
                        new AnalyticsDashboardForm().ShowDialog();
                        break;
                    case "Manage Payments":
                        new PaymentManagementForm().ShowDialog();
                        break;
                    case "Search Events":
                        new EventSearchForm().ShowDialog();
                        break;
                    case "Manage Profile":
                        new OrganizerProfileForm().ShowDialog();
                        break;
                    case "Event Queries":
                        new EventQueriesForm().ShowDialog();
                        break;
                    case "Vendor Management":
                        new VendorManagementForm().ShowDialog();
                        break;
                    case "Attendee Feedback":
                        new FeedbackForm().ShowDialog();
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OrganizerDashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "OrganizerDashboardForm";
            this.ResumeLayout(false);
        }
    }
}