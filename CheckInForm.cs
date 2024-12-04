using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class CheckInForm : BaseForm
    {
        private ComboBox eventComboBox;
        private TextBox ticketCodeTextBox;
        private Button checkInButton;

        public CheckInForm(string selectedEvent = null)
        {
            InitializeComponents();
            SetTitle("Event Check-In");
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

            Label ticketCodeLabel = new Label
            {
                Text = "Ticket Code:",
                Font = new Font("Arial", 12),
                Location = new Point(50, 100),
                Size = new Size(120, 30)
            };

            ticketCodeTextBox = new TextBox
            {
                Font = new Font("Arial", 12),
                Location = new Point(180, 100),
                Size = new Size(300, 30)
            };

            checkInButton = new Button
            {
                Text = "Check In",
                Font = new Font("Arial", 14),
                Size = new Size(200, 50),
                Location = new Point(this.ClientSize.Width / 2 - 100, 180),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            checkInButton.Click += CheckInButton_Click;

            this.Controls.Add(eventLabel);
            this.Controls.Add(eventComboBox);
            this.Controls.Add(ticketCodeLabel);
            this.Controls.Add(ticketCodeTextBox);
            this.Controls.Add(checkInButton);
        }

        private void CheckInButton_Click(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an event.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ticketCodeTextBox.Text))
            {
                MessageBox.Show("Please enter your ticket code.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Here you would typically validate the ticket code against your database
            MessageBox.Show($"Successfully checked in to {eventComboBox.SelectedItem}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}