using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        SqlConnection sqlConn = new SqlConnection();

        public string InsertUserDetails(string email, string password)
        {
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            SqlCommand chkUsr = new SqlCommand("checkEmail", this.sqlConn);
            chkUsr.CommandType = CommandType.StoredProcedure;
            chkUsr.Parameters.Add("@userEmail", email);
            if ((int)chkUsr.ExecuteScalar() > 0) //email has been registered
            {
                return "Please use a different email";
            }
            else //email is unique
            {
                sqlConn.Close();
                return this.sqlProcedure("register", email, password);
            }
        }
        public string CheckUser(string email, string password)
        {
            return this.sqlProcedure("login", email, password);
        }

        public DataTable getAllUsers()
        {
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            
            SqlDataAdapter adapter = new SqlDataAdapter(); 
            adapter.SelectCommand = new SqlCommand("getAllUsers", this.sqlConn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            adapter.Fill(ds, "userTable");
            DataTable dt = ds.Tables["userTable"];
            return dt;
        }

        public string UpdateUser(string email, string password, int id)
        {
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
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
            }
            return message;
        }

        public string DeleteUser(int id)
        {
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            SqlCommand chkUsr = new SqlCommand("deleteUser", this.sqlConn);
            chkUsr.CommandType = CommandType.StoredProcedure;
            chkUsr.Parameters.Add("@userID", id);
            chkUsr.ExecuteNonQuery();
            return "User Deleted Successfully";
        }
        public string sqlProcedure(string procedureName, string userEmail, string userPassword)
        {
            //LAPTOP - 90E9I307
            string message="";
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            string cmd = procedureName.Equals("login") ? "checkUser" : "createUser";  //two procedures
            SqlCommand sqlCmd = new SqlCommand(cmd, this.sqlConn);  //call stored procedure and add input into parameter 
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@userEmail", userEmail);
            sqlCmd.Parameters.Add("@userPassword", userPassword);
            if (cmd.Equals("checkUser"))
            {
                message = (int)sqlCmd.ExecuteScalar() > 0 ? "Login Successful" : "Invalid email or password, Pls try again.";
            }
            else if (cmd.Equals("createUser"))
            {
                int result = sqlCmd.ExecuteNonQuery();
                
                message = result==1? "Registration completed":"Registration failed";
            }
            sqlConn.Close();
            return message;
        }

        ///////////////////Machine///////////////////////////
        public string CreateMachine(string name)
        {
            string message;
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            SqlCommand chkM = new SqlCommand("checkMachine", this.sqlConn);
            chkM.CommandType = CommandType.StoredProcedure;
            chkM.Parameters.Add("@mName", name);
            if ((int)chkM.ExecuteScalar() > 0) //email is unique
            {
                return "Please use a different machine name";
            }
            else ////email has been registered
            {
                SqlCommand addM = new SqlCommand("addMachine", this.sqlConn);
                addM.CommandType = CommandType.StoredProcedure;
                addM.Parameters.Add("@mName", name);
                message = addM.ExecuteNonQuery() == 1 ? "Machine Created Successfully" : "Failed to create machine";
                return message;
            }
        }
        public string UpdateMachine(string machineName, int id)
        {
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
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
            }
            return message;
        }
        public string DeleteMachine(int id)
        {
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            SqlCommand chkUsr = new SqlCommand("deleteMachine", this.sqlConn);
            chkUsr.CommandType = CommandType.StoredProcedure;
            chkUsr.Parameters.Add("@mId", id);
            chkUsr.ExecuteNonQuery();
            return "Machine Deleted Successfully";
        }

        public DataTable getAllMachine()
        {
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("getAllMachine", this.sqlConn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            adapter.Fill(ds, "machineTable");
            DataTable dt = ds.Tables["machineTable"];
            return dt;
        }

        public string UpdateMachinStatusWithDelay(int mId, string delay, string finalDelay)
        {
            int retrievedStatus = int.Parse(this.getMachineStatus(mId));
            string message;
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            int updatedMStatus = retrievedStatus == 1 ? 2 : 1;
            SqlCommand statusCmd = new SqlCommand("updateMachineStatus", this.sqlConn);
            statusCmd.CommandType = CommandType.StoredProcedure;
            statusCmd.Parameters.Add("@mId", mId);
            statusCmd.Parameters.Add("@mStatus", updatedMStatus);
            statusCmd.Parameters.AddWithValue("@delay", delay);
            int result = statusCmd.ExecuteNonQuery(); 
            if (result == 1)
            {
                SqlCommand chkStatus = new SqlCommand("checkUpdatedMachineStatus", this.sqlConn);
                chkStatus.CommandType = CommandType.StoredProcedure;
                chkStatus.Parameters.Add("@mId", mId);
                chkStatus.Parameters.Add("@mStatus", updatedMStatus);
                chkStatus.Parameters.Add("@delay", finalDelay);
                message = (int)chkStatus.ExecuteScalar() > 0 ? "Successfully Updated" : "Update not completed";
            }
            else
            {
                message = "Update Failed";
            }
            return message;
        }
        public string getMachineStatus(int mId)
        {
            string message;
            this.sqlConn.ConnectionString = "Server=SAMUELHAN; Database=newDb; User Id=sa; Password=sasa;";
            sqlConn.Open();
            string result="";
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
