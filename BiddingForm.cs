using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class BiddingForm : BaseForm
    {
        private DataGridView bidsGridView;
        private Button placeBidButton;
        private Button editBidButton;
        private Button withdrawBidButton;
        private Button viewDetailsButton;
        private ComboBox filterStatusComboBox;

        public BiddingForm()
        {
            InitializeComponents();
            SetTitle("Manage Bids");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Manage Bids",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Location = new Point(50, 30),
                Size = new Size(800, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            bidsGridView = new DataGridView
            {
                Location = new Point(50, 120),
                Size = new Size(800, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            bidsGridView.Columns.Add("EventName", "Event Name");
            bidsGridView.Columns.Add("BidAmount", "Bid Amount");
            bidsGridView.Columns.Add("Status", "Status");
            bidsGridView.Columns.Add("BidType", "Bid Type");
            bidsGridView.Columns.Add("SubmissionDate", "Submission Date");

            Label filterLabel = new Label
            {
                Text = "Filter by Status:",
                Location = new Point(50, 80),
                Size = new Size(120, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            filterStatusComboBox = new ComboBox
            {
                Location = new Point(180, 80),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            filterStatusComboBox.Items.AddRange(new object[] { "All", "Pending", "Accepted", "Rejected" });
            filterStatusComboBox.SelectedIndex = 0;
            filterStatusComboBox.SelectedIndexChanged += FilterStatusComboBox_SelectedIndexChanged;

            placeBidButton = new Button
            {
                Text = "Place New Bid",
                Location = new Point(50, 540),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            placeBidButton.Click += PlaceBidButton_Click;

            editBidButton = new Button
            {
                Text = "Edit Bid",
                Location = new Point(220, 540),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            editBidButton.Click += EditBidButton_Click;

            withdrawBidButton = new Button
            {
                Text = "Withdraw Bid",
                Location = new Point(390, 540),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(204, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            withdrawBidButton.Click += WithdrawBidButton_Click;

            viewDetailsButton = new Button
            {
                Text = "View Bid Details",
                Location = new Point(560, 540),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            viewDetailsButton.Click += ViewDetailsButton_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(filterLabel);
            this.Controls.Add(filterStatusComboBox);
            this.Controls.Add(bidsGridView);
            this.Controls.Add(placeBidButton);
            this.Controls.Add(editBidButton);
            this.Controls.Add(withdrawBidButton);
            this.Controls.Add(viewDetailsButton);

            LoadBids();
        }

        private void LoadBids(string statusFilter = "All")
        {
            bidsGridView.Rows.Clear();
            // In a real application, this would fetch data from a database
            var bids = new[]
            {
                new { Event = "Tech Conference 2024", Amount = 5000m, Status = "Pending", Type = "Service", Date = DateTime.Now.AddDays(-5) },
                new { Event = "Music Festival", Amount = 10000m, Status = "Accepted", Type = "Sponsorship", Date = DateTime.Now.AddDays(-10) },
                new { Event = "Business Workshop", Amount = 3000m, Status = "Rejected", Type = "Service", Date = DateTime.Now.AddDays(-15) },
                new { Event = "Sports Event", Amount = 7500m, Status = "Pending", Type = "Sponsorship", Date = DateTime.Now.AddDays(-2) }
            };

            foreach (var bid in bids)
            {
                if (statusFilter == "All" || bid.Status == statusFilter)
                {
                    bidsGridView.Rows.Add(bid.Event, $"${bid.Amount}", bid.Status, bid.Type, bid.Date.ToShortDateString());
                }
            }
        }

        private void FilterStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBids(filterStatusComboBox.SelectedItem.ToString());
        }

        private void PlaceBidButton_Click(object sender, EventArgs e)
        {
            using (var placeBidForm = new PlaceBidForm())
            {
                if (placeBidForm.ShowDialog() == DialogResult.OK)
                {
                    bidsGridView.Rows.Add(placeBidForm.SelectedEvent, $"${placeBidForm.BidAmount}", "Pending", placeBidForm.BidType, DateTime.Now.ToShortDateString());
                    MessageBox.Show("Bid placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void EditBidButton_Click(object sender, EventArgs e)
        {
            if (bidsGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = bidsGridView.SelectedRows[0];
                string eventName = selectedRow.Cells["EventName"].Value.ToString();
                decimal bidAmount = decimal.Parse(selectedRow.Cells["BidAmount"].Value.ToString().TrimStart('$'));
                string bidType = selectedRow.Cells["BidType"].Value.ToString();

                using (var editBidForm = new PlaceBidForm(eventName, bidAmount, bidType))
                {
                    if (editBidForm.ShowDialog() == DialogResult.OK)
                    {
                        selectedRow.Cells["EventName"].Value = editBidForm.SelectedEvent;
                        selectedRow.Cells["BidAmount"].Value = $"${editBidForm.BidAmount}";
                        selectedRow.Cells["BidType"].Value = editBidForm.BidType;
                        selectedRow.Cells["Status"].Value = "Pending";
                        selectedRow.Cells["SubmissionDate"].Value = DateTime.Now.ToShortDateString();
                        MessageBox.Show("Bid updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a bid to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void WithdrawBidButton_Click(object sender, EventArgs e)
        {
            if (bidsGridView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to withdraw this bid?", "Confirm Withdrawal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bidsGridView.Rows.RemoveAt(bidsGridView.SelectedRows[0].Index);
                    MessageBox.Show("Bid withdrawn successfully.", "Withdraw Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a bid to withdraw.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ViewDetailsButton_Click(object sender, EventArgs e)
        {
            if (bidsGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = bidsGridView.SelectedRows[0];
                string details = $"Event: {selectedRow.Cells["EventName"].Value}\n" +
                                 $"Bid Amount: {selectedRow.Cells["BidAmount"].Value}\n" +
                                 $"Status: {selectedRow.Cells["Status"].Value}\n" +
                                 $"Bid Type: {selectedRow.Cells["BidType"].Value}\n" +
                                 $"Submission Date: {selectedRow.Cells["SubmissionDate"].Value}";

                MessageBox.Show(details, "Bid Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a bid to view details.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}