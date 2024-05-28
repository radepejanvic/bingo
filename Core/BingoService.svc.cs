using SharedLibrary;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml.Linq;

namespace Core
{
    public class BingoService : IPlayer, IMachine
    {
        delegate void MessageArrivedDelegate(string message);

        static Dictionary<string, Player> players = new Dictionary<string, Player>();
        static Dictionary<string, Ticket> tickets = new Dictionary<string, Ticket>();
        static Dictionary<string, MessageArrivedDelegate> notifiers = new Dictionary<string, MessageArrivedDelegate>();

        static bool firstRound = true;

        public void InitPlayer(string name, Ticket ticket)
        {
            if (!players.ContainsKey(name))
            {
                players.Add(name, new Player(name));
                tickets.Add(name, ticket);
                notifiers.Add(name, OperationContext.Current.GetCallbackChannel<ICallBack>().MessageArrived);
                SendMessage(name, $"SUCCESS: User and ticket registered! \n\t{players[name]} \n\t{ticket}");
            } else if (!tickets.ContainsKey(name))
            {
                tickets.Add(name, ticket);
                SendMessage(name, $"SUCCESS: Ticket registered! \n\t{ticket}");
            } else
            {
                SendMessage(name, "WARNING: Ticket already exists in this round.");
            }
        }

        public void StartRound()
        {
            if (firstRound)
            {
                BroadcastMessage($"\n-- THE GAME HAS STARTED PLACE YOUR BETS --\n");
                firstRound = false;
                return;
            }

            var numbers = GenerateNumbers(2);
            int matches;
            Player player;

            BroadcastMessage($"BINGO: [ {numbers[0]}, {numbers[1]} ]");

            var rankings = RankPlayers();
            foreach (var ticket in tickets)
            {
                matches = Compare(numbers, ticket.Value.Numbers);
                player = players[ticket.Key];
                player.Winnings += CalculatePrize(matches, ticket.Value.Deposit);

                SendMessage(ticket.Key,
                    $"\tRanking: {rankings.Values.ToList().IndexOf(player)+1}," +
                    $" Mathces: {matches}, Winnings: {players[ticket.Key].Winnings}e");
            }

            BroadcastMessage($"\n-- NEW ROUND HAS STARTED PLACE YOUR BETS --\n");
            BroadcastMessage($"SCOREBOARD: \n{GetScoreboard()}");
            tickets.Clear();

        }

        private int[] GenerateNumbers(int size)
        {
            var numbers = new int[size];
            numbers[0] = CryptoRandom.GetRandomInt(10);

            while (true)
            {
                numbers[1] = CryptoRandom.GetRandomInt(10);
                if (numbers[1] != numbers[0]) { return numbers; } 
            }
        }

        private int Compare(int[] bingo, int[] ticket)
        {
            int matches = 0;
            foreach (int value in bingo)
            {
                if (Array.IndexOf(ticket, value) != -1)
                    matches++;
            }
            return matches;
        }

        private double CalculatePrize(int matches, double deposit)
        {
            switch(matches)
            {
                case 0: return -deposit;
                case 1: return deposit;
                case 2: return 5 * deposit;
                default: return 0;
            }
        }

        private string GetScoreboard()
        {
            var scoreboard = new StringBuilder();

            var counter = 1;
            foreach (var player in RankPlayers().Values)
            {
                scoreboard.AppendLine($"\t{counter}. {player.ToString()}");
                counter++;
            }

            return scoreboard.ToString();
        }

        private Dictionary<string, Player> RankPlayers()
        {
            return players.Values
                .OrderByDescending(player => player.Winnings)
                .ToDictionary(player => player.Name, player => player);
        }

        private void SendMessage(string name, string message)
        {
            notifiers[name]?.Invoke(message);
        }

        private void BroadcastMessage(string message)
        {
            foreach(var notifier in notifiers.Values)
            {
                notifier?.Invoke(message);
            }
        }
    }
}
