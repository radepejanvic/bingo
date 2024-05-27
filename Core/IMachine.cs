using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [ServiceContract]
    public interface IMachine
    {
        [OperationContract(IsOneWay = true)]
        void StartRound();
    }
}
