using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class FeedbackModerationForm : BaseForm
    {
        private DataGridView feedbackGridView;
        private Button approveButton;
        private Button rejectButton;
        private RichTextBox feedbackContentTextBox;
        private Label titleLabel;

        public FeedbackModerationForm()
        {
            InitializeComponents();
            SetTitle("Feedback Moderation");
        }

        private void InitializeComponents()
        {
            // Add a title label at the top
            titleLabel = new Label
            {
                Text = "Feedback Moderation",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(50, 30),
                Size = new Size(700, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Move the grid down by adjusting Y coordinate
            feedbackGridView = new DataGridView
            {
                Location = new Point(50, 80), // Changed from 50 to 80
                Size = new Size(700, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.Fixed3D,
                RowHeadersWidth = 51,
                Font = new Font("Arial", 10)
            };
            feedbackGridView.Columns.Add("FeedbackId", "Feedback ID");
            feedbackGridView.Columns.Add("EventName", "Event Name");
            feedbackGridView.Columns.Add("User", "User");
            feedbackGridView.Columns.Add("Rating", "Rating");
            feedbackGridView.Columns.Add("Status", "Status");
            feedbackGridView.SelectionChanged += FeedbackGridView_SelectionChanged;

            // Adjust other controls' positions accordingly
            approveButton = new Button
            {
                Text = "Approve Feedback",
                Location = new Point(50, 300), // Changed from 270 to 300
                Size = new Size(150, 30),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            approveButton.Click += ApproveButton_Click;

            rejectButton = new Button
            {
                Text = "Reject Feedback",
                Location = new Point(220, 300), // Changed from 270 to 300
                Size = new Size(150, 30),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            rejectButton.Click += RejectButton_Click;

            feedbackContentTextBox = new RichTextBox
            {
                Location = new Point(50, 350), // Changed from 320 to 350
                Size = new Size(700, 150),
                ReadOnly = true,
                Font = new Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Set form size
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Add controls
            this.Controls.Add(titleLabel);
            this.Controls.Add(feedbackGridView);
            this.Controls.Add(approveButton);
            this.Controls.Add(rejectButton);
            this.Controls.Add(feedbackContentTextBox);

            // Populate with sample data
            feedbackGridView.Rows.Add("1", "Tech Conference 2024", "john_doe", "4", "Pending");
            feedbackGridView.Rows.Add("2", "Music Festival", "jane_smith", "5", "Pending");
            feedbackGridView.Rows.Add("3", "Business Workshop", "bob_user", "3", "Pending");
        }

        private void FeedbackGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (feedbackGridView.SelectedRows.Count > 0)
            {
                string feedbackContent = "Sample feedback content for " +
                    feedbackGridView.SelectedRows[0].Cells["EventName"].Value +
                    " by " + feedbackGridView.SelectedRows[0].Cells["User"].Value +
                    ":\n\nThe event was well organized and informative. I particularly enjoyed the networking opportunities and the keynote speakers. However, the venue could have been more spacious to accommodate all attendees comfortably.";

                feedbackContentTextBox.Text = feedbackContent;
            }
        }

        private void ApproveButton_Click(object sender, EventArgs e)
        {
            if (feedbackGridView.SelectedRows.Count > 0)
            {
                feedbackGridView.SelectedRows[0].Cells["Status"].Value = "Approved";
                MessageBox.Show("Feedback approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a feedback to approve.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            if (feedbackGridView.SelectedRows.Count > 0)
            {
                feedbackGridView.SelectedRows[0].Cells["Status"].Value = "Rejected";
                MessageBox.Show("Feedback rejected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a feedback to reject.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}