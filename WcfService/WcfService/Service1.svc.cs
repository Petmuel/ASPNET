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
using System.Threading;
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
        public System.Timers.Timer _timer = new System.Timers.Timer(1000);
        SqlConnection sqlConn = new SqlConnection(V);
        private int randIndex;
        public string userEmailLoggedIn="";
        private DataTable machines = new DataTable();
        private TcpListener listener;        private bool isRunning = false;
        public Service1()
        {
            this.machines = this.getAllMachine();
            StartListener();
        }
        private void TimerCallback(object state)
        {
            Console.WriteLine("hi");
            this.sqlConn.Close();
            this.sqlConn.ConnectionString = V;
            this.sqlConn.Open();
            DataRow row = this.machines.Rows[this.randIndex];
            int id = Convert.ToInt32(row["MachineID"]);
            int status = Convert.ToInt32(row["MachineStatus"]);

            // Set flag to prevent concurrent updates
            this.UpdateMachineStatusRand(status, id);
        }

        public void StartListener()
        {
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
            listener.Start();
            isRunning = true;

            Console.WriteLine("Server is listening for incoming connections...");

            while (isRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();

                    // Create a new thread to handle the client
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"SocketException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            string info = "";
            string MID = "";
            string clientIP = "";
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;
                string response = "";
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string email = "";
                    string MPassword = "";
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] splitString = { "", "", "" };
                    string command = data;
                    string result = "";
                    string checkUserResult = "";

                    if (data.Contains('-'))
                    {
                        splitString = data.Split('-');
                        command = splitString[0];
                    }

                    switch (command)
                    {
                        case "connectServer":
                            MID = splitString[1];
                            clientIP = splitString[2];
                            response = "Hello from the server!";
                            info = " [Client IP: " + clientIP + "] [Client Send]: Connection Request From Client (MID" + MID+")" + System.Environment.NewLine;
                            this.writeLog(info, MID);

                            result = this.SendData(stream, response);
                            if (result.Equals("true"))
                            {
                                info = " [Client IP: " + clientIP + "] [Server Reply]: Client Connected Successfully (MID" + MID + ")" + System.Environment.NewLine;
                            }
                            else
                            {
                                info = " [Client IP: " + splitString[1] + "] [Server Error Reply]: " + result + System.Environment.NewLine;
                            }
                            this.writeLog(info, MID);
                            break;
                        case "MachineLogin":
                            MID = splitString[1];
                            MPassword = splitString[2];
                            clientIP = splitString[3];
                            //check user exists or not
                            checkUserResult = this.checkMachine(MID, MPassword);
                            //this.writeLog(" [Client IP: " + clientIP + "] [Client Send]: Login Request - Check Existing User (Email: '" + email + "', Password: '" + password + "') " + System.Environment.NewLine);
                            if (checkUserResult.Equals("Login Successful"))
                            {
                                response = checkUserResult;
                                result = this.SendData(stream, response);
                                if (result.Equals("true"))
                                {
                                    info = " [Client IP: " + clientIP + "] [MachineService]: Machine (ID: " + MID + ") Logged In Successfully." + System.Environment.NewLine;
                                    this.writeLog(info, MID);
                                }
                                else
                                {
                                    info = " [Client IP: " + clientIP + "] [Server Error Reply]: Failed Sending Error Message: " + response + System.Environment.NewLine;
                                }
                            }
                            else
                            {
                                response = checkUserResult;
                                result = this.SendData(stream, response);
                                if (result.Equals("true"))
                                {
                                    info = " [Client IP: " + clientIP + "] [Server Reply]: Machine Login Failed (" + response + ")" + System.Environment.NewLine;
                                    this.writeLog(info, MID);
                                }
                                else
                                {
                                    info = " [Client IP: " + clientIP + "] [Server Error Reply]: Failed Sending Error Message: " + response + System.Environment.NewLine;
                                }
                            }
                            break;
                        case "updateMachineStatus":
                            MID = splitString[1];
                            int status = int.Parse(splitString[2]);
                            clientIP = splitString[3];
                            info = " [Client IP: " + clientIP + "] [Client Send]: Machine (ID: " + MID + ") Status Update As " + status + "." + System.Environment.NewLine;
                            this.writeLog(info, MID);
                            response = this.UpdateMachineStatus(status, MID);
                            if (response.Contains("Failed"))
                            {

                                if (this.SendData(stream, response).Equals("true"))
                                {
                                    info = " [Client IP: " + clientIP + "] [Server Reply]: " + response + System.Environment.NewLine;
                                    this.writeLog(info, MID);
                                }
                                else
                                {
                                    info = " [Client IP: " + clientIP + "] [Server Error Reply]: Failed Sending Error Message: " + response + System.Environment.NewLine;
                                }
                            }
                            else
                            {
                                if (this.SendData(stream, response).Equals("true"))
                                {
                                    info = " [Client IP: " + clientIP + "] [Server Reply]: " + response + System.Environment.NewLine;
                                    this.writeLog(info, MID);
                                }
                                else
                                {
                                    info = " [Client IP: " + clientIP + "] [Server Error Reply]: Failed Sending Error Message: " + response + System.Environment.NewLine;
                                }
                            }
                            break;
                    }

                }
            }
            catch (SocketException ex)
            {
                info = " [Client IP: " + clientIP + "] [Error] " + ex.Message + " (MID" + MID + ")" + System.Environment.NewLine;
                this.writeLog(info, MID);
            }
            catch (Exception ex)
            {
                info = " [Client IP: " + clientIP + "] [Error] " + ex.Message + " (MID" + MID + ")" + System.Environment.NewLine;
                this.writeLog(info, MID);
            }
            finally
            {
                client.Close();
                info = " [Client IP: " + clientIP + "] Client Disconnected (MID" + MID + ")" + System.Environment.NewLine;
                this.writeLog(info, MID);
                //Console.WriteLine("Client disconnected.");
            }
        }

        //public void StartListener()
        //{
        //    while (true)
        //    {
        //        string MID = "";
        //        string clientIP = "";
        //        listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
        //        string response = "";
        //        try
        //        {
        //            listener.Start();
        //            isRunning = true;

        //            while (isRunning)
        //            {
        //                //Waiting for a connection...
        //                TcpClient client = listener.AcceptTcpClient();
        //                //listener.AcceptSocket();
        //                NetworkStream stream = client.GetStream();
        //                byte[] buffer = new byte[1024];
        //                int bytesRead;

        //                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
        //                {
        //                    string info = "";
        //                    string email = "";
        //                    string MPassword = "";
        //                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        //                    string[] splitString = { "", "", "" };
        //                    string command = data;
        //                    string result = "";
        //                    string checkUserResult = "";
        //                    //Console.WriteLine($"Received: {data}");
        //                    if (data.Contains('-'))
        //                    {
        //                        splitString = data.Split('-');
        //                        command = splitString[0];
        //                    }
        //                    switch (command)
        //                    {
        //                        case "connectServer":
        //                            MID = splitString[1];
        //                            clientIP = splitString[2];
        //                            response = "Hello from the server!";
        //                            info = " [Client IP: " + clientIP + "] [Client Send]: Conenction Request From Client" + System.Environment.NewLine;
        //                            this.writeLog(info, MID);

        //                            result = this.SendData(stream, response);
        //                            if (result.Equals("true"))
        //                            {
        //                                info = " [Client IP: " + clientIP + "] [Server Reply]: Client Connected Successfully" + System.Environment.NewLine;
        //                            }
        //                            else
        //                            {
        //                                info = " [Client IP: " + splitString[1] + "] [Server Error Reply]: " + result + System.Environment.NewLine;
        //                            }
        //                            this.writeLog(info, MID);
        //                            break;
        //                        case "MachineLogin":
        //                            MID = splitString[1];
        //                            MPassword = splitString[2];
        //                            clientIP = splitString[3];
        //                            //check user exists or not
        //                            checkUserResult = this.checkMachine(MID, MPassword);
        //                            //this.writeLog(" [Client IP: " + clientIP + "] [Client Send]: Login Request - Check Existing User (Email: '" + email + "', Password: '" + password + "') " + System.Environment.NewLine);
        //                            if (checkUserResult.Equals("Login Successful"))
        //                            {
        //                                response = checkUserResult;
        //                                result = this.SendData(stream, response);
        //                                if (result.Equals("true"))
        //                                {
        //                                    info = " [Client IP: " + clientIP + "] [MachineService]: Machine (ID: " + MID + ") Logged In Successfully." + System.Environment.NewLine;
        //                                    this.writeLog(info, MID);
        //                                }
        //                                else
        //                                {
        //                                    info = " [Client IP: " + clientIP + "] [Server Error Reply]: Failed Sending Error Message: " + response + System.Environment.NewLine;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                response = checkUserResult;
        //                                result = this.SendData(stream, response);
        //                                if (result.Equals("true"))
        //                                {
        //                                    info = " [Client IP: " + clientIP + "] [Server Reply]: Machine Login Failed (" + response + ")" + System.Environment.NewLine;
        //                                    this.writeLog(info, MID);
        //                                }
        //                                else
        //                                {
        //                                    info = " [Client IP: " + clientIP + "] [Server Error Reply]: Failed Sending Error Message: " + response + System.Environment.NewLine;
        //                                }
        //                            }
        //                            break;
        //                        case "updateMachineStatus":
        //                            MID = splitString[1];
        //                            int status = int.Parse(splitString[2]);
        //                            clientIP = splitString[3];
        //                            info = " [Client IP: " + clientIP + "] [Client Send]: Machine (ID: " + MID + ") Status Update As " + status + "." + System.Environment.NewLine;
        //                            this.writeLog(info, MID);
        //                            response = this.UpdateMachineStatus(status, MID);
        //                            if (response.Contains("Failed")){
                                        
        //                                if (this.SendData(stream, response).Equals("true"))
        //                                {
        //                                    info = " [Client IP: " + clientIP + "] [Server Reply]: " + response + System.Environment.NewLine;
        //                                    this.writeLog(info, MID);
        //                                }
        //                                else
        //                                {
        //                                    info = " [Client IP: " + clientIP + "] [Server Error Reply]: Failed Sending Error Message: " + response + System.Environment.NewLine;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (this.SendData(stream, response).Equals("true"))
        //                                {
        //                                    info = " [Client IP: " + clientIP + "] [Server Reply]: " + response + System.Environment.NewLine;
        //                                    this.writeLog(info, MID);
        //                                }
        //                                else
        //                                {
        //                                    info = " [Client IP: " + clientIP + "] [Server Error Reply]: Failed Sending Error Message: " + response + System.Environment.NewLine;
        //                                }
        //                            }
        //                            break;
        //                    }
        //                }
        //                client.Close();
        //            }
        //        }
        //        catch (SocketException ex)
        //        {
        //            Console.WriteLine($"SocketException: {ex.Message}");
        //            //this.TCPClientDisconnect();
        //            // Add a delay before retrying
        //            Thread.Sleep(5000); // Adjust the delay as needed
        //        }
        //        catch (InvalidOperationException ex)
        //        {
        //            Console.WriteLine($"ObjectDisposedException: {ex.Message}");
        //            // Add a delay before retrying
        //            Thread.Sleep(5000); // Adjust the delay as needed
        //        }
        //        catch (Exception ex)
        //        {
        //            //Console.WriteLine("Error service: " + ex.Message);
        //            if(ex.Message.Equals("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host."))
        //            {
        //                this.writeLog(" [Client IP: " + clientIP + "] [Error] " + ex.Message + " (MID" + MID + ")", MID);
        //            }
        //        }
        //        finally
        //        {
        //            isRunning = false; // Server is no longer running
        //            if (listener != null)
        //            {
        //                listener.Stop();
        //            }
        //        }
        //    }
        //}
        //[DateNow] [Client IP] [Com Type] : [event info]
        public void writeLog(string eventInfo, string MID)
        {
            try
            {
                Console.WriteLine("[" + DateTime.Now + "]" + eventInfo);
                if (!eventInfo.Equals(""))
                {
                    string folderPath = Path.Combine(@"C:\\Users\\samue\\OneDrive\\Desktop\\ASPNET\\WcfService\\Log_down\\", "MID"+MID);

                    try
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            // If it doesn't exist, create it
                            Directory.CreateDirectory(folderPath);
                            //Console.WriteLine($"Folder for Machine {machineID} created successfully.");
                        }

                        string filePath = "C:\\Users\\samue\\OneDrive\\Desktop\\ASPNET\\WcfService\\Log_down\\MID" + MID + "\\MID" + MID + "_LogFile_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                        using (StreamWriter writer = File.AppendText(filePath))
                        {
                            writer.WriteLine("[" + DateTime.Now + "]" + eventInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating folder: {ex.Message}");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
            
        }

        // Convert a DataTable to a string array
        public string[] ConvertDataTableToStringArray(DataTable dataTable)      
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
        public string SendData(NetworkStream stream, String response)
        {
            try
            {
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public string SendDataTable(NetworkStream stream, String response)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(response);

                // Send the length of the data as a 4-byte integer
                byte[] lengthBytes = BitConverter.GetBytes(buffer.Length);
                stream.Write(lengthBytes, 0, lengthBytes.Length);

                // Send the data in chunks (e.g., 1024 bytes at a time)
                int offset = 0;
                int chunkSize = 1024;
                while (offset < buffer.Length)
                {
                    int remainingBytes = buffer.Length - offset;
                    int bytesToSend = Math.Min(chunkSize, remainingBytes);
                    stream.Write(buffer, offset, bytesToSend);
                    offset += bytesToSend;
                }
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void TCPClientDisconnect()
        {
            //if (true)
            //{
                //isRunning = false; // Stop the server loop
                if (listener != null)   
                {
                    listener.Stop(); // Stop listening for new connections
                    //listener.AcceptSocket();
                }
            //}
        }
        public void InitializeTimer()
        {
            this._timer.Elapsed += TimerElapsed;
            this._timer.Start();
        }

        public void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("hi");
            
            this.sqlConn.ConnectionString = V;
            this.sqlConn.Open();
            DataRow row = this.machines.Rows[this.randIndex];
            int id = Convert.ToInt32(row["MachineID"]);
            int status = Convert.ToInt32(row["MachineStatus"]);

            // Set flag to prevent concurrent updates
            this.UpdateMachineStatusRand(status, id);
            //this.sqlConn.Close();
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
        public DataTable getAllAuditLogs()
        {
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("getAuditLogs", this.sqlConn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            adapter.Fill(ds, "AuditlogTable");
            dataTable = ds.Tables["AuditlogTable"];
            sqlConn.Close();
            return dataTable;
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

        public string checkAppUser(string userEmail, string userPassword)
        {
            //LAPTOP - 90E9I307
            string message = "";
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlCommand checkEmail = new SqlCommand("checkEmail", this.sqlConn);  //call stored procedure and add input into parameter 
            checkEmail.CommandType = CommandType.StoredProcedure;
            checkEmail.Parameters.Add("@userEmail", userEmail);

            SqlCommand checkPassword = new SqlCommand("checkPassword", this.sqlConn);  //call stored procedure and add input into parameter 
            checkPassword.CommandType = CommandType.StoredProcedure;
            checkPassword.Parameters.Add("@userPassword", userPassword);
            int checkEmailResult = (int)checkEmail.ExecuteScalar();
            int checkPasswordResult = (int)checkPassword.ExecuteScalar();
            if (checkEmailResult > 0 && checkPasswordResult > 0)
            {
                message = "Login Successful";
            }
            else 
            {
                if (!(checkEmailResult > 0) && checkPasswordResult > 0)
                {
                    message = "Email Does Not Exist";
                }
                else if (checkEmailResult > 0 && !(checkPasswordResult > 0))
                {
                    message = "Incorrect Password";
                }
                else
                {
                    message = "Incorrect Email and Password";
                }
            }
            sqlConn.Close();
            return message;
        }

        public string checkMachine(string MID, string MPassword)
        {
            //LAPTOP - 90E9I307
            string message = "";
            this.sqlConn.ConnectionString = V;
            sqlConn.Open();
            SqlCommand checkM = new SqlCommand("checkMachine", this.sqlConn);  //call stored procedure and add input into parameter 
            checkM.CommandType = CommandType.StoredProcedure;
            checkM.Parameters.Add("@mID", MID);
           
            SqlCommand checkMPassword = new SqlCommand("checkMachinePassword", this.sqlConn);  //call stored procedure and add input into parameter 
            checkMPassword.CommandType = CommandType.StoredProcedure;
            checkMPassword.Parameters.Add("@mPassword", MPassword);
            int checkMachineIDResult = (int)checkM.ExecuteScalar();
            int checkMachinePasswordResult = (int)checkMPassword.ExecuteScalar();
            if (checkMachineIDResult > 0 && checkMachinePasswordResult > 0)
            {
                message = "Login Successful";
            }
            else
            {
                if (!(checkMachineIDResult > 0) && checkMachinePasswordResult > 0)
                {
                    message = "Machine ID Does Not Exist";
                }
                else if (checkMachineIDResult > 0 && !(checkMachinePasswordResult > 0))
                {
                    message = "Incorrect Password for Machine ID: " + MID;
                }
                else
                {
                    message = "Incorrect Machine ID and Password";
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
        public string StopTcp()
        {
            //isRunning = false;
            //listener.Stop();
            //this.StartListener();
            return "turn asd";
        }
        public string UpdateMachineStatus(int status, string mid)
        {
            this.sqlConn.ConnectionString = V;
            this.sqlConn.Open();
            SqlCommand upMS = new SqlCommand("updateMachineStatus", this.sqlConn);
            upMS.CommandType = CommandType.StoredProcedure;
            upMS.Parameters.Add("@mStatus", status);
            upMS.Parameters.Add("@mId", mid);

            // Add an output parameter to capture the result
            SqlParameter resultParam = new SqlParameter("@result", SqlDbType.Int);
            resultParam.Direction = ParameterDirection.Output;
            upMS.Parameters.Add(resultParam);

            upMS.ExecuteNonQuery();

            // Get the value of the @result parameter
            int result = Convert.ToInt32(upMS.Parameters["@result"].Value);

            string updateStatus = "";
            if (result == 1)
            {
                updateStatus = "Machine (ID: " + mid + ") Status Successfully Updated To " + status + ".";
            }
            else
            {
                updateStatus = "Machine (ID: " + mid + ") Status Update Failed";
            }

            this.sqlConn.Close();
            return updateStatus;
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
