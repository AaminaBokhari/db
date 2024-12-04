using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class VendorDashboardForm : BaseForm
    {
        private Label welcomeLabel;
        private Button viewEventsButton;
        private Button manageBidsButton;
        private Button profileButton;
        private Button resourceCatalogButton;
        private Button searchEventsButton;
        private Button contractManagementButton;
        private Button paymentTrackingButton;

        public VendorDashboardForm()
        {
            InitializeComponents();
            SetTitle("Vendor Dashboard");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            int centerX = this.ClientSize.Width / 2;

            welcomeLabel = new Label
            {
               
                Font = new Font("Arial", 24, FontStyle.Bold),
                Size = new Size(300, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            welcomeLabel.Location = new Point(centerX - welcomeLabel.Width / 2, 50);

            viewEventsButton = CreateDashboardButton("View Events", 0);
            manageBidsButton = CreateDashboardButton("Manage Bids", 1);
            profileButton = CreateDashboardButton("Profile", 2);
            resourceCatalogButton = CreateDashboardButton("Resource Catalog", 3);
            searchEventsButton = CreateDashboardButton("Search Events", 4);
            contractManagementButton = CreateDashboardButton("Contract Management", 5);
            paymentTrackingButton = CreateDashboardButton("Payment Tracking", 6);

            this.Controls.AddRange(new Control[] {
                welcomeLabel, viewEventsButton, manageBidsButton, profileButton,
                resourceCatalogButton, searchEventsButton, contractManagementButton, paymentTrackingButton
            });
        }

        private Button CreateDashboardButton(string text, int index)
        {
            Button button = new Button
            {
                Text = text,
                Font = new Font("Arial", 14),
                Size = new Size(250, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };

            int centerX = this.ClientSize.Width / 2;
            int startY = 120;
            int spacing = 70;

            button.Location = new Point(centerX - button.Width / 2, startY + index * spacing);
            button.Click += DashboardButton_Click;
            return button;
        }

        private void DashboardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Form formToShow = null;

                switch (clickedButton.Text)
                {
                    case "View Events":
                        formToShow = new ViewEventsForm();
                        break;
                    case "Manage Bids":
                        formToShow = new BiddingForm();
                        break;
                    case "Profile":
                        formToShow = new VendorProfileForm();
                        break;
                    case "Resource Catalog":
                        formToShow = new ResourceCatalogueForm();
                        break;
                    case "Search Events":
                        formToShow = new EventSearchForm();
                        break;
                    case "Contract Management":
                        formToShow = new ContractManagementForm();
                        break;
                    case "Payment Tracking":
                        formToShow = new PaymentOverviewForm();
                        break;
                }

                if (formToShow != null)
                {
                    formToShow.ShowDialog();
                }
            }
        }
    }
}

