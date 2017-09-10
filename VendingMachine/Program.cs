using System;
using System.Text;

namespace VendingMachine
{
    public class Program
    {
        public static VendingMachineState vendingMachine = new VendingMachineState();

        static void Main(string[] args)
        {
            var endProgram = false;


            while (!endProgram)
            {
                Console.WriteLine(DisplayMenu());
                endProgram = SelectOption();

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
            stringBuilder.AppendLine("6. EXIT");
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }

        public static bool SelectOption()
        {
            Console.WriteLine("Choose Option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    {
                        Console.WriteLine(vendingMachine.GetAvailableProducts());
                        return false;
                    }
                case "2":
                    {
                        vendingMachine.AddProduct();
                        return false;
                    }
                case "3":
                    {
                        Console.WriteLine(vendingMachine.GetAvailableCash());
                        return false;
                    }
                case "4":
                    {
                        vendingMachine.addCoins();
                        return false;
                    }
                case "5":
                    {
                        vendingMachine.BuyItem();
                        return false;
                    }
                case "6":
                    {
                        return true;
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
