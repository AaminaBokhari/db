using System.Drawing;
using System.Windows.Forms;

public class BaseForm : Form
{
    protected PictureBox logoBox;
    protected Label titleLabel;
    protected Button backButton;

    public BaseForm()
    {
        InitializeBaseComponents();
        ApplyStyles();
    }

    private void InitializeBaseComponents()
    {
        // Set default form size
        this.ClientSize = new Size(800, 600);
        this.MinimumSize = new Size(800, 600);

        logoBox = new PictureBox
        {
            Image = EventVerse.Properties.Resources.Logo,
            SizeMode = PictureBoxSizeMode.Zoom,
            Size = new Size(80, 80),
            Location = new Point(20, 20)
        };

        titleLabel = new Label
        {
            Font = new Font("Arial", 24, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 102, 204),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = false,
            Size = new Size(600, 40),
            Location = new Point(100, 40)
        };

        backButton = new Button
        {
            Text = "Back",
            Font = new Font("Arial", 12),
            Size = new Size(100, 40),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 102, 204),
            ForeColor = Color.White
        };

        // Position back button at bottom left
        backButton.Location = new Point(20, this.ClientSize.Height - backButton.Height - 20);

        backButton.Click += (sender, e) => this.Close();

        this.Controls.Add(logoBox);
        this.Controls.Add(titleLabel);
        this.Controls.Add(backButton);

        // Handle form resize to reposition back button
        this.Resize += (sender, e) =>
        {
            backButton.Location = new Point(20, this.ClientSize.Height - backButton.Height - 20);
            titleLabel.Left = (this.ClientSize.Width - titleLabel.Width) / 2;
        };
    }

    private void ApplyStyles()
    {
        this.BackColor = Color.White;
        this.Font = new Font("Arial", 12);
        this.ForeColor = Color.FromArgb(64, 64, 64);
        this.Padding = new Padding(20);
        this.StartPosition = FormStartPosition.CenterScreen;
    }

    protected void SetTitle(string title)
    {
        this.Text = title;
        titleLabel.Text = title;
        titleLabel.Left = (this.ClientSize.Width - titleLabel.Width) / 2;
    }
}