using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Player.ServiceReference;
using SharedLibrary;

namespace Player
{
    public class Callback : IPlayerCallback
    {
        public void MessageArrived(string message)
        {
            Console.WriteLine(message);
        }
    }

    internal class Program
    {
        static PlayerClient playerClient;

        static void Main(string[] args)
        {
            InstanceContext ic = new InstanceContext(new Callback());
            playerClient = new PlayerClient(ic);

            string name = ConsoleReader.ReadName();

            while (true)
            {
                var numbers = ConsoleReader.ReadNumbers();
                var deposit = ConsoleReader.ReadDeposit();

                playerClient.InitPlayer(name, new Ticket(numbers, deposit));

                Thread.Sleep(10 * 1000); 
                Console.ReadKey();
            }
        }

    }
}
