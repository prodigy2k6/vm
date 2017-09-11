using System;
using System.Text;

namespace VendingMachine
{
    public class Program
    {
        public static VendingMachineState VendingMachine = new VendingMachineState(new VendingCash());

        static void Main()
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

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("*******************Vending Machine*********************");
            stringBuilder.AppendLine("1. List available products");
            stringBuilder.AppendLine("2. Add product");
            stringBuilder.AppendLine("3. Show available change");
            stringBuilder.AppendLine("4. Add change");
            stringBuilder.AppendLine("5. Buy Product");
            stringBuilder.AppendLine("6. Clear Products");
            stringBuilder.AppendLine("7. Clear Cash");
            stringBuilder.AppendLine("8. EXIT");
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
                        Console.WriteLine(VendingMachine.GetAvailableProducts());
                        return false;
                    }
                case "2":
                    {
                        VendingMachine.AddProduct();
                        return false;
                    }
                case "3":
                    {
                        Console.WriteLine(VendingMachine.GetAvailableCash());
                        return false;
                    }
                case "4":
                    {
                        VendingMachine.AddCoins();
                        return false;
                    }
                case "5":
                    {
                        VendingMachine.BuyItem();
                        return false;
                    }
                case "6":
                    {
                        VendingMachine.ClearExistingProducts();
                        return false;
                    }
                case "7":
                    {
                        VendingMachine.ClearExistingCash();
                        return false;
                    }
                case "8":
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
