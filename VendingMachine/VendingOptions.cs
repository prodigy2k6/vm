

using NDesk.Options;

namespace VendingMachine
{
    public class VendingOptions
    {
        public bool Showhelp { get; set; }
        public bool AddProduct { get; set; }
        public bool ListProducts { get; set; }
        public bool BuyItem { get; set; }

        public static OptionSet CreateOptionSet(VendingOptions arguments)
        {
            var options = new OptionSet()
            {
                {"help","Show help",  x => arguments.Showhelp = true },
                {"AddProduct", "Adds new product", x => arguments.AddProduct = true },
                {"ListProducts", "Shows available products", x => arguments.ListProducts = true },
                {"BuyItem", "Buy Item from Vending Machine", x => arguments.BuyItem = true }
            };

            return options;
        }
    }
}
