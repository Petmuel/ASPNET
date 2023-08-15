using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
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
        string UpdateMachine(string machineName, int id);

        [OperationContract]
        string DeleteMachine(int id);

        [OperationContract]
        DataTable getAllMachine();

        [OperationContract]
        string UpdateMachinStatusWithDelay(int mId, string delay, string finalDelay);
        
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

    
}
