using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class homePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AddRowToTable();
            }
        }

        protected void userTable_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument); // Get the row index
            GridViewRow row = GridView1.Rows[rowIndex]; // Get all data from row
            string userId = row.Cells[0].Text;  
            string email = row.Cells[1].Text;
            string password = row.Cells[2].Text;
            if (e.CommandName.Equals( "DeleteUser"))
            {
                this.lblDeleteUserId.Text = userId;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DeleteUserModal').modal(); ", true);
            }
            else if (e.CommandName.Equals("EditUser"))
            {
                this.txtEditUserEmail.Text = email;
                this.txtEditUserPassword.Text = password;
                this.lblUserId.Text = userId;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#EditUserModal').modal(); ", true);
            }
            else
            {
                return;
            }
        }
        private void AddRowToTable()
        {
            this.lblTableHeader.Text = "All Users";
            DataTable tableUserData = new DataTable();
            tableUserData.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                            new DataColumn("Email", typeof(string)),
                            new DataColumn("Password",typeof(string)),});
            
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            tableUserData = client.getAllUsers();
            if (tableUserData.Rows.Count == 0)
            {
                this.lblTableHeader.Text = "No User Registered";
            }
            this.GridView1.DataSource = tableUserData;
            this.GridView1.DataBind();
        }
        protected void btnRegisterUser_Click(object sender, EventArgs e)
        {
            string returnString;
            string userEmail = Request.Form["email"];
            string userPassword = Request.Form["password"];
            Console.WriteLine(userEmail, userPassword);
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            returnString = client.InsertUserDetails(userEmail, userPassword);
            if(returnString.Equals("Please use a different email"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + returnString + "');", true);
                return;
            }
            this.lblRegisterResult.Text = returnString;
            this.AddRowToTable();
        }

        protected void EditRow(object sender, EventArgs e)
        {
            string returnString;
            string updatedUserEmail = txtEditUserEmail.Text;
            string updateUserPassword = txtEditUserPassword.Text;
            int userId = Convert.ToInt32(this.lblUserId.Text);
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            returnString = client.UpdateUser(updatedUserEmail, updateUserPassword, userId);
            if (returnString.Equals("This email has been registered"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + returnString + "');", true);
                return;
            }
            this.lblRegisterResult.Text = returnString;
            this.AddRowToTable();
        }
        protected void DeleteRow(object sender, EventArgs e)
        {
            string returnString; 
            int userId = Convert.ToInt32(this.lblDeleteUserId.Text);
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            returnString = client.DeleteUser(userId);
            this.lblRegisterResult.Text = returnString;
            this.AddRowToTable();
        }

        protected void SignOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }

        protected void MachineNavigate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/machine.aspx");
        }
    }
}