using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuaSaudaraFinance
{
    public partial class LoginForm : Form
    {

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblUsername;
        private Label lblPassword;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Initialize an empty string to store the error messages
            string errorMessage = "";

            // Check if username is empty
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorMessage += "Username cannot be empty.\n";
            }

            // Check if password is empty
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorMessage += "Password cannot be empty.\n";
            }
            else
            {
                // Check if password is at least 6 characters long
                if (txtPassword.Text.Length < 6)
                {
                    errorMessage += "Password must be at least 6 characters long.\n";
                }
            }

            

            // If there are any errors, show the message box with all error messages
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage.TrimEnd('\n'), "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // Stop further processing
            }

            // Simple username/password validation
            if (txtUsername.Text == "admin" && txtPassword.Text == "password")
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
