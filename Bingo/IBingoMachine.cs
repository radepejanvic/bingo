using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Bingo
{
    [ServiceContract (CallbackContract = typeof(ICallBack))]
    public interface IBingoMachine
    {
        [OperationContract (IsOneWay = true)]
        void GenerateNumbers();

        [OperationContract(IsOneWay = true)]
        void InitPlayer();
    }

    public interface ICallBack
    {
        [OperationContract (IsOneWay = true)]
        void NumbersRecieved(int[] numbers);
    }
}
