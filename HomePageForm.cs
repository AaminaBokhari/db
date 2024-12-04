using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using EventVerse;

public class HomePageForm : BaseForm
{
    private Button attendeeLoginButton;
    private Button organizerLoginButton;
    private Button adminLoginButton;
    private Button vendorLoginButton;
    private Label welcomeLabel;
    private PictureBox logoBox;

    public HomePageForm()
    {
        InitializeHomeComponents();
        SetTitle("Welcome to EventVerse");
        backButton.Visible = false; // Hide back button on home page
    }

    private void InitializeHomeComponents()
    {
        // Initialize and configure the logo
        logoBox = new PictureBox
        {
            Size = new Size(100, 100),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(50, 30)
        };
        SetLogoImage();
        logoBox.Left = (this.ClientSize.Width - logoBox.Width) / 2;

        // Initialize the welcome label
        welcomeLabel = new Label
        {
            Text = "Welcome to EventVerse - Your Ultimate Event Management Platform",
            Font = new Font("Arial", 16, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = false,
            Size = new Size(600, 60),
            Location = new Point(100, 150)
        };
        welcomeLabel.Left = (this.ClientSize.Width - welcomeLabel.Width) / 2;

        // Create and position buttons
        attendeeLoginButton = CreateLoginButton("Attendee Login", 0);
        organizerLoginButton = CreateLoginButton("Organizer Login", 1);
        adminLoginButton = CreateLoginButton("Admin Login", 2);
        vendorLoginButton = CreateLoginButton("Vendor Login", 3);

        // Add controls to the form
        this.Controls.Add(logoBox);
        this.Controls.Add(welcomeLabel);
        this.Controls.Add(attendeeLoginButton);
        this.Controls.Add(organizerLoginButton);
        this.Controls.Add(adminLoginButton);
        this.Controls.Add(vendorLoginButton);

        // Handle form resize
        this.Resize += (sender, e) =>
        {
            logoBox.Left = (this.ClientSize.Width - logoBox.Width) / 2;
            welcomeLabel.Left = (this.ClientSize.Width - welcomeLabel.Width) / 2;
            RecenterButtons();
        };
    }

    private void SetLogoImage()
    {
        string logoPath = Path.Combine(Application.StartupPath, "LOGO.png");

        // Add diagnostic message for debugging
        System.Diagnostics.Debug.WriteLine($"Attempting to load logo from: {logoPath}");

        if (File.Exists(logoPath))
        {
            try
            {
                // Load image using a memory stream to avoid file locking
                using (var fs = new FileStream(logoPath, FileMode.Open, FileAccess.Read))
                using (var ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    logoBox.Image = Image.FromStream(ms);
                }

                System.Diagnostics.Debug.WriteLine("Logo loaded successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading logo: {ex.Message}");
                MessageBox.Show(
                    $"Error loading logo: {ex.Message}",
                    "Logo Loading Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        else
        {
            string errorMessage = $"Logo file not found at: {logoPath}";
            System.Diagnostics.Debug.WriteLine(errorMessage);
            MessageBox.Show(
                errorMessage,
                "Logo Not Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private Button CreateLoginButton(string text, int index)
    {
        int buttonWidth = 200;
        int buttonHeight = 60;
        int verticalSpacing = 80;
        int startY = 250;

        Button button = new Button
        {
            Text = text,
            Font = new Font("Arial", 14),
            Size = new Size(buttonWidth, buttonHeight),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 102, 204),
            ForeColor = Color.White
        };

        button.Location = new Point(
            (this.ClientSize.Width - buttonWidth) / 2,
            startY + (index * verticalSpacing)
        );

        button.Click += (sender, e) => OpenLoginForm(text);
        return button;
    }

    private void RecenterButtons()
    {
        Button[] buttons = { attendeeLoginButton, organizerLoginButton, adminLoginButton, vendorLoginButton };
        foreach (Button button in buttons)
        {
            button.Left = (this.ClientSize.Width - button.Width) / 2;
        }
    }

    private void OpenLoginForm(string loginType)
    {
        Form loginForm = null;
        switch (loginType)
        {
            case "Attendee Login":
                loginForm = new AttendeeLoginForm();
                break;
            case "Organizer Login":
                loginForm = new OrganizerLoginForm();
                break;
            case "Admin Login":
                loginForm = new AdminLoginForm();
                break;
            case "Vendor Login":
                loginForm = new VendorLoginForm();
                break;
        }

        if (loginForm != null)
        {
            this.Hide();
            loginForm.FormClosed += (s, args) => this.Show();
            loginForm.Show();
        }
    }

    private void InitializeComponent()
    {
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoBox
            // 
            this.logoBox.Image = global::EventVerse.Resource1.download;
            this.logoBox.Location = new System.Drawing.Point(3, 0);
            this.logoBox.Click += new System.EventHandler(this.logoBox_Click);
            // 
            // HomePageForm
            // 
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "HomePageForm";
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);

    }

    private void logoBox_Click(object sender, EventArgs e)
    {

    }
}
