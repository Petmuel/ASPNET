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
    public partial class Form2 : Form
    {
        private BackgroundWorker worker;
        private Timer t;
        public Form2()
        {
            InitializeComponent();
            //this.loadingPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //this.loadingPictureBox.Visible = false;
            //worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWork;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.loadingPictureBox.Visible = false;
            this.addMachineRow();
            this.initializeTimer();
            this.lblDateTime.Text = DateTime.Now.ToString();
        }
        private void initializeTimer()
        {
            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
            
        }
        private void t_Tick(object sender, EventArgs e)
        {
            this.addMachineRow();
            this.lblDateTime.Text = DateTime.Now.ToString();
            t.Stop();
            initializeTimer();
        }
        private void addMachineRow()
        {
            DataTable tableMachineData = new DataTable();
            listView1.Items.Clear();
            ServiceReference4.Service1Client client = new ServiceReference4.Service1Client();
            tableMachineData = client.getAllMachine();
            foreach (DataRow row in tableMachineData.Rows)
            {
                //Add Item to ListView.
                string mId = row["MachineID"].ToString();
                string mName = row["MachineName"].ToString();
                string mStatus = row["MachineStatus"].ToString();
                string[] rowItem = { mId, mName, mStatus };
                ListViewItem item = new ListViewItem(rowItem);
                item.BackColor = mStatus.Equals("1") ? Color.GreenYellow : Color.PaleVioletRed;   //change row backcolor based on status
                listView1.Items.Add(item);
            }
            listView1.View = View.Details;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 Check = new Form1();
            Check.Show();
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private void btnConfirm_Click(object sender, EventArgs e)
        //{
        //    this.loadingPictureBox.Visible = true;
        //    this.textBoxButtonsDisabled(false);
        //    if (lblMachineId.Text.Equals(""))
        //    {
        //        MessageBox.Show("Machine is not selected", "Please selecte a machine to udpate its status",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        this.textBoxButtonsDisabled(true);
        //        this.loadingPictureBox.Visible = false;
        //    }
        //    else if (this.ValidateContact() == false)
        //    {
        //        MessageBox.Show("Invalid Input", "Please enter the number of seconds",
        //           MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        this.textBoxButtonsDisabled(true);
        //        this.loadingPictureBox.Visible = false;
        //    }
        //    else
        //    {
        //        worker.RunWorkerAsync();
        //    }
        //}
        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    string returnString;
        //    int id = int.Parse(lblMachineId.Text);
        //    int finalSeconds = Int32.Parse(txtFinalTimer.Text);
        //    int seconds = Int32.Parse(txtTimer.Text);

        //    ServiceReference4.Service1Client client = new ServiceReference4.Service1Client();

        //    TimeSpan time = TimeSpan.FromSeconds(seconds);
        //    TimeSpan finalTime = TimeSpan.FromSeconds(finalSeconds);
        //    returnString = client.UpdateMachinStatusWithDelay(id, time.ToString(@"hh\:mm\:ss"), finalTime.ToString(@"hh\:mm\:ss"));

        //    if (returnString.Equals("Successfully Updated"))
        //    {
        //        this.Invoke((Action)(() =>{this.addMachineRow();}));
        //        MessageBox.Show(returnString, "Machine Status Updated Successfully",
        //        MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show(returnString, "Update failed, please try again",
        //        MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    loadingPictureBox.Visible = false;
        //    this.textBoxButtonsDisabled(true);
        //}
        private bool ValidateContact()
        {
            int i;
            return int.TryParse(this.txtTimer.Text, out i);
        }
        //private void textBoxButtonsDisabled (bool isEnable)
        //{
        //    if (isEnable == false)
        //    {
        //        this.btnConfirm.Enabled = false;
        //        this.btnCancel.Enabled = false;
        //        this.txtFinalTimer.Enabled = false;
        //        this.txtTimer.Enabled = false;
        //    }
        //    else if(isEnable == true)
        //    {
        //        this.btnConfirm.Enabled = true;
        //        this.btnCancel.Enabled = true;
        //        this.txtFinalTimer.Enabled = true;
        //        this.txtTimer.Enabled = true;
        //    }

        //}
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void txtFinalTimer_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            int index = listView1.FocusedItem.Index;
            
            ListViewItem selectedItem = listView1.Items[index];
            //this.lblMachineId.Text = selectedItem.Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.addMachineRow();
        }

        private void btnTimerSet_Click(object sender, EventArgs e)
        {
            if (txtTimer.Text.Equals(""))
            {
                MessageBox.Show("INvalid input", "Please enter a timer",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (this.ValidateContact() == false)
            {
                MessageBox.Show("Invalid Input", "Please enter the number of seconds",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int seconds = Convert.ToInt32(txtTimer.Text);
                t.Interval = seconds * 1000;
                t.Start();
            }
        }
    }
}
