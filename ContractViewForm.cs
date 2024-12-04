using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class ContractViewForm : BaseForm
    {
        private string eventName;
        private string contractType;
        private RichTextBox contractContentBox;
        private Button closeButton;

        public ContractViewForm(string eventName, string contractType)
        {
            this.eventName = eventName;
            this.contractType = contractType;
            InitializeComponents();
            SetTitle($"Contract View - {eventName}");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Title remains at the top
            Label titleLabel = new Label
            {
                Text = $"Contract for {eventName}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(50, 30),
                Size = new Size(700, 30)
            };

            // Move other elements down by adding an offset
            int verticalOffset = 50;  // Additional space after title

            Label contractTypeLabel = new Label
            {
                Text = $"Contract Type: {contractType}",
                Font = new Font("Arial", 12, FontStyle.Regular),
                Location = new Point(50, 110 + verticalOffset),  // Moved down
                Size = new Size(700, 20)
            };

            contractContentBox = new RichTextBox
            {
                Location = new Point(50, 140 + verticalOffset),  // Moved down
                Size = new Size(700, 350),  // Adjusted size to fit
                ReadOnly = true,
                Font = new Font("Arial", 12)
            };

            closeButton = new Button
            {
                Text = "Close",
                Location = new Point(350, 510 + verticalOffset),  // Moved down
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            closeButton.Click += CloseButton_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(contractTypeLabel);
            this.Controls.Add(contractContentBox);
            this.Controls.Add(closeButton);

            LoadContractContent();
        }

        private void LoadContractContent()
        {
            contractContentBox.Text = $"This is a sample contract for the event '{eventName}' of type '{contractType}'.\n\n" +
                                      "1. Terms and Conditions\n" +
                                      "2. Scope of Work\n" +
                                      "3. Payment Terms\n" +
                                      "4. Cancellation Policy\n" +
                                      "5. Liability and Insurance\n\n" +
                                      "... (contract details would go here) ...";
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();

            this.logoBox.Click += new System.EventHandler(this.logoBox_Click);

            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "ContractViewForm";
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
        }

        private void logoBox_Click(object sender, EventArgs e)
        {
        }
    }
}