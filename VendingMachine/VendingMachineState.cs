using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using VendingMachine.DTO;
using VendingMachine.DTO.Enums;
using VendingMachine.Exceptions;
using VendingMachine.Helpers;
using VendingMachine.Interfaces;

namespace VendingMachine
{
    public class VendingMachineState : IVendingMachineState
    {
        internal Dictionary<Product, int> Products { get; set; }
        internal IVendingCash Cash { get; set; }

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public VendingMachineState(IVendingCash myVendingCash)
        {
            Products = new Dictionary<Product, int>();
            Cash = myVendingCash;
        }

        public string GetAvailableProducts()
        {
            var stringBuilder = new StringBuilder();

            if (Products.Count == 0)
                return "No products in VendingMachine";

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("******************PRODUCT******************");

            foreach (var product in Products)
            {
                stringBuilder.AppendLine($"{product.Key} Count: {product.Value}");
            }

            return stringBuilder.ToString();
        }

        public string GetAvailableCash()
        {
            return Cash.ShowAvailableCash();
        }

        public void AddCoins()
        {
            const string format = "'Name' 'Amount'";
            Console.WriteLine("Available Denominations: ");
            foreach (var denomName in Enum.GetNames(typeof(DenominationNames)))
            {
                Console.WriteLine(denomName);
            }

            Console.WriteLine($"Add denominations in format {format}");
            Console.WriteLine("Multiple denominations can be added in one line separated by ','");
            Console.WriteLine("Example: 'TwoPound - 20, Penny - 100, TenPence - 30'");
            try
            {
                var input = Console.ReadLine();
                var coins = ProcessInputCoins(input);
                Cash.AddCoins(coins);
            }
            catch (InvalidInputException exception)
            {
                Console.WriteLine($"Failed to add coins. Error : {exception.Message}");
                return;
            }

            var message = "Successfully added coins";
            _logger.Debug(message);
            Console.WriteLine(message);
        }

        internal List<Denomination> ProcessInputCoins(string input)
        {
            var coinsToAdd = new List<Denomination>();
            var multipleCoins = input.Split(',');
            foreach (var coin in multipleCoins)
            {
                var coinAmount = coin.Split('-');
                if (coinAmount.Length > 2)
                {
                    _logger.Error($"Incorrect Coin input {coin}");
                    throw new InvalidInputException($"Incorrect Coin input {coin}");
                }

                try
                {
                    var coinName = (DenominationNames)Enum.Parse(typeof(DenominationNames), coinAmount[0].Trim(), true);
                    var converted = int.TryParse(coinAmount[1].Trim(), out var amount);
                    if (!converted)
                    {

                        _logger.Error($"Bad coin amount '{coinAmount[1]}'");
                        throw new InvalidInputException($"Bad coin amount '{coinAmount[1]}'");
                    }

                    for (var i = 0; i < amount; i++)
                    {
                        coinsToAdd.Add(coinName.GetCurrency());
                    }
                }
                catch (ArgumentException e)
                {
                    _logger.Error($"Invalid Coin {coinAmount[0]}", e);
                    throw new InvalidInputException($"Invalid Coin {coinAmount[0]}", e);
                }
            }

            return coinsToAdd;
        }


        internal List<Denomination> ProcessPaymentCoins(string input)
        {
            var coinsToAdd = new List<Denomination>();
            var multipleCoins = input.Split(',');
            foreach (var coin in multipleCoins)
            {

                try
                {
                    var coinName = (DenominationNames)Enum.Parse(typeof(DenominationNames), coin, true);

                    coinsToAdd.Add(coinName.GetCurrency());
                }
                catch (ArgumentException e)
                {
                    _logger.Error($"Invalid Coin {coin}", e);
                    throw new InvalidInputException($"Invalid Coin {coin}", e);
                }
            }

            return coinsToAdd;
        }

        public void AddProduct()
        {
            Console.WriteLine("Enter Product Name: ");
            var productName = Console.ReadLine();

            Console.WriteLine("Enter Price");
            var price = Console.ReadLine();

            Console.WriteLine("Enter Amount");
            var amount = Console.ReadLine();

            try
            {
                CheckProduct(productName, price, amount);
                Console.WriteLine("Product successfully added");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to add product. Error '{e.Message}'");
            }

            Console.WriteLine(GetAvailableProducts());
        }

        internal bool CheckProduct(string productName, string price, string amount)
        {
            if (Products.ContainsKey(new Product { Name = productName }))
            {
                const string message = "Product already exists";
                _logger.Error(message);
                throw new InvalidInputException(message);
            }

            var convertedDec = decimal.TryParse(price, out var priceDecimal);

            if (!convertedDec)
            {
                var message = $"Invalid input price '{price}'";
                _logger.Error(message);
                throw new InvalidInputException(message);
            }

            var convertedInt = int.TryParse(amount, out var intAmount);

            if (!convertedInt)
            {
                var message = $"Invalid input amount '{amount}'";
                _logger.Error(message);
                throw new InvalidInputException(message);
            }

            var inputPrice = Math.Round(priceDecimal, 2, MidpointRounding.AwayFromZero);

            Products.Add(new Product(productName, inputPrice), intAmount);
            _logger.Debug("Product added");
            return true;
        }

        public void BuyItem()
        {
            Console.WriteLine("Enter Product Name: ");
            var productName = Console.ReadLine();

            var productSelected = ProductExists(productName);

            if (productSelected == null)
            {
                Console.WriteLine("No such Product exists");
                return;
            }

            if (!ProductAvailable(productName))
            {
                Console.WriteLine("Product out of Stock");
                return;
            }

            Console.WriteLine("Enter Coins by name separated by ',' (Pound,Pound,FivePence) :");
            var coinNames = Console.ReadLine();
            List<Denomination> payment;

            try
            {
                payment = ProcessPaymentCoins(coinNames);
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            var totalPayment = payment.Sum(x => x.Value);

            var priceDifference = totalPayment - productSelected.Price;

            if (priceDifference < 0)
            {
                Console.WriteLine($"Payment {totalPayment} less than product price {productSelected.Price}");
                return;
            }

            if (priceDifference == 0)
            {
                Console.WriteLine("Exact payment given. No Change to return");
                Cash.AddCoins(payment);
                Products[productSelected] -= 1;
                Console.WriteLine($"{productName} bought successfully");
                
            }
            else
            {
                var changeToReturn = Cash.CanReturnChange(priceDifference, payment);

                if (!changeToReturn.Success)
                {
                    Console.WriteLine($"Unable to provide enough change for selected Product {productName}");
                    Console.WriteLine($"Change returned: {string.Join(",",payment)} ");
                }
                else
                {
                    Products[productSelected] -= 1;
                    Console.WriteLine($"{productName} bought successfully");
                    Console.WriteLine($"Change returned: {changeToReturn} ");
                }
            }

            Console.WriteLine(GetAvailableProducts());
            Console.WriteLine(GetAvailableCash());
        }

        internal Product ProductExists(string productName)
        {
            try
            {
                var product = Products.Keys.Single(x => x.Name.Equals(productName,StringComparison.InvariantCultureIgnoreCase));
                return product;
            }
            catch (InvalidOperationException)
            {
                _logger.Error($"Product '{productName} not found'");
                return null;
            }
        }

        private bool ProductAvailable(string productName)
        {
            return Products[new Product {Name = productName}] > 0;
        }

        public void ClearExistingProducts()
        {
            Products.Clear();
            Console.WriteLine("All products cleared");
        }

        public void ClearExistingCash()
        {
            Cash.ClearCash();
            Console.WriteLine("All Cash cleared");
        }
    }
}
