using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SharedLibrary
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public string Name { get; }

        [DataMember]
        public double Winnings { get; set; }

        public Player(string name)
        {
            Name = name;
            Winnings = 0.0;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Winnings: {Winnings}e";
        }
    }
}
