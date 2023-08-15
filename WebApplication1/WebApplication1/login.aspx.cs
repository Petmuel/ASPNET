using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        private System.Threading.Timer timer;
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timer = new System.Threading.Timer(Timer_Tick, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string returnString;
            string userEmail = Request.Form["email"];
            string userPassword = Request.Form["password"];
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            returnString = client.CheckUser(userEmail, userPassword);
            //returnString = client.InsertUserDetails(this.emailTxtBox.Text, this.pwTxtBox.Text);
            if(returnString.Equals("Login Successful"))
            {
                this.DisplayTextForFewSeconds(returnString);
                Response.Redirect("~/homepage.aspx");
            }
            else
            {
                this.DisplayTextForFewSeconds(returnString);
            }
        }
        private void DisplayTextForFewSeconds(string message)
        {
            this.Label1.Text = message;
            timer.Change(3000, System.Threading.Timeout.Infinite);
        }
        private void Timer_Tick(object state)
        {
            this.Label1.Text = "";
        }
    }
}