using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void logout();

        [OperationContract]
        void StartListener();


        [OperationContract]
        string checkLoggedInUser();

        [OperationContract] 
        DataTable getAllLogs();

        [OperationContract]
        string InsertUserDetails(string email, string pass);

        [OperationContract]
        string CheckUser(string email, string pass);

        [OperationContract]
        DataTable getAllUsers();

        [OperationContract]
        string UpdateUser(string email, string password, int id);

        [OperationContract]
        string DeleteUser(int id);

        [OperationContract]
        string CreateMachine(string machineName);

        [OperationContract]
        string UpdateMachineName(string machineName, int id);

        [OperationContract]
        string DeleteMachine(int id);

        [OperationContract]
        DataTable getAllMachine();

        //[OperationContract]
        //void writeLogs(string info);

        //[OperationContract]
        //string sendData(string response);

        //[OperationContract]
        //string sendDataTable(DataTable dt);

        //[OperationContract]
        //string[] ConvertDataTableToStringArray(DataTable dt);

        //[OperationContract]
        //void changeTimer(int seconds);

        //[OperationContract]
        //string UpdateMachinStatusWithDelay(int mId, string delay, string finalDelay);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class UserDetails
    //{
    //    string email = string.Empty;
    //    string password = string.Empty;

    //    [DataMember]
    //    public string Password
    //    {
    //        get { return password; }
    //        set { password = value; }
    //    }

    //    [DataMember]
    //    public string Email
    //    {
    //        get { return email; }
    //        set { email = value; }
    //    }
    //}


    class Program
    {
        static void Main(string[] args)
        {

            //Uri baseAddress = new Uri("http://localhost:52597/Service1.svc");
            //ServiceHost host = new ServiceHost(typeof(Service1), baseAddress);
            Service1 client = new Service1();
            client.StartListener();
            //try
            //{
            //    // Add a service endpoint
            //    //host.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(), "http://localhost:52597/Service1.svc");

            //    // Enable metadata publishing (for testing purposes)
            //    ServiceMetadataBehavior smb = new ServiceMetadataBehavior
            //    {
            //        HttpGetEnabled = true
            //    };
            //    //host.Description.Behaviors.Add(smb);

            //    // Open the service host
            //    host.Open();
                
            //    Console.WriteLine("Service is running. Press Enter to exit.");
            //    Console.ReadLine();
            //}
            ////A binding instance has already been associated to listen URI 'http://localhost:52597/Service1.svc'.
            ////If two endpoints want to share the same ListenUri, they must also share the same binding object instance.
            ////The two conflicting endpoints were either specified in AddServiceEndpoint() calls, in a config file,
            ////or a combination of AddServiceEndpoint() and config. 
            //catch (Exception ex)
            //{
            //    Console.WriteLine("An error occurred: " + ex.Message);
            //    host.Abort();
            //}
            //finally
            //{
            //    // Close the service host
            //    host.Close();
            //}
        }
    }





}
