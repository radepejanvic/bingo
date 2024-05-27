using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class CryptoRandom
    {
        public static int GetRandomInt(int cap)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4];
                rng.GetBytes(randomNumber);
                var randomInt = BitConverter.ToInt32(randomNumber, 0) & int.MaxValue;
                return randomInt % (cap + 1);
            }
        }
    }
}
