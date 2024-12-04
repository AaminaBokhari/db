using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace EventVerse
{
    public class VendorRegistrationForm : Form
    {
        private TextBox nameTextBox;
        private TextBox emailTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private TextBox companyNameTextBox;
        private TextBox phoneNumberTextBox;
        private RichTextBox serviceDescriptionTextBox;
        private ComboBox businessTypeComboBox;
        private CheckedListBox servicesOfferedCheckedListBox;
        private Button registerButton;
        private Button cancelButton;

        public VendorRegistrationForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(500, 700);
            this.Text = "Vendor Registration";
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Vendor Registration",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(150, 20),
                Size = new Size(200, 30)
            };

            Label nameLabel = new Label { Text = "Name:", Location = new Point(50, 70), Size = new Size(100, 20) };
            nameTextBox = new TextBox { Location = new Point(200, 70), Size = new Size(200, 20) };

            Label emailLabel = new Label { Text = "Email:", Location = new Point(50, 100), Size = new Size(100, 20) };
            emailTextBox = new TextBox { Location = new Point(200, 100), Size = new Size(200, 20) };

            Label passwordLabel = new Label { Text = "Password:", Location = new Point(50, 130), Size = new Size(100, 20) };
            passwordTextBox = new TextBox { Location = new Point(200, 130), Size = new Size(200, 20), PasswordChar = '*' };

            Label confirmPasswordLabel = new Label { Text = "Confirm Password:", Location = new Point(50, 160), Size = new Size(120, 20) };
            confirmPasswordTextBox = new TextBox { Location = new Point(200, 160), Size = new Size(200, 20), PasswordChar = '*' };

            Label companyNameLabel = new Label { Text = "Company Name:", Location = new Point(50, 190), Size = new Size(100, 20) };
            companyNameTextBox = new TextBox { Location = new Point(200, 190), Size = new Size(200, 20) };

            Label phoneNumberLabel = new Label { Text = "Phone Number:", Location = new Point(50, 220), Size = new Size(100, 20) };
            phoneNumberTextBox = new TextBox { Location = new Point(200, 220), Size = new Size(200, 20) };

            Label businessTypeLabel = new Label { Text = "Business Type:", Location = new Point(50, 250), Size = new Size(100, 20) };
            businessTypeComboBox = new ComboBox { Location = new Point(200, 250), Size = new Size(200, 20) };
            businessTypeComboBox.Items.AddRange(new object[] { "Catering", "Audio/Visual", "Decor", "Entertainment", "Venue", "Other" });

            Label servicesOfferedLabel = new Label { Text = "Services Offered:", Location = new Point(50, 280), Size = new Size(100, 20) };
            servicesOfferedCheckedListBox = new CheckedListBox { Location = new Point(200, 280), Size = new Size(200, 100) };
            servicesOfferedCheckedListBox.Items.AddRange(new object[] { "Food & Beverage", "Equipment Rental", "Staffing", "Photography", "Videography", "Event Planning" });

            Label serviceDescriptionLabel = new Label { Text = "Service Description:", Location = new Point(50, 390), Size = new Size(120, 20) };
            serviceDescriptionTextBox = new RichTextBox { Location = new Point(200, 390), Size = new Size(200, 100) };

            registerButton = new Button
            {
                Text = "Register",
                Location = new Point(150, 510),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            registerButton.Click += RegisterButton_Click;

            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(270, 510),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            cancelButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[]
            {
                titleLabel, nameLabel, nameTextBox, emailLabel, emailTextBox,
                passwordLabel, passwordTextBox, confirmPasswordLabel, confirmPasswordTextBox,
                companyNameLabel, companyNameTextBox, phoneNumberLabel, phoneNumberTextBox,
                businessTypeLabel, businessTypeComboBox, servicesOfferedLabel, servicesOfferedCheckedListBox,
                serviceDescriptionLabel, serviceDescriptionTextBox,
                registerButton, cancelButton
            });
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                // TODO: Implement registration logic
                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) ||
                string.IsNullOrWhiteSpace(emailTextBox.Text) ||
                string.IsNullOrWhiteSpace(passwordTextBox.Text) ||
                string.IsNullOrWhiteSpace(confirmPasswordTextBox.Text) ||
                string.IsNullOrWhiteSpace(companyNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(phoneNumberTextBox.Text) ||
                businessTypeComboBox.SelectedIndex == -1 ||
                servicesOfferedCheckedListBox.CheckedItems.Count == 0 ||
                string.IsNullOrWhiteSpace(serviceDescriptionTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields and select at least one service offered.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!IsValidEmail(emailTextBox.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!IsValidPhoneNumber(phoneNumberTextBox.Text))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.Match(phoneNumber, @"^(\+\d{1,2}\s)?$$?\d{3}$$?[\s.-]?\d{3}[\s.-]?\d{4}$").Success;
        }
    }
}