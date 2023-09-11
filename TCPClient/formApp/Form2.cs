using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace formApp
{
    public partial class Form2 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        ServiceReference4.Service1Client wcf_service;
        //private TcpClient client;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            wcf_service = new ServiceReference4.Service1Client();
            this.lblDateTime.Text = DateTime.Now.ToString();
        }

        private void addAllLogs(DataTable TBlogs, String order)
        {
            if (order.Equals("activityLogs"))
            {
                listView1.Items.Clear();
                try
                {
                    foreach (DataRow row in TBlogs.Rows)
                    {
                        //Add Item to ListView.
                        string mId = row["LogDateTime"].ToString();
                        string mName = row["LogUserEmail"].ToString();
                        string mStatus = row["LogActivity"].ToString();
                        string[] rowItem = { mId, mName, mStatus };
                        ListViewItem item = new ListViewItem(rowItem);
                        //item.BackColor = mStatus.Equals("1") ? Color.GreenYellow : Color.PaleVioletRed;   //change row backcolor based on status
                        listView1.Items.Add(item);
                    }
                    listView1.View = View.Details;
                }
                catch (Exception ex)
                {
                    lblDateTime.Text = "dataTable failed: " + ex.Message;
                    return;
                }
            }
            else if (order.Equals("auditLogs"))
            {
                listView2.Items.Clear();
                try
                {
                    foreach (DataRow row in TBlogs.Rows)
                    {
                        //Add Item to ListView.
                        string Etime = row["EventTime"].ToString();
                        string Etype = row["EventType"].ToString();
                        string SPN = row["ServerPrincipalName"].ToString();
                        string IP = row["IPAddress"].ToString();
                        string addInfo = row["AdditionalInfo"].ToString();
                        string[] rowItem = { Etime, Etype, SPN, IP, addInfo };
                        ListViewItem item = new ListViewItem(rowItem);
                        //item.BackColor = mStatus.Equals("1") ? Color.GreenYellow : Color.PaleVioletRed;   //change row backcolor based on status
                        listView2.Items.Add(item);
                    }
                    listView2.View = View.Details;
                }
                catch (Exception ex)
                {
                    lblDateTime.Text = "dataTable failed: " + ex.Message;
                    return;
                }
            }

        }
        private void listView1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            int index = listView1.FocusedItem.Index;
            ListViewItem selectedItem = listView1.Items[index];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
        }

        private void btnTimerSet_Click(object sender, EventArgs e)
        {
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnSignOut_Click(object sender, EventArgs e)
        {
            wcf_service.stopTcp();
            try
            {
                if (client != null)
                {
                    // Close the network stream and the client
                    client.Close();
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    await this.tcpCommandAsync("disconnect");
                }
                else
                {
                    // Handle the case where the client is not connected
                    lblDateTime.Text = "Client not initialized";
                }
            }
            catch (Exception ex)
            {
                lblDateTime.Text = "Error: " + ex.Message;
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            await this.tcpCommandAsync("connectServer");
        }

        public async Task tcpCommandAsync(string command)
        {
            try
            {
                string[] splitCommand;
                string ipAddress = txtHost.Text;
                int port = Convert.ToInt32(txtIp.Text);
                //empty string
                if (txtHost.Text.Equals("") || txtIp.Text.Equals(""))
                {
                    lblDateTime.Text = "Please enter a valid IP address and Port";
                    return;
                }
                client = new TcpClient();
                await client.ConnectAsync(ipAddress, port);
                if (!client.Connected)
                {
                    lblDateTime.Text = "Please enter correct server IP address and port that is available";
                    return;
                }
                stream = client.GetStream();

                // Send data to the server
                string message = command;
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);

                if (command.Contains('-'))
                {
                    splitCommand = command.Split('-');
                    message = splitCommand[0];
                }
                string response = "";
                string response2 = "";
                switch (message)
                {
                    case "connectServer":
                        // Receive a response from the server
                        response = await this.ReceiveDataAsync(stream);
                        if (response.Equals("Hello from the server!"))
                        {
                            txtLogin.ReadOnly = false;
                            btnLogin.Enabled = true;
                            lblDateTime.Text = "Server Response: " + response;
                        }
                        else
                        {
                            lblDateTime.Text = "Invalid IP address or port, please try again";
                        }
                        break;
                    case "login":
                        // Receive a response from the server
                        //response2 = await this.ReceiveDataAsync(stream);
                        response = await this.ReceiveDataTableAsync(stream);
                        if (response.Equals("Wrong email or password"))
                        {
                            lblDateTime.Text = "Server Response: " + response;
                        }
                        else
                        {
                            try
                            {
                                // Parse the received string array
                                string[] dataArray = response.Split(';');
                                //lblLoginError.Text = dataArray[0];
                                DataTable receivedDataTable = this.ConvertStringArrayToDataTable(dataArray, "activityLogs"); // Convert the XML data back to a DataTable
                                txtHost.ReadOnly = true;
                                txtIp.ReadOnly = true;
                                btnConnect.Enabled = false;
                                btnSignOut.Enabled = false;
                                btnLogOut.Enabled = true;
                                lblDateTime.Text = "";
                                btnLogin.Enabled = false;
                                txtLogin.ReadOnly = true;
                                btnQuery.Enabled = true;
                                lblLoginError.Text = "Login Successful";
                                this.addAllLogs(receivedDataTable, "activityLogs");
                            }
                            catch (Exception ex)
                            {
                                lblDateTime.Text = ex.Message;
                            }

                        }
                        break;
                    case "getAuditLogin":
                        // Receive a response from the server
                        response = await this.ReceiveDataTableAsync(stream);

                        try
                        {
                            // Parse the received string array
                            string[] auditDataArray = response.Split(';');
                            //lblLoginError.Text = dataArray[0];
                            DataTable receivedDataTable = this.ConvertStringArrayToDataTable(auditDataArray, "auditLogins");
                            this.addAllLogs(receivedDataTable, "auditLogs");
                        }
                        catch (Exception ex)
                        {
                            lblDateTime.Text = ex.Message;
                        }


                        break;
                    case "disconnect":
                        // Receive a response from the server
                        response = await this.ReceiveDataAsync(stream);
                        if (response.Equals("Disconnected"))
                        {
                            txtLogin.ReadOnly = true;
                            btnLogin.Enabled = false;
                            btnQuery.Enabled = true;
                            txtHost.Text = "";
                            txtIp.Text = "";
                            // Optionally, you can reset UI elements or perform other actions here
                            lblDateTime.Text = "Disconnected from the server";
                        }
                        break;
                }

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                txtLogin.ReadOnly = true;
                btnLogin.Enabled = false;
                lblDateTime.Text = "Error: " + ex.Message;
            }
        }

        //netstat -aon
        // Convert a string array back to a DataTable
        private DataTable ConvertStringArrayToDataTable(string[] dataArray, string dtType)
        {
            DataTable dataTable = new DataTable();
            if (dtType.Equals("activityLogs"))
            {
                // Create a DataTable with appropriate columns
                dataTable.Columns.Add("LogDateTime");
                dataTable.Columns.Add("LogUserEmail");
                dataTable.Columns.Add("LogActivity");
                // Add columns to dataTable as needed
                foreach (string dataRow in dataArray)
                {
                    string[] values = dataRow.Split(',');
                    if (values.Length == 3) // Ensure the array has all three values
                    {
                        dataTable.Rows.Add(values);
                    }
                }

            }
            else if (dtType.Equals("auditLogins"))
            {
                dataTable.Columns.Add("EventTime");
                dataTable.Columns.Add("EventType");
                dataTable.Columns.Add("ServerPrincipalName");
                dataTable.Columns.Add("IPAddress");
                dataTable.Columns.Add("AdditionalInfo");
                // Add columns to dataTable as needed
                foreach (string dataRow in dataArray)
                {
                    string[] values = dataRow.Split(',');
                    if (values.Length == 5) // Ensure the array has all three values
                    {
                        dataTable.Rows.Add(values);
                    }
                }
            }
            return dataTable;
        }
        //private bool IsPortOpen(string ip, int port)
        //{
        //    try
        //    {
        //        using (TcpClient tcpClient1 = new TcpClient())
        //        {
        //            tcpClient1.Connect(IPAddress.Parse(ip), port);
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblDateTime.Text = "Connection failed: " + ex.Message;
        //        return false;
        //    }
        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Equals(""))
            {
                lblLoginError.Text = "Please enter email and password";
                return;
            }
            try
            {
                string[] splitString = txtLogin.Text.Split('-');
                string email = splitString[0];
                string password = splitString[1];
                if (IsValidEmail(email) == true)
                {
                    this.tcpCommandAsync("login-" + txtLogin.Text);
                }
                else
                {
                    lblLoginError.Text = "Please enter valid email address";
                }
            }
            catch (Exception ex)
            {
                lblLoginError.Text = "Wrong email or password";
            }
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        // Receive a DataTable as binary data from the server
        private async Task<string> ReceiveDataAsync(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string a = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return a;
        }

        private async Task<string> ReceiveDataTableAsync(NetworkStream stream)
        {
            try
            {
                // Read the length of the data as a 4-byte integer
                byte[] lengthBytes = new byte[4];
                await stream.ReadAsync(lengthBytes, 0, 4);
                int dataLength = BitConverter.ToInt32(lengthBytes, 0);

                // Read the data in chunks and reconstruct the long string
                byte[] buffer = new byte[dataLength];
                int bytesRead = 0;
                int totalBytesRead = 0;

                while (totalBytesRead < dataLength)
                {
                    bytesRead = await stream.ReadAsync(buffer, totalBytesRead, dataLength - totalBytesRead);
                    if (bytesRead <= 0)
                    {
                        // Handle the case where not all data is received
                        break;
                    }
                    totalBytesRead += bytesRead;
                }

                // Convert the received data back to a string
                string longString = Encoding.UTF8.GetString(buffer);
                return longString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtLogin.ReadOnly = false;
            btnLogin.Enabled = true;
            txtLogin.Text = "";
            listView1.Items.Clear();
            listView2.Items.Clear();
            lblLoginError.Text = "Log Out Successfully";
            lblDateTime.Text = "";
            txtIp.ReadOnly = false;
            txtHost.ReadOnly = false;
            btnLogOut.Enabled = false;
            btnConnect.Enabled = true;
            btnSignOut.Enabled = true;
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            await this.tcpCommandAsync("getAuditLogin");
        }
    }
}
