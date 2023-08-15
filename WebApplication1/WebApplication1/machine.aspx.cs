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
    public partial class machine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AddRowToTable();
            }
        }

        protected void machineTable_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument); // Get the row index
            GridViewRow row = MachineGridView.Rows[rowIndex]; // Get all data from row
            string machineId = row.Cells[0].Text;
            string machineName = row.Cells[1].Text;
            string machineStatus = row.Cells[2].Text;
            if (e.CommandName.Equals("DeleteMachine"))
            {
                this.lblDeleteMachineId.Text = machineId;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DeleteMachineModal').modal(); ", true);
            }
            else if (e.CommandName.Equals("EditMachine"))
            {
                this.txtEditMachineName.Text = machineName;
                this.lblMachineId.Text = machineId;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#EditMachineModal').modal(); ", true);
            }
            else
            {
                return;
            }
        }
        private void AddRowToTable()
        {
            this.lblTableHeader.Text = "All Machines";
            DataTable tableMachineData = new DataTable();
            tableMachineData.Columns.AddRange(new DataColumn[3] { new DataColumn("MachineID", typeof(int)),
                            new DataColumn("MachineName", typeof(string)),
                            new DataColumn("MachineStatus",typeof(string)),});

            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            tableMachineData = client.getAllMachine();
            if (tableMachineData.Rows.Count==0)
            {
                this.lblTableHeader.Text = "No Machine Created";
            }
            this.MachineGridView.DataSource = tableMachineData;
            this.MachineGridView.DataBind();
        }
        protected void btnCreateMachine_Click(object sender, EventArgs e)
        {
            string returnString;
            string machineName = Request.Form["mName"];
            Console.WriteLine(machineName);
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            returnString = client.CreateMachine(machineName);
            if (returnString.Equals("Please use a different machine name"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + returnString + "');", true);
                return;
            }
            this.lblCreateResult.Text = returnString;
            this.AddRowToTable();
        }

        protected void EditMachine_Click(object sender, EventArgs e)
        {
            string returnString;
            string updatedMachineName = txtEditMachineName.Text;
            int machineId = Convert.ToInt32(this.lblMachineId.Text);
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            returnString = client.UpdateMachine(updatedMachineName, machineId);
            if (returnString.Equals("This machine name existed"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + returnString + "');", true);
                return;
            }
            this.lblCreateResult.Text = returnString;
            this.AddRowToTable();
        }
        protected void DeleteMachine_Click(object sender, EventArgs e)
        {
            string returnString;
            int machineId = Convert.ToInt32(this.lblDeleteMachineId.Text);
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            returnString = client.DeleteMachine(machineId);
            this.lblCreateResult.Text = returnString;
            this.AddRowToTable();
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/homepage.aspx");
        }
    }
}