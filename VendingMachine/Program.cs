

using System;
using System.Text;

namespace VendingMachine
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            var runProgram = true;

            while (runProgram)
            {
                DisplayMenu();


            }
         

        }


        public static string DisplayMenu()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("*******************Vending Machine*********************");
            stringBuilder.AppendLine("1. List available products");
            stringBuilder.AppendLine("2. Add product");
            stringBuilder.AppendLine("3. Show available change");
            stringBuilder.AppendLine("4. Add change");
            stringBuilder.AppendLine("5. Buy Product");
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }

        public static bool SelectOption()
        {
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                {
                    return false;
                }
                case "2":
                {
                    return false;
                }
                case "3":
                {
                    return false;
                }
                case "4":
                {
                    return false;
                }
                case "5":
                {
                    return false;
                }
                default:
                {
                    Console.WriteLine("Please choose a valid option from the menu.");
                    return false;
                }

            }
        }
    }
}
