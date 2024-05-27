using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    public static class ConsoleReader
    {

        public static string ReadName()
        {
            Console.WriteLine("Enter your username.");
            Console.Write("Username: ");

            return Console.ReadLine();
        }

        public static int[] ReadNumbers()
        {
            int[] numbers = new int[2];
            int count = 0;

            Console.WriteLine("Enter your 2 number combination.");

            while (count < 2)
            {
                Console.Write($"Number {count + 1}: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number) && number >= 1 && number <= 10)
                {
                    if (count == 1 && number == numbers[0])
                    {
                        Console.WriteLine("WARNING: The numbers must be different!");
                    }
                    else
                    {
                        numbers[count] = number;
                        count++;
                    }
                }
                else
                {
                    Console.WriteLine("WARNING: Numbers have to be in range 1 - 10!");
                }
            }

            return numbers;
        }

        public static double ReadDeposit()
        {
            double deposit = 0;
            bool validInput = false;

            Console.WriteLine("Enter the deposit amount.");

            while (!validInput)
            {
                Console.Write("Deposit: ");
                string input = Console.ReadLine();
                if (double.TryParse(input, out deposit) && deposit > 0)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("WARNING: Deposit has to be a positive value!");
                }
            }

            return deposit;
        }





    }
}
