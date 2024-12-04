using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class FeedbackForm : BaseForm
    {
        private ComboBox eventComboBox;
        private RichTextBox feedbackTextBox;
        private TrackBar ratingTrackBar;
        private Label ratingLabel;
        private Button submitButton;

        public FeedbackForm(string selectedEvent = null)
        {
            InitializeComponents();
            SetTitle("Event Feedback");
            if (!string.IsNullOrEmpty(selectedEvent))
            {
                eventComboBox.SelectedItem = selectedEvent;
                eventComboBox.Enabled = false; // Lock the selection if event was passed
            }
        }

        private void InitializeComponents()
        {
            Label eventLabel = new Label
            {
                Text = "Select Event:",
                Font = new Font("Arial", 12),
                Location = new Point(50, 50),
                Size = new Size(120, 30)
            };

            eventComboBox = new ComboBox
            {
                Font = new Font("Arial", 12),
                Location = new Point(180, 50),
                Size = new Size(300, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            eventComboBox.Items.AddRange(new object[] { "Tech Conference 2024", "Music Festival", "Business Workshop" });

            Label feedbackLabel = new Label
            {
                Text = "Your Feedback:",
                Font = new Font("Arial", 12),
                Location = new Point(50, 100),
                Size = new Size(120, 30)
            };

            feedbackTextBox = new RichTextBox
            {
                Font = new Font("Arial", 12),
                Location = new Point(50, 130),
                Size = new Size(430, 150)
            };

            Label ratingHeaderLabel = new Label
            {
                Text = "Rating:",
                Font = new Font("Arial", 12),
                Location = new Point(50, 300),
                Size = new Size(120, 30)
            };

            ratingTrackBar = new TrackBar
            {
                Location = new Point(50, 330),
                Size = new Size(300, 45),
                Minimum = 1,
                Maximum = 5,
                Value = 3
            };
            ratingTrackBar.ValueChanged += RatingTrackBar_ValueChanged;

            ratingLabel = new Label
            {
                Text = "3 / 5",
                Font = new Font("Arial", 12),
                Location = new Point(360, 330),
                Size = new Size(60, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            submitButton = new Button
            {
                Text = "Submit Feedback",
                Font = new Font("Arial", 14),
                Size = new Size(200, 50),
                Location = new Point(this.ClientSize.Width / 2 - 100, 400),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            submitButton.Click += SubmitButton_Click;

            this.Controls.Add(eventLabel);
            this.Controls.Add(eventComboBox);
            this.Controls.Add(feedbackLabel);
            this.Controls.Add(feedbackTextBox);
            this.Controls.Add(ratingHeaderLabel);
            this.Controls.Add(ratingTrackBar);
            this.Controls.Add(ratingLabel);
            this.Controls.Add(submitButton);
        }

        private void RatingTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ratingLabel.Text = $"{ratingTrackBar.Value} / 5";
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an event.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(feedbackTextBox.Text))
            {
                MessageBox.Show("Please provide your feedback.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Here you would typically save the feedback to your database
            MessageBox.Show($"Thank you for your feedback for {eventComboBox.SelectedItem}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}

