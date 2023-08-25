using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Timers;

namespace WebApplication1
{
    public partial class machine : System.Web.UI.Page
    {
        private ServiceReference2.Service1Client _client;
        protected void Page_Load(object sender, EventArgs e)
        {
            _client = new ServiceReference2.Service1Client();
            if (!IsPostBack)
            {
                if (_client.checkLoggedInUser().Equals(""))
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Please login first" + "');", true);
                    Response.Redirect("~/login.aspx");
                    return;
                }
                this.AddRowToTable();
                UpdateTimestamp();
            }
        }
        private void UpdateTimestamp()
        {
            Label1.Text = DateTime.Now.ToString();
            this.AddRowToTable();
        }
        protected void WebAppTimerTick(object sender, EventArgs e)
        {
            UpdateTimestamp();

            //updatePanel1.Update();
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
                this.deleteMachineModal.Style["display"] = "block";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DeleteMachineModal').modal(); ", true);
            }
            else if (e.CommandName.Equals("EditMachine"))
            {
                this.txtEditMachineName.Text = machineName;
                this.lblMachineId.Text = machineId;
                editMachineModal.Style["display"] = "block";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#EditMachineModal').modal(); ", true);
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
            tableMachineData = _client.getAllMachine();
            if (tableMachineData.Rows.Count==0)
            {
                this.lblTableHeader.Text = "No Machine Created";
            }
            this.MachineGridView.DataSource = tableMachineData;
            this.MachineGridView.DataBind();  
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string returnString;
            string machineName = Request.Form["mName"];
            if (machineName.Equals(""))
            {
                this.lblCreateMachineError.Text = "Please enter a machine name";
            }
            else
            {
                returnString = _client.CreateMachine(machineName);
                if (!returnString.Equals("Please use a different machine name"))
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + returnString + "');", true);
                    // Set the hidden field value to false to hide the popup
                    createMachineModal.Style["display"] = "none";
                    //this.lblCreateResult.Text = returnString;
                    this.AddRowToTable();
                }
                else
                {
                    this.lblCreateMachineError.Text = returnString;
                    ClientScript.RegisterStartupScript(this.GetType(), "CloseModal", "closeModalWithFade('createMachineModal');", true);
                }
            }
        }
        protected void btnShowPopup_Click(object sender, EventArgs e)
        {
            createMachineModal.Style["display"] = "block";
        }
        protected void btnCloseDeleteMachinePopup_Click(object sender, EventArgs e)
        {
            this.deleteMachineModal.Style["display"] = "none";
            this.lblCreateMachineError.Text = "";
        }
        protected void btnCloseCreateMachinePopup_Click(object sender, EventArgs e)
        {
            this.createMachineModal.Style["display"] = "none";

            //this.createMachineModal.Visible = false;
            this.lblCreateMachineError.Text = "";
        }
        protected void btnCloseEditMachinePopup_Click(object sender, EventArgs e)
        {
            this.editMachineModal.Style["display"] = "none";
            this.lblEditMachineError.Text = "";
        }
        protected void EditMachine_Click(object sender, EventArgs e)
        {
            string returnString;
            string updatedMachineName = txtEditMachineName.Text;
            int machineId = Convert.ToInt32(this.lblMachineId.Text);
            if(updatedMachineName.Equals(""))
            {
                this.lblEditMachineError.Text = "Invalid input, please enter a machine name";
                return;
            }
            returnString = _client.UpdateMachineName(updatedMachineName, machineId);
            if (returnString.Equals("This machine name existed"))
            {
                this.lblEditMachineError.Text = returnString;
                return;
            }
            else
            {
                this.txtEditMachineName.Text = "";
                this.lblEditMachineError.Text = "";
                editMachineModal.Style["display"] = "none";
                this.AddRowToTable();
            }
            //this.lblCreateResult.Text = returnString;
            
        }
        protected void DeleteMachine_Click(object sender, EventArgs e)
        {
            //string returnString;
            int machineId = Convert.ToInt32(this.lblDeleteMachineId.Text);
            _client.DeleteMachine(machineId);
            //this.lblCreateResult.Text = returnString;
            this.deleteMachineModal.Style["display"] = "none";
            this.AddRowToTable();
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/homepage.aspx");
        }

        protected void timerSet_Click(object sender, EventArgs e)
        {
            if (txtTimerSet.Text.Equals(""))
            {
                this.lblTimerError.Text = "Please enter seconds";
            }
            else
            {
                this.lblTimerError.Text = "";
                int seconds = (Convert.ToInt32(txtTimerSet.Text) * 1000);
                timer.Interval = seconds;
            }
            //_client.changeTimer(seconds);
        }
    }
}