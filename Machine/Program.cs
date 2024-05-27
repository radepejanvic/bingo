using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ServiceModel;
using Machine.ServiceReference;

namespace Machine
{

    internal class Program
    {
        static MachineClient machineClient = new MachineClient();

        static void Main(string[] args)
        {

            while (true)
            {
                machineClient.StartRound();
                Thread.Sleep(60 * 1000);
            }
        }
    }
}
