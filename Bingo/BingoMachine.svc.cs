using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace Bingo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BingoMachine" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BingoMachine.svc or BingoMachine.svc.cs at the Solution Explorer and start debugging.
    public class BingoMachine : IBingoMachine
    {
        static ICallBack proxy;

        //static List<Player> players = new List<Player>();

        public void InitPlayer()
        {
            proxy = OperationContext.Current.GetCallbackChannel<ICallBack>();
        }

        public void GenerateNumbers()
        {
           
            proxy.NumbersRecieved(new int[4]);
            //return new int[4];
        }

        private int GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[length];
                randomNumberGenerator.GetBytes(randomBytes);

                return BitConverter.ToInt32(randomBytes, 0);
            }
        }

        private int GetRandomInteger(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue must be less than or equal to maxValue");

            int range = maxValue - minValue + 1;

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[4];
                rng.GetBytes(randomBytes);
                int randomValue = BitConverter.ToInt32(randomBytes, 0) & int.MaxValue; 

                int scaledValue = (int)((randomValue / (double)int.MaxValue) * range) + minValue;

                return scaledValue;
            }
        }


    }
}
