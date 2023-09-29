using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace formApp
{
    public partial class Form2 : Form
    {
        private TcpClient client = new TcpClient();
        private NetworkStream stream;
        ServiceReference4.Service1Client wcf_service;
        private ListViewItem lvItem;
        private string machineID = ConfigurationManager.AppSettings["MachineID"];
        private string machinePassword = ConfigurationManager.AppSettings["MachinePassword"];
        private string machineName = ConfigurationManager.AppSettings["MachineName"];
        private string machineStatus = ConfigurationManager.AppSettings["MachineStatus"];
        private string serverIP = ConfigurationManager.AppSettings["serverIP"];
        private string serverPort = ConfigurationManager.AppSettings["serverPort"];
        private string machineIP = GetLocalIPAddress();
        private Timer statusTimer;
        private Timer connectServerTimer;
        private int interval = 5000;
        private int failedUpdateStatus = 0;
        private Boolean isLogin = false;
        private Boolean isConnectedToServer = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtInterval.Text = this.interval/1000+"";
            this.tcpClientConnectAsync();
            wcf_service = new ServiceReference4.Service1Client();
            lblMachineID.Text = "MachineID: " + this.machineID;
            lblMachine_password.Text = "Machine Password: " + this.machinePassword;
            lblMachineName.Text = "Machine Name: " + this.machineName;
            lblMachineStatus.Text = "Machine Status: " + this.machineStatus;
            this.initializeTimer();
            this.lblDateTime.Text = DateTime.Now.ToString();
        }
        private void initializeTimer()
        {
            statusTimer = new Timer();
            statusTimer.Interval = this.interval;
            statusTimer.Tick += new EventHandler(t_Tick);
            connectServerTimer = new Timer();
            connectServerTimer.Interval = 2000;
            connectServerTimer.Tick += new EventHandler(serverConnect_tick);
            connectServerTimer.Start();
        }
        private async void serverConnect_tick(object sender, EventArgs e)
        {
            try
            {
                this.addLogs("Client Send", "Connect To Server");
                this.writeLog("Client Send", "Connect To Server");

                // if the ip address/port is invalid
                if (!this.client.Connected)
                {
                    this.tcpClientConnectAsync();
                }
                else
                {
                    await this.tcpCommandAsync("connectServer-"+this.machineID, this.client);
                    if (isConnectedToServer == true)
                    {
                        this.connectServerTimer.Stop();
                        await this.tcpCommandAsync("MachineLogin-" + this.machineID + "-" + this.machinePassword + "-" + this.machineIP, this.client);
                        if (isLogin == true)
                        {
                            this.statusTimer.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.isConnectedToServer = false;
                this.addLogs("Error", ex.Message);
                this.writeLog("Error", ex.Message);
                //label1.Text = ex.Message;
                if (ex.Message.Contains("asynchronous operation is in progress"))
                {
                    //await client.ConnectAsync(this.serverIP, int.Parse(this.serverPort));
                    this.client.Dispose();
                }
            }
            return;
        }

        private async void tcpClientConnectAsync()
        {
            try
            {
                this.client = new TcpClient();
                await this.client.ConnectAsync(this.serverIP, Convert.ToInt32(this.serverPort));
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    // The target machine actively refused the connection.
                    this.addLogs("Error", "Connection Failed (Server is inactive, or the server's IP address or port is invalid)");
                    this.writeLog("Error", "Connection Failed (Server is inactive, or the server's IP address or port is invalid)");
                }
                else
                {
                    // Handle other socket exceptions.
                    this.addLogs("Error", "Connection Failed: " + ex.Message);
                    this.writeLog("Error", "Connection Failed: " + ex.Message);
                }
            }
        }

        private void t_Tick(object sender, EventArgs e)
        {
            Random rStatus = new Random();
            int newStatus = rStatus.Next(1, 3);
            string result = this.heartbeat("" + newStatus);
            if (result.Equals("true"))
            {
                this.machineStatus = ConfigurationManager.AppSettings["MachineStatus"];
                lblMachineStatus.Text = "Machine Status: " + this.machineStatus;
                this.addLogs("Client Send", "Send new status: " + this.machineStatus);
                this.writeLog("Client Send", "Send new status: " + this.machineStatus);
                this.tcpCommandAsync("updateMachineStatus-" + this.machineID + "-" + this.machineStatus, this.client);
                this.lblDateTime.Text = DateTime.Now.ToString();
                this.label1.Text = failedUpdateStatus+""; 
            }
            else
            {
                this.lblDateTime.Text = result;
            }

        }
        private void btnTimerSet_Click(object sender, EventArgs e)
        {
        }

        private string heartbeat(string newStatus)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); 
                config.AppSettings.Settings["MachineStatus"].Value = newStatus;
                // Save the updated configuration
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return "true";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
        }
        
        private void addLogs(string comType, string eventInfo)
        {
            string[] rowItem = { DateTime.Now.ToString(), machineIP, comType, eventInfo };
            this.lvItem = new ListViewItem(rowItem);
            listView1.Items.Add(lvItem);
            listView1.EnsureVisible(listView1.Items.Count - 1);
        }
    
        private void listView1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            int index = listView1.FocusedItem.Index;
            ListViewItem selectedItem = listView1.Items[index];
        }

        public async Task tcpCommandAsync(string command, TcpClient client)
        {
            try
            {
                string[] splitCommand;
                stream = client.GetStream();
                string message = command + "-" + this.machineIP;    //connectServer-machineIP
                
                byte[] data = Encoding.UTF8.GetBytes(message); 
                await stream.WriteAsync(data, 0, data.Length);

                if (message.Contains('-'))
                {
                    splitCommand = command.Split('-');
                    message = splitCommand[0];
                }
                string response = "";
                switch (message)
                {
                    case "connectServer":
                        // Receive a response from the server
                        response = await this.ReceiveDataAsync(stream);
                        if (response.Equals("Hello from the server!"))
                        {
                            this.isConnectedToServer = true;
                            this.addLogs("Server Reply", response);
                            this.writeLog("Server Reply", response);
                            this.connectServerTimer.Stop();
                            //this.statusTimer.Start();
                            //lblDateTime.Text = "Server Response: " + response;
                        }
                        else
                        {
                            this.isConnectedToServer = false;
                            lblDateTime.Text = "Invalid IP address or port, please try again";
                        }
                        break;
                    case "MachineLogin":
                        // Receive a response from the server
                        //response2 = await this.ReceiveDataAsync(stream);
                        response = await this.ReceiveDataAsync(stream);
                        if (response.Equals("Machine ID Does Not Exist") || response.Contains("Incorrect Password for Machine ID: ") || response.Equals("Incorrect Machine ID and Password"))
                        {
                            this.isLogin = false;
                            this.isConnectedToServer = false;
                            this.addLogs("Server Reply", response);
                            this.writeLog("Server Reply", response);
                            this.connectServerTimer.Start();
                            //lblLoginError.Text = "Server Response: " + response;
                        }
                        else
                        {
                            this.isLogin = true;
                            this.addLogs("Server Reply", response);
                            this.writeLog("Server Reply", response);
                        }
                        break;
                    case "updateMachineStatus":
                        response = await this.ReceiveDataAsync(stream);
                        if (response.Contains("Failed"))
                        {
                            this.addLogs("Server Reply", "Failed to update machine status");
                            this.writeLog("Server Reply", "Failed to update machine status");
                            this.failedUpdateStatus += 1;
                            if (this.failedUpdateStatus == 20)
                            {
                                this.failedUpdateStatus = 0;
                                this.isLogin = false;
                                this.isConnectedToServer = false;
                                this.statusTimer.Stop();
                                this.connectServerTimer.Start();
                            }
                        }
                        else
                        {
                            this.failedUpdateStatus = 0;
                            this.addLogs("Server Reply", "Machine Status (" + this.machineStatus + ") is updated successfully");
                            this.writeLog("Server Reply", "Machine Status (" + this.machineStatus + ") is updated successfully");
                        }
                        break;
                }
                //client.Close();
            }
            catch (Exception ex)
            {
                lblDateTime.Text = "Error: " + ex.Message;
            }
        }
        public void writeLog(string comType, string eventInfo)
        {
            try
            {
                if (!eventInfo.Equals("")) 
                {
                    string filePath = "C:\\Users\\samue\\OneDrive\\Desktop\\ASPNET\\TCPClient\\logs\\MID" + this.machineID + "_LogFile_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine("[" + DateTime.Now + "] [" + comType + "] " + eventInfo + System.Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
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
            //wcf_service.StopTcp();
            try
            {
                this.interval = int.Parse(txtInterval.Text) * 1000;
                statusTimer.Interval = this.interval;
                lblIntervalError.Text = "New interval has been set!";
            }
            catch (Exception ex)
            {
                lblIntervalError.Text = "Please enter valid input";
            }
            //
        }
    }
}
