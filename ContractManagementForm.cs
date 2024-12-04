using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class ContractManagementForm : Form
    {
        private DataGridView contractsGridView;
        private Button viewContractButton;
        private Button signContractButton;
        private Button viewPaymentsButton;

        public ContractManagementForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(800, 600);
            this.Text = "Contract Management";
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Contract Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(50, 30),
                Size = new Size(300, 30)
            };

            contractsGridView = new DataGridView
            {
                Location = new Point(50, 80),
                Size = new Size(700, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            contractsGridView.Columns.Add("EventName", "Event Name");
            contractsGridView.Columns.Add("ContractType", "Contract Type");
            contractsGridView.Columns.Add("Status", "Status");
            contractsGridView.Columns.Add("Value", "Value");
            contractsGridView.Columns.Add("PaymentStatus", "Payment Status");

            viewContractButton = CreateButton("View Contract", new Point(50, 500), ViewContractButton_Click);
            signContractButton = CreateButton("Sign Contract", new Point(220, 500), SignContractButton_Click);
            viewPaymentsButton = CreateButton("View Payments", new Point(390, 500), ViewPaymentsButton_Click);

            this.Controls.AddRange(new Control[] {
                titleLabel, contractsGridView, viewContractButton, signContractButton, viewPaymentsButton
            });

            LoadContracts();
        }

        private Button CreateButton(string text, Point location, EventHandler clickHandler)
        {
            Button button = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            button.Click += clickHandler;
            return button;
        }

        private void LoadContracts()
        {
            contractsGridView.Rows.Clear();
            contractsGridView.Rows.Add("Tech Conference 2024", "Service", "Pending Signature", "$5000", "Not Paid");
            contractsGridView.Rows.Add("Music Festival", "Sponsorship", "Signed", "$10000", "Partially Paid");
            contractsGridView.Rows.Add("Business Workshop", "Service", "In Negotiation", "$3000", "Not Paid");
        }

        private void ViewContractButton_Click(object sender, EventArgs e)
        {
            if (contractsGridView.SelectedRows.Count > 0)
            {
                var selectedRow = contractsGridView.SelectedRows[0];
                string eventName = selectedRow.Cells["EventName"].Value.ToString();
                string contractType = selectedRow.Cells["ContractType"].Value.ToString();

                ContractViewForm contractViewForm = new ContractViewForm(eventName, contractType);
                contractViewForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a contract to view.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SignContractButton_Click(object sender, EventArgs e)
        {
            if (contractsGridView.SelectedRows.Count > 0)
            {
                var selectedRow = contractsGridView.SelectedRows[0];
                string eventName = selectedRow.Cells["EventName"].Value.ToString();
                string status = selectedRow.Cells["Status"].Value.ToString();

                if (status == "Pending Signature")
                {
                    selectedRow.Cells["Status"].Value = "Signed";
                    MessageBox.Show($"Contract for {eventName} has been signed.", "Contract Signed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("This contract cannot be signed at this time.", "Cannot Sign", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a contract to sign.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ViewPaymentsButton_Click(object sender, EventArgs e)
        {
            if (contractsGridView.SelectedRows.Count > 0)
            {
                var selectedRow = contractsGridView.SelectedRows[0];
                string eventName = selectedRow.Cells["EventName"].Value.ToString();
                string contractType = selectedRow.Cells["ContractType"].Value.ToString();

                PaymentTrackingForm paymentTrackingForm = new PaymentTrackingForm(eventName, contractType);
                paymentTrackingForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a contract to view its payments.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
