using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class OrganizerProfileForm : BaseForm
    {
        private TextBox nameTextBox;
        private TextBox emailTextBox;
        private TextBox companyNameTextBox;
        private TextBox phoneNumberTextBox;
        private Button updateButton;
        private Button cancelButton;

        public OrganizerProfileForm()
        {
            InitializeComponents();
            SetTitle("Organizer Profile");
            LoadProfile(); // TODO: Implement this method to load the organizer's profile
        }

        private void InitializeComponents()
        {
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Organizer Profile",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(100, 20),
                Size = new Size(200, 30)
            };

            Label nameLabel = new Label
            {
                Text = "Name:",
                Location = new Point(50, 70),
                Size = new Size(100, 20)
            };
            nameTextBox = new TextBox
            {
                Location = new Point(160, 70),
                Size = new Size(180, 20)
            };

            Label emailLabel = new Label
            {
                Text = "Email:",
                Location = new Point(50, 110),
                Size = new Size(100, 20)
            };
            emailTextBox = new TextBox
            {
                Location = new Point(160, 110),
                Size = new Size(180, 20)
            };

            Label companyNameLabel = new Label
            {
                Text = "Company Name:",
                Location = new Point(50, 150),
                Size = new Size(100, 20)
            };
            companyNameTextBox = new TextBox
            {
                Location = new Point(160, 150),
                Size = new Size(180, 20)
            };

            Label phoneNumberLabel = new Label
            {
                Text = "Phone Number:",
                Location = new Point(50, 190),
                Size = new Size(100, 20)
            };
            phoneNumberTextBox = new TextBox
            {
                Location = new Point(160, 190),
                Size = new Size(180, 20)
            };

            updateButton = new Button
            {
                Text = "Update Profile",
                Location = new Point(100, 240),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            updateButton.Click += UpdateButton_Click;

            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(230, 240),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            cancelButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[]
            {
                titleLabel, nameLabel, nameTextBox, emailLabel, emailTextBox,
                companyNameLabel, companyNameTextBox, phoneNumberLabel, phoneNumberTextBox,
                updateButton, cancelButton
            });
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                // TODO: Implement profile update logic
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) ||
                string.IsNullOrWhiteSpace(emailTextBox.Text) ||
                string.IsNullOrWhiteSpace(companyNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(phoneNumberTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void LoadProfile()
        {
            // TODO: Implement logic to load the organizer's profile from the database
            // For now, we'll use placeholder data
            nameTextBox.Text = "John Doe";
            emailTextBox.Text = "john.doe@example.com";
            companyNameTextBox.Text = "Event Masters Inc.";
            phoneNumberTextBox.Text = "123-456-7890";
        }
    }
}