using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;

namespace Core
{
    

    [ServiceContract(CallbackContract = typeof(ICallBack))]
    public interface IPlayer
    {
        [OperationContract(IsOneWay = true)]
        void InitPlayer(string name, Ticket ticket);
    }

    public interface ICallBack 
    {
        [OperationContract (IsOneWay = true)]
        void MessageArrived(string message);
    }
}
