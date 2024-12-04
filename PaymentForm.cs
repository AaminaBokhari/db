using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class PaymentForm : BaseForm
    {
        private Label eventNameLabel;
        private Label ticketTypeLabel;
        private Label quantityLabel;
        private Label totalLabel;
        private TextBox cardNumberTextBox;
        private TextBox cardHolderNameTextBox;
        private ComboBox expirationMonthComboBox;
        private ComboBox expirationYearComboBox;
        private TextBox cvvTextBox;
        private Button purchaseButton;

        private string eventName;
        private string ticketType;
        private int quantity;
        private decimal totalAmount;
        private string attendeeName;
        private string attendeeEmail;

        public PaymentForm()
        {
        }

        public PaymentForm(string eventName, string ticketType, int quantity, decimal totalAmount, string attendeeName, string attendeeEmail)
        {
            this.eventName = eventName;
            this.ticketType = ticketType;
            this.quantity = quantity;
            this.totalAmount = totalAmount;
            this.attendeeName = attendeeName;
            this.attendeeEmail = attendeeEmail;

            InitializeComponents();
            SetTitle("Complete Payment");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Booking details
            eventNameLabel = new Label
            {
                Text = $"Event: {eventName}",
                Location = new Point(50, 50),
                Size = new Size(400, 20),
                Font = new Font(this.Font, FontStyle.Bold)
            };

            ticketTypeLabel = new Label
            {
                Text = $"Ticket Type: {ticketType}",
                Location = new Point(50, 80),
                Size = new Size(400, 20)
            };

            quantityLabel = new Label
            {
                Text = $"Quantity: {quantity}",
                Location = new Point(50, 110),
                Size = new Size(400, 20)
            };

            totalLabel = new Label
            {
                Text = $"Total Amount: ${totalAmount}",
                Location = new Point(50, 140),
                Size = new Size(400, 20),
                Font = new Font(this.Font, FontStyle.Bold)
            };

            // Payment information
            Label cardNumberLabel = new Label
            {
                Text = "Card Number:",
                Location = new Point(50, 180),
                Size = new Size(120, 20)
            };

            cardNumberTextBox = new TextBox
            {
                Location = new Point(180, 180),
                Size = new Size(250, 20)
            };

            Label cardHolderNameLabel = new Label
            {
                Text = "Cardholder Name:",
                Location = new Point(50, 220),
                Size = new Size(120, 20)
            };

            cardHolderNameTextBox = new TextBox
            {
                Location = new Point(180, 220),
                Size = new Size(250, 20)
            };

            Label expirationLabel = new Label
            {
                Text = "Expiration Date:",
                Location = new Point(50, 260),
                Size = new Size(120, 20)
            };

            expirationMonthComboBox = new ComboBox
            {
                Location = new Point(180, 260),
                Size = new Size(60, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            for (int i = 1; i <= 12; i++)
            {
                expirationMonthComboBox.Items.Add(i.ToString("D2"));
            }

            expirationYearComboBox = new ComboBox
            {
                Location = new Point(250, 260),
                Size = new Size(80, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 10; i++)
            {
                expirationYearComboBox.Items.Add((currentYear + i).ToString());
            }

            Label cvvLabel = new Label
            {
                Text = "CVV:",
                Location = new Point(350, 260),
                Size = new Size(40, 20)
            };

            cvvTextBox = new TextBox
            {
                Location = new Point(400, 260),
                Size = new Size(40, 20),
                MaxLength = 3
            };

            // Purchase button
            purchaseButton = new Button
            {
                Text = "Complete Purchase",
                Location = new Point(150, 320),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            purchaseButton.Click += PurchaseButton_Click;

            // Add controls to the form
            this.Controls.Add(eventNameLabel);
            this.Controls.Add(ticketTypeLabel);
            this.Controls.Add(quantityLabel);
            this.Controls.Add(totalLabel);
            this.Controls.Add(cardNumberLabel);
            this.Controls.Add(cardNumberTextBox);
            this.Controls.Add(cardHolderNameLabel);
            this.Controls.Add(cardHolderNameTextBox);
            this.Controls.Add(expirationLabel);
            this.Controls.Add(expirationMonthComboBox);
            this.Controls.Add(expirationYearComboBox);
            this.Controls.Add(cvvLabel);
            this.Controls.Add(cvvTextBox);
            this.Controls.Add(purchaseButton);
        }

        private void PurchaseButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                // In a real application, you would process the payment here
                MessageBox.Show("Payment processed successfully! Your tickets have been purchased.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update the PaymentManagementForm with the new transaction
                PaymentManagementForm paymentManagementForm = new PaymentManagementForm();
                paymentManagementForm.AddNewPayment(eventName, attendeeName, totalAmount, DateTime.Now, "Completed");

                this.Close();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(cardNumberTextBox.Text) || cardNumberTextBox.Text.Length < 16)
            {
                MessageBox.Show("Please enter a valid card number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cardHolderNameTextBox.Text))
            {
                MessageBox.Show("Please enter the cardholder's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (expirationMonthComboBox.SelectedIndex == -1 || expirationYearComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the card's expiration date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cvvTextBox.Text) || cvvTextBox.Text.Length < 3)
            {
                MessageBox.Show("Please enter a valid CVV.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}