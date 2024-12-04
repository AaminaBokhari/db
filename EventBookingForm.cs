using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace EventVerse
{
    public class EventBookingForm : BaseForm
    {
        private ComboBox eventComboBox;
        private ListBox ticketTypeListBox;
        private NumericUpDown quantityNumericUpDown;
        private DateTimePicker eventDatePicker;
        private TextBox nameTextBox;
        private TextBox emailTextBox;
        private Button proceedToPaymentButton;
        private Label totalLabel;

        private Dictionary<string, List<TicketType>> eventTickets;

        public EventBookingForm()
        {
            InitializeComponents();
            SetTitle("Event Booking");
            LoadEventData();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Book Your Event",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Size = new Size(400, 30),
                Location = new Point(50, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label eventLabel = new Label
            {
                Text = "Select Event:",
                Location = new Point(50, 70),
                Size = new Size(100, 20)
            };

            eventComboBox = new ComboBox
            {
                Location = new Point(160, 70),
                Size = new Size(280, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            eventComboBox.SelectedIndexChanged += EventComboBox_SelectedIndexChanged;

            Label ticketTypeLabel = new Label
            {
                Text = "Ticket Type:",
                Location = new Point(50, 110),
                Size = new Size(100, 20)
            };

            ticketTypeListBox = new ListBox
            {
                Location = new Point(160, 110),
                Size = new Size(280, 80)
            };
            ticketTypeListBox.SelectedIndexChanged += TicketTypeListBox_SelectedIndexChanged;

            Label quantityLabel = new Label
            {
                Text = "Quantity:",
                Location = new Point(50, 200),
                Size = new Size(100, 20)
            };

            quantityNumericUpDown = new NumericUpDown
            {
                Location = new Point(160, 200),
                Size = new Size(60, 20),
                Minimum = 1,
                Maximum = 10,
                Value = 1
            };
            quantityNumericUpDown.ValueChanged += QuantityNumericUpDown_ValueChanged;

            Label dateLabel = new Label
            {
                Text = "Event Date:",
                Location = new Point(50, 240),
                Size = new Size(100, 20)
            };

            eventDatePicker = new DateTimePicker
            {
                Location = new Point(160, 240),
                Size = new Size(180, 20),
                Format = DateTimePickerFormat.Short
            };

            Label nameLabel = new Label
            {
                Text = "Your Name:",
                Location = new Point(50, 280),
                Size = new Size(100, 20)
            };

            nameTextBox = new TextBox
            {
                Location = new Point(160, 280),
                Size = new Size(280, 20)
            };

            Label emailLabel = new Label
            {
                Text = "Your Email:",
                Location = new Point(50, 320),
                Size = new Size(100, 20)
            };

            emailTextBox = new TextBox
            {
                Location = new Point(160, 320),
                Size = new Size(280, 20)
            };

            totalLabel = new Label
            {
                Text = "Total: $0",
                Location = new Point(50, 360),
                Size = new Size(200, 20),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            proceedToPaymentButton = new Button
            {
                Text = "Proceed to Payment",
                Location = new Point(160, 400),
                Size = new Size(180, 40),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            proceedToPaymentButton.Click += ProceedToPaymentButton_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(eventLabel);
            this.Controls.Add(eventComboBox);
            this.Controls.Add(ticketTypeLabel);
            this.Controls.Add(ticketTypeListBox);
            this.Controls.Add(quantityLabel);
            this.Controls.Add(quantityNumericUpDown);
            this.Controls.Add(dateLabel);
            this.Controls.Add(eventDatePicker);
            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);
            this.Controls.Add(emailLabel);
            this.Controls.Add(emailTextBox);
            this.Controls.Add(totalLabel);
            this.Controls.Add(proceedToPaymentButton);
        }

        private void LoadEventData()
        {
            eventTickets = new Dictionary<string, List<TicketType>>
            {
                {
                    "Tech Conference 2024", new List<TicketType>
                    {
                        new TicketType { Name = "General Admission", Price = 100 },
                        new TicketType { Name = "VIP", Price = 250 },
                        new TicketType { Name = "Early Bird", Price = 80 }
                    }
                },
                {
                    "Music Festival", new List<TicketType>
                    {
                        new TicketType { Name = "1-Day Pass", Price = 50 },
                        new TicketType { Name = "3-Day Pass", Price = 120 },
                        new TicketType { Name = "VIP Pass", Price = 200 }
                    }
                },
                {
                    "Business Workshop", new List<TicketType>
                    {
                        new TicketType { Name = "Standard", Price = 75 },
                        new TicketType { Name = "Premium", Price = 150 }
                    }
                }
            };

            eventComboBox.Items.AddRange(eventTickets.Keys.ToArray());
        }

        private void EventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedItem != null)
            {
                string selectedEvent = eventComboBox.SelectedItem.ToString();
                ticketTypeListBox.Items.Clear();
                ticketTypeListBox.Items.AddRange(eventTickets[selectedEvent].ToArray());
            }
            UpdateTotal();
        }

        private void TicketTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void QuantityNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            if (eventComboBox.SelectedItem != null && ticketTypeListBox.SelectedItem != null)
            {
                TicketType selectedTicket = (TicketType)ticketTypeListBox.SelectedItem;
                int quantity = (int)quantityNumericUpDown.Value;
                decimal totalPrice = selectedTicket.Price * quantity;
                totalLabel.Text = $"Total: ${totalPrice}";
            }
            else
            {
                totalLabel.Text = "Total: $0";
            }
        }

        private void ProceedToPaymentButton_Click(object sender, EventArgs e)
        {
            if (ValidateBooking())
            {
                string selectedEvent = eventComboBox.SelectedItem.ToString();
                TicketType selectedTicket = (TicketType)ticketTypeListBox.SelectedItem;
                int quantity = (int)quantityNumericUpDown.Value;
                decimal totalPrice = selectedTicket.Price * quantity;

                PaymentForm paymentForm = new PaymentForm(selectedEvent, selectedTicket.Name, quantity, totalPrice, nameTextBox.Text, emailTextBox.Text);
                this.Hide();
                paymentForm.FormClosed += (s, args) => this.Close();
                paymentForm.Show();
            }
        }

        private bool ValidateBooking()
        {
            if (eventComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an event.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (ticketTypeListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a ticket type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Please enter your name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(emailTextBox.Text) || !emailTextBox.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }

    public class TicketType
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name} - ${Price}";
        }
    }
}