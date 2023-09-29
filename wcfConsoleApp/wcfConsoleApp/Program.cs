using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WcfService;

namespace wcfConsoleApp
{
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
        //void changeTimer(int seconds);

        //[OperationContract]
        //string UpdateMachinStatusWithDelay(int mId, string delay, string finalDelay);

    }
    public class YourClass
    {
        //private TcpListener listener;
        private Boolean isRunning = false;
        private Service1 service1Client = new Service1();
        static void Main(string[] args)
        {
            Service1 client = new Service1();
            try
            {
                client.StartListener();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}
