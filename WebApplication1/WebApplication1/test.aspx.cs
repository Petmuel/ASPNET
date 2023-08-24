using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class test : System.Web.UI.Page
    {
        private ServiceReference2.Service1Client _client;
        protected void Page_Load(object sender, EventArgs e)
        {
            _client = new ServiceReference2.Service1Client();
            if (!IsPostBack)
            {
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
        private void AddRowToTable()
        {
            DataTable tableMachineData = new DataTable();
            tableMachineData.Columns.AddRange(new DataColumn[3] { new DataColumn("MachineID", typeof(int)),
                            new DataColumn("MachineName", typeof(string)),
                            new DataColumn("MachineStatus",typeof(string)),});
            tableMachineData = _client.getAllMachine();
            if (tableMachineData.Rows.Count == 0)
            {
                this.lblTableHeader.Text = "No Machine Created";
            }
            this.MachineGridView.DataSource = tableMachineData;
            this.MachineGridView.DataBind();
        }
    }
}