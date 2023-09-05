using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Timers;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;

namespace WcfService
{
    
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string V = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
        public Timer _timer = new Timer (10000);
        SqlConnection sqlConn = new SqlConnection();
        private int randIndex;
        public string userEmailLoggedIn="";
        private DataTable machines = new DataTable();
        //private static TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
        private TcpListener listener;
        //private bool isListenerRunning = false;
        // Static constructor 
        private bool isRunning = false;
        public Service1()
        {
            this.sqlConn.ConnectionString = V;
            if (isRunning==false)
            {
                this.StartListener();
            }
            this.machines = this.getAllMachine();

            if (this.machines.Rows.Count > 0)
            {
                if (this._timer==null||this._timer.Enabled)
                {
                    Random rnd = new Random();
                    this.randIndex = rnd.Next(0, this.getAllMachine().Rows.Count);
                    if (this._timer != null)
                    {
                        this._timer.Dispose();
                        this._timer.Elapsed += null;
                        this._timer.Stop();
                        this._timer=null;
                    }
                    this.InitializeTimer();
                }
                else
                {
                    Random rnd = new Random();
                    this.randIndex = rnd.Next(0, this.machines.Rows.Count);
                    this.InitializeTimer();
                }
            }
        }

        private void StartListener()
        {
            string response = "";
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
                listener.Start();
                isRunning = true;

                while (isRunning)
                {
                    //Waiting for a connection...
                    TcpClient client = listener.AcceptTcpClient();
                    //client connected

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        string[] splitString = { "", "", "" };
                        string command = data;
                        Console.WriteLine($"Received: {data}");
                        if (data.Contains('-'))
                        {
                            splitString = data.Split('-');
                            command = splitString[0];
                        }
                        switch (command)
                        {
                            case "connectServer":
                                response = "Hello from the server!";
                                this.SendData(stream, response);
                                //SendDataTable(stream, this.getAllLogs());
                                break;
                            case "login":
                                string email = splitString[1];
                                string password = splitString[2]; 
                                if(this.CheckUser(email, password).Equals("Login Successful"))
                                {
                                    DataTable dt = this.getAllLogs();
                                    // Convert the DataTable to XML
                                    string[] responseArray = this.ConvertDataTableToStringArray(dt);
                                    response = string.Join(";", responseArray);
                                    this.SendData(stream, response);
                                }
                                else
                                {
                                    response = "Wrong email or password";
                                    this.SendData(stream, response);
                                }
                                break;
                            case "disconnect":
                                response = "Server disconnected.";
                                break;
                            case "getLogs":
                                
                                break;
                        }
                        
                    }
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error service: " + ex.Message);
            }
            finally
            {
                isRunning = false; // Server is no longer running
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }
        
        // Convert a DataTable to a string array
        private string[] ConvertDataTableToStringArray(DataTable dataTable)
        {
            // Convert the DataTable to a string array
            List<string> dataList = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                string[] values = row.ItemArray.Select(x => x.ToString()).ToArray();
                string rowString = string.Join(",", values);
                dataList.Add(rowString);
            }
            return dataList.ToArray();
        }
        private void SendData(NetworkStream stream, String response)
        {
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            stream.Write(responseData, 0, responseData.Length);
        }
       
        public void stopTcp()
        {
            isRunning = false;
            listener.Stop();
        }

        public void InitializeTimer()
        {
            this._timer.Elapsed += TimerElapsed;
            this._timer.AutoReset = false;
            this._timer.Start();
        }

        public void TimerElapsed(object sender, ElapsedEventArgs e)
        {
                this.sqlConn.Close();
                this.sqlConn.ConnectionString = V;
                this.sqlConn.Open();
                DataRow row = this.machines.Rows[this.randIndex];
                int id = Convert.ToInt32(row["MachineID"]);
                int status = Convert.ToInt32(row["MachineStatus"]);

                // Set flag to prevent concurrent updates
                this.UpdateMachineStatusRand(status, id);
            
        }
      
        public void login(string email)
        {
            SqlCommand login = new SqlCommand("changeUserLoginEmail", this.sqlConn);
            login.CommandType = CommandType.StoredProcedure;
            login.Parameters.Add("@email", email);
            login.ExecuteNonQuery();
        }
        public void logout()
        {
            this.sqlConn.Open();
            string activity = "Admin (" + this.checkLoggedInUser() + ") signed out successfully";
            this.logActivity(activity);
            SqlCommand login = new SqlCommand("changeUserLoginEmail", this.sqlConn);
            login.CommandType = CommandType.StoredProcedure;
            login.Parameters.Add("@email", "");
            login.ExecuteNonQuery();
            this.sqlConn.Close();
        }

        public DataTable getAllLogs()
        {
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(); 
            adapter.SelectCommand = new SqlCommand("getAllLogs", this.sqlConn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            adapter.Fill(ds, "logTable");
            DataTable dt = ds.Tables["logTable"];
            sqlConn.Close();
            return dt;
        }
        public string checkLoggedInUser()
        {
            this.sqlConn.Close();
            this.sqlConn.Open();
            string email;
            SqlCommand login = new SqlCommand("getUserLogin", this.sqlConn);
            login.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = login.ExecuteReader();
            reader.Read();
            email = reader["UserEmail"].ToString();
            this.sqlConn.Close();
            this.sqlConn.Open();
            return email;
        }
        public string InsertUserDetails(string email, string password)
        {
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlCommand chkUsr = new SqlCommand("checkEmail", this.sqlConn);
            chkUsr.CommandType = CommandType.StoredProcedure;
            chkUsr.Parameters.Add("@userEmail", email);
            if ((int)chkUsr.ExecuteScalar() > 0) //email has been registered
            {
                sqlConn.Close();
                return "Please use a different email";
            }
            else //email is unique
            {
                sqlConn.Close();
                return this.sqlProcedure("register", email, password);
            }
            sqlConn.Close();
        }

        public void logActivity(string activity)
        {
            SqlCommand addLog = new SqlCommand("addLog", this.sqlConn);
            addLog.CommandType = CommandType.StoredProcedure;
            addLog.Parameters.Add("@logEmail", this.checkLoggedInUser());
            addLog.Parameters.Add("@logDateTime", DateTime.Now.ToString());
            addLog.Parameters.Add("@logActivity", activity);
            addLog.ExecuteNonQuery();
        }
        public string CheckUser(string email, string password)
        {
            return this.sqlProcedure("login", email, password);
        }

        public DataTable getAllUsers()
        {
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(); 
            adapter.SelectCommand = new SqlCommand("getAllUsers", this.sqlConn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            adapter.Fill(ds, "userTable");
            DataTable dt = ds.Tables["userTable"];
            sqlConn.Close();
            return dt;
        }

        public string UpdateUser(string email, string password, int id)
        {
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlCommand chkUsr = new SqlCommand("checkEmail", this.sqlConn);
            chkUsr.CommandType = CommandType.StoredProcedure;
            chkUsr.Parameters.Add("@userEmail", email);

            string message;
            if ((int)chkUsr.ExecuteScalar() > 0) //email is not unique
            {
                message = "This email has been registered";
            }
            else //email is unique
            {
                SqlCommand sqlCmd = new SqlCommand("editUser", this.sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@userEmail", email);
                sqlCmd.Parameters.Add("@userPassword", password);
                sqlCmd.Parameters.Add("@userId", id);
                int result = sqlCmd.ExecuteNonQuery();
                message = result == 1 ? "Successfully Updated" : "Update Failed";
                string activity = "Admin (" + this.checkLoggedInUser() + ") updated a user detail (new email: " + email + " | new password: " + password + ")";
                this.logActivity(activity);
            }
            sqlConn.Close();
            return message;
        }

        public string DeleteUser(int id)
        {
            sqlConn.Open();
            SqlCommand chkUsr = new SqlCommand("deleteUser", this.sqlConn);
            chkUsr.CommandType = CommandType.StoredProcedure;
            chkUsr.Parameters.Add("@userID", id);
            chkUsr.ExecuteNonQuery();
            string activity = "Admin (" + this.checkLoggedInUser() + ") deleted a user (userID: " + id + ")";
            this.logActivity(activity);
            sqlConn.Close();
            return "User Deleted Successfully";
        }
        public string sqlProcedure(string procedureName, string userEmail, string userPassword)
        {
            //LAPTOP - 90E9I307
            string message="";
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            string cmd = procedureName.Equals("login") ? "checkUser" : "createUser";  //two procedures
            SqlCommand sqlCmd = new SqlCommand(cmd, this.sqlConn);  //call stored procedure and add input into parameter 
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@userEmail", userEmail);
            sqlCmd.Parameters.Add("@userPassword", userPassword);
            if (cmd.Equals("checkUser"))
            {
                if ((int)sqlCmd.ExecuteScalar() > 0)
                {
                    this.login(userEmail);
                    this.userEmailLoggedIn = userEmail;
                    if (!userEmail.Equals("ttt@gmail.com")) // admin in TCPCLient
                    {
                        string activity = "Admin (" + userEmail + ") logged in successfully";
                        this.logActivity(activity);
                    }
                    message = "Login Successful";
                }
                else
                {
                    message = "Invalid email or password, Pls try again.";
                }
            }
            else if (cmd.Equals("createUser"))
            {
                int result = sqlCmd.ExecuteNonQuery();
                if (result == 1)
                {
                    string activity = "Admin (" + this.checkLoggedInUser() + ") registered a new user (email: " + userEmail + " | password: " + userPassword + ")";
                    this.logActivity(activity);
                    message = "Registration completed";
                }
                else
                {
                    message = "Registration failed";
                }
            }
            sqlConn.Close();
            return message;
        }

        ///////////////////Machine///////////////////////////
        public string CreateMachine(string name)
        {
            string message;
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlCommand chkM = new SqlCommand("checkMachine", this.sqlConn);
            chkM.CommandType = CommandType.StoredProcedure;
            chkM.Parameters.Add("@mName", name);
            if ((int)chkM.ExecuteScalar() > 0) //machine name has been registered
            {
                sqlConn.Close();
                return "Please use a different machine name";
            }
            else ////machine name is unique
            {
                SqlCommand addM = new SqlCommand("addMachine", this.sqlConn);
                addM.CommandType = CommandType.StoredProcedure;
                addM.Parameters.Add("@mName", name);
                message = addM.ExecuteNonQuery() == 1 ? "Machine Created Successfully" : "Failed to create machine";
                string activity = "Admin (" + this.checkLoggedInUser() + ") created a new machine (Machine Name: " + name + ")";
                this.logActivity(activity);
                sqlConn.Close();
                return message;
            }
        }
        public void UpdateMachineStatusRand(int status, int id)
        {
            Random rStatus = new Random();
            int newStatus = rStatus.Next(1, 3);
            SqlCommand upMS = new SqlCommand("updateMachineStatus", this.sqlConn);
            upMS.CommandType = CommandType.StoredProcedure;
            upMS.Parameters.Add("@mStatus", newStatus);
            upMS.Parameters.Add("@mId", id);
            upMS.ExecuteNonQuery();
            this.sqlConn.Close();
            //this.OnUpdateCompleted();
        }
        public string UpdateMachineName(string machineName, int id)
        {    
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlCommand chkM = new SqlCommand("checkMachine", this.sqlConn);
            chkM.CommandType = CommandType.StoredProcedure;
            chkM.Parameters.Add("@mName", machineName);

            string message;
            if ((int)chkM.ExecuteScalar() > 0) //machineName is not unique
            {
                message = "This machine name existed";
            }
            else //machineName is unique
            {
                SqlCommand sqlCmd = new SqlCommand("editMachine", this.sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@mName", machineName);
                sqlCmd.Parameters.Add("@mId", id);
                int result = sqlCmd.ExecuteNonQuery();
                message = result == 1 ? "Successfully Updated" : "Update Failed";
                string activity = "Admin (" + this.checkLoggedInUser() + ") updated a machine (New Machine Name: " + machineName + ")";
                this.logActivity(activity);
            }
            sqlConn.Close();
            return message;
        }
        public string DeleteMachine(int id)
        {
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlCommand chkUsr = new SqlCommand("deleteMachine", this.sqlConn);
            chkUsr.CommandType = CommandType.StoredProcedure;
            chkUsr.Parameters.Add("@mId", id);
            chkUsr.ExecuteNonQuery();
            string activity = "Admin (" + this.checkLoggedInUser() + ") deleted a machine (Machine ID: " + id + ")";
            this.logActivity(activity);
            sqlConn.Close();
            return "Machine Deleted Successfully";
        }

        public DataTable getAllMachine()
        {
            //System.Threading.Thread.Sleep(3000);
            this.sqlConn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("getAllMachine", this.sqlConn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            adapter.Fill(ds, "machineTable");
            DataTable dt = ds.Tables["machineTable"];
            this.sqlConn.Close();
            return dt;
        }

        //public string UpdateMachinStatusWithDelay(int mId, string delay, string finalDelay)
        //{
        //    i nt retrievedStatus = int.Parse(this.getMachineStatus(mId));
        //    string message;
        //    this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
        //    sqlConn.Open();
        //    int updatedMStatus = retrievedStatus == 1 ? 2 : 1;
        //    SqlCommand statusCmd = new SqlCommand("updateMachineStatus", this.sqlConn);
        //    statusCmd.CommandType = CommandType.StoredProcedure;
        //    statusCmd.Parameters.Add("@mId", mId);
        //    statusCmd.Parameters.Add("@mStatus", updatedMStatus);
        //    statusCmd.Parameters.AddWithValue("@delay", delay);
        //    int result = statusCmd.ExecuteNonQuery(); 
        //    if (result == 1)
        //    {
        //        SqlCommand chkStatus = new SqlCommand("checkUpdatedMachineStatus", this.sqlConn);
        //        chkStatus.CommandType = CommandType.StoredProcedure;
        //        chkStatus.Parameters.Add("@mId", mId);
        //        chkStatus.Parameters.Add("@mStatus", updatedMStatus);
        //        chkStatus.Parameters.Add("@delay", finalDelay);
        //        message = (int)chkStatus.ExecuteScalar() > 0 ? "Successfully Updated" : "Update not completed";
        //    }
        //    else
        //    {
        //        message = "Update Failed";
        //    }
        //    return message;
        //}
        public string getMachineStatus(int mId)
        {
            string message;
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            string result = "";
            SqlCommand getStatusCmd = new SqlCommand("getMachineStatus", this.sqlConn);
            getStatusCmd.CommandType = CommandType.StoredProcedure;
            getStatusCmd.Parameters.Add("@mId", mId);
            SqlDataReader reader = getStatusCmd.ExecuteReader();
            if (reader.HasRows) 
            {
                reader.Read();
                result = reader["MachineStatus"].ToString(); // Retrieve the attribute value
            }
            sqlConn.Close();
            return result;
        }

    }
}
