using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class AdminRegistrationForm : BaseForm
    {
        private TextBox nameTextBox;
        private TextBox emailTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private TextBox employeeIdTextBox;
        private ComboBox departmentComboBox;
        private Button registerButton;
        private Button cancelButton;

        public AdminRegistrationForm()
        {
            InitializeComponents();
            SetTitle("Admin Registration");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(400, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Admin Registration",
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

            Label passwordLabel = new Label
            {
                Text = "Password:",
                Location = new Point(50, 150),
                Size = new Size(100, 20)
            };
            passwordTextBox = new TextBox
            {
                Location = new Point(160, 150),
                Size = new Size(180, 20),
                PasswordChar = '*'
            };

            Label confirmPasswordLabel = new Label
            {
                Text = "Confirm Password:",
                Location = new Point(50, 190),
                Size = new Size(100, 20)
            };
            confirmPasswordTextBox = new TextBox
            {
                Location = new Point(160, 190),
                Size = new Size(180, 20),
                PasswordChar = '*'
            };

            Label employeeIdLabel = new Label
            {
                Text = "Employee ID:",
                Location = new Point(50, 230),
                Size = new Size(100, 20)
            };
            employeeIdTextBox = new TextBox
            {
                Location = new Point(160, 230),
                Size = new Size(180, 20)
            };

            Label departmentLabel = new Label
            {
                Text = "Department:",
                Location = new Point(50, 270),
                Size = new Size(100, 20)
            };
            departmentComboBox = new ComboBox
            {
                Location = new Point(160, 270),
                Size = new Size(180, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            departmentComboBox.Items.AddRange(new object[] { "IT", "HR", "Finance", "Operations" });

            registerButton = new Button
            {
                Text = "Register",
                Location = new Point(100, 320),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            registerButton.Click += RegisterButton_Click;

            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(220, 320),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            cancelButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] 
            {
                titleLabel, nameLabel, nameTextBox, emailLabel, emailTextBox,
                passwordLabel, passwordTextBox, confirmPasswordLabel, confirmPasswordTextBox,
                employeeIdLabel, employeeIdTextBox, departmentLabel, departmentComboBox,
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
                string.IsNullOrWhiteSpace(employeeIdTextBox.Text) ||
                departmentComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}

