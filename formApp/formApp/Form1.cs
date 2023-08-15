using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void loginBtn_Click(object sender, EventArgs e)
        {
            ServiceReference4.Service1Client client = new ServiceReference4.Service1Client();
            string returnString;
            returnString = client.CheckUser(this.emailTxtBox.Text, this.pwTxtBox.Text);
            //returnString = client.InsertUserDetails(this.emailTxtBox.Text, this.pwTxtBox.Text);
            if (returnString.Equals("Login Successful"))
            {
                Form2 Check = new Form2();
                Check.Show();
                Hide();
            }
            else
            {
                MessageBox.Show(returnString, "Login Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pwTxtBox_TextChanged(object sender, EventArgs e)
        {
            pwTxtBox.PasswordChar = '*';
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
