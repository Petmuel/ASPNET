using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplication1
{
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        private System.Threading.Timer timer;
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }   
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string returnString;
            string userEmail = Request.Form["email"];
            string userPassword = Request.Form["password"];
            if (userEmail.Equals("") || userPassword.Equals(""))
            {
                this.Label1.Text="Invalid Input. Please enter correct email and password";
            }
            else
            {
                ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
                returnString = client.CheckUser(userEmail, userPassword);
                //returnString = client.InsertUserDetails(this.emailTxtBox.Text, this.pwTxtBox.Text);
                if (returnString.Equals("Login Successful"))
                {
                    Response.Redirect("~/homepage.aspx");
                }
                else
                {
                    this.Label1.Text=returnString;
                }
            }
        }
        private void DisplayTextForFewSeconds(string message)
        {
            this.Label1.Text = message;
            timer.Change(3000, System.Threading.Timeout.Infinite);
        }

        private void errorMessage(string message)
        {
            System.Timers.Timer timer = new System.Timers.Timer(3000);
            timer.Enabled = true;
            timer.Elapsed += WebAppTimerTick;
        }

        protected void WebAppTimerTick(object sender, EventArgs e)
        {
            this.Label1.Text = "Invalid input, please try again";
        }
    }
}
