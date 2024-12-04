using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventVerse
{
    public class VendorManagementForm : BaseForm
    {
        private ListBox vendorListBox;
        private Button addVendorButton;
        private Button editVendorButton;
        private Button deleteVendorButton;

        public VendorManagementForm()
        {
            InitializeComponents();
            SetTitle("Vendor Management");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(500, 400);

            Label titleLabel = new Label
            {
                Text = "Vendor Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(300, 30)
            };

            vendorListBox = new ListBox
            {
                Location = new Point(20, 100),
                Size = new Size(300, 250),
                Font = new Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Moved buttons down and aligned with list content
            addVendorButton = new Button
            {
                Text = "Add Vendor",
                Location = new Point(340, 100), // Aligned with top of ListBox
                Size = new Size(120, 35),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            addVendorButton.Click += AddVendorButton_Click;

            editVendorButton = new Button
            {
                Text = "Edit Vendor",
                Location = new Point(340, 145), // Added spacing between buttons
                Size = new Size(120, 35),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            editVendorButton.Click += EditVendorButton_Click;

            deleteVendorButton = new Button
            {
                Text = "Delete Vendor",
                Location = new Point(340, 190), // Added spacing between buttons
                Size = new Size(120, 35),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            deleteVendorButton.Click += DeleteVendorButton_Click;

            // Add hover effects for buttons
            addVendorButton.MouseEnter += (s, e) => addVendorButton.BackColor = Color.FromArgb(0, 102, 184);
            addVendorButton.MouseLeave += (s, e) => addVendorButton.BackColor = Color.FromArgb(0, 122, 204);

            editVendorButton.MouseEnter += (s, e) => editVendorButton.BackColor = Color.FromArgb(0, 102, 184);
            editVendorButton.MouseLeave += (s, e) => editVendorButton.BackColor = Color.FromArgb(0, 122, 204);

            deleteVendorButton.MouseEnter += (s, e) => deleteVendorButton.BackColor = Color.FromArgb(200, 33, 49);
            deleteVendorButton.MouseLeave += (s, e) => deleteVendorButton.BackColor = Color.FromArgb(220, 53, 69);

            this.Controls.AddRange(new Control[] { titleLabel, vendorListBox, addVendorButton, editVendorButton, deleteVendorButton });

            // Add some sample vendors
            vendorListBox.Items.AddRange(new string[] {
                "Catering Co.",
                "Security Services",
                "Audio Equipment Rental",
                "Lighting Services",
                "Decoration Services"
            });
        }

        private void AddVendorButton_Click(object sender, EventArgs e)
        {
            string newVendor = InputBox.Show("Enter new vendor name:", "Add Vendor");
            if (!string.IsNullOrWhiteSpace(newVendor))
            {
                vendorListBox.Items.Add(newVendor);
            }
        }

        private void EditVendorButton_Click(object sender, EventArgs e)
        {
            if (vendorListBox.SelectedIndex != -1)
            {
                string currentVendor = vendorListBox.SelectedItem.ToString();
                string editedVendor = InputBox.Show("Edit vendor name:", "Edit Vendor", currentVendor);
                if (!string.IsNullOrWhiteSpace(editedVendor))
                {
                    vendorListBox.Items[vendorListBox.SelectedIndex] = editedVendor;
                }
            }
            else
            {
                MessageBox.Show("Please select a vendor to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteVendorButton_Click(object sender, EventArgs e)
        {
            if (vendorListBox.SelectedIndex != -1)
            {
                if (MessageBox.Show("Are you sure you want to delete this vendor?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    vendorListBox.Items.RemoveAt(vendorListBox.SelectedIndex);
                }
            }
            else
            {
                MessageBox.Show("Please select a vendor to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoBox
            // 
            this.logoBox.Click += new System.EventHandler(this.logoBox_Click);
            // 
            // VendorManagementForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Name = "VendorManagementForm";
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
        }

        private void logoBox_Click(object sender, EventArgs e)
        {
            // Implement logo click functionality if needed
        }
    }

    // InputBox class remains unchanged
    public static class InputBox
    {
        public static string Show(string prompt, string title, string defaultValue = "")
        {
            Form inputForm = new Form
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label { Left = 10, Top = 10, Text = prompt, Width = 360 };
            TextBox inputTextBox = new TextBox { Left = 10, Top = 40, Width = 360, Text = defaultValue };
            Button confirmation = new Button { Text = "OK", Left = 260, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            Button cancel = new Button { Text = "Cancel", Left = 150, Width = 100, Top = 70, DialogResult = DialogResult.Cancel };

            confirmation.Click += (sender, e) => { inputForm.Close(); };
            cancel.Click += (sender, e) => { inputForm.Close(); };

            inputForm.Controls.Add(textLabel);
            inputForm.Controls.Add(inputTextBox);
            inputForm.Controls.Add(confirmation);
            inputForm.Controls.Add(cancel);
            inputForm.AcceptButton = confirmation;

            return inputForm.ShowDialog() == DialogResult.OK ? inputTextBox.Text : string.Empty;
        }
    }
}