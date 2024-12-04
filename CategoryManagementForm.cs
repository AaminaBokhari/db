using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EventVerse
{
    public class CategoryManagementForm : BaseForm
    {
        private ListBox categoriesListBox;
        private TextBox categoryNameTextBox;
        private Button addButton;
        private Button editButton;
        private Button deleteButton;

        public CategoryManagementForm()
        {
            InitializeComponents();
            SetTitle("Category Management");
        }

        private void InitializeComponents()
        {
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Category Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(300, 30)
            };

            categoriesListBox = new ListBox
            {
                Location = new Point(20, 60),
                Size = new Size(300, 250),
                Font = new Font("Arial", 12)
            };

            categoryNameTextBox = new TextBox
            {
                Location = new Point(340, 60),
                Size = new Size(120, 25),
                Font = new Font("Arial", 12)
            };

            addButton = new Button
            {
                Text = "Add",
                Location = new Point(340, 100),
                Size = new Size(120, 30),
                Font = new Font("Arial", 12),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            addButton.Click += AddButton_Click;

            editButton = new Button
            {
                Text = "Edit",
                Location = new Point(340, 140),
                Size = new Size(120, 30),
                Font = new Font("Arial", 12),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            editButton.Click += EditButton_Click;

            deleteButton = new Button
            {
                Text = "Delete",
                Location = new Point(340, 180),
                Size = new Size(120, 30),
                Font = new Font("Arial", 12),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            deleteButton.Click += DeleteButton_Click;

            this.Controls.AddRange(new Control[] { titleLabel, categoriesListBox, categoryNameTextBox, addButton, editButton, deleteButton });

            LoadCategories();
        }

        private void LoadCategories()
        {
            // In a real application, this would load categories from a database
            List<string> sampleCategories = new List<string> { "Music", "Technology", "Business", "Sports", "Art" };
            categoriesListBox.Items.AddRange(sampleCategories.ToArray());
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(categoryNameTextBox.Text))
            {
                categoriesListBox.Items.Add(categoryNameTextBox.Text);
                categoryNameTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a category name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (categoriesListBox.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(categoryNameTextBox.Text))
            {
                categoriesListBox.Items[categoriesListBox.SelectedIndex] = categoryNameTextBox.Text;
                categoryNameTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please select a category and enter a new name.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (categoriesListBox.SelectedIndex != -1)
            {
                if (MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    categoriesListBox.Items.RemoveAt(categoriesListBox.SelectedIndex);
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
