using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using VendingMachine.DTO;
using VendingMachine.DTO.Enums;
using VendingMachine.Exceptions;
using VendingMachine.Helpers;

namespace VendingMachine
{
	public class VendingMachineState
	{
		Dictionary<Product, int> products { get; }
		IVendingCash cash { get;  }

	    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public VendingMachineState()
		{
			products = new Dictionary<Product, int>();
            cash = new VendingCash();
		}

	    public string GetAvailableProducts()
	    {
	        var stringBuilder = new StringBuilder();

	        if (products.Count == 0)
	            return "No products in VendingMachine";

	        foreach (var product in products)
	        {
	            stringBuilder.AppendLine($"{product.Key} Count: {product.Value}");
	        }

	        return stringBuilder.ToString();
	    }

	    public string GetAvailableCash()
	    {
	        return cash.ShowAvailableCash();
	    }

	    public void addCoins()
	    {
	        var format = "'Name' 'Amount'";
	        Console.WriteLine("Available Denominations: ");
	        foreach (var denomName in Enum.GetNames(typeof(DenominationNames)))
            {
	            Console.WriteLine(denomName);
	        }

            Console.WriteLine($"Add denominations in format {format}");
	        Console.WriteLine("Multiple denominations can be added in one line separated by ','");
	        Console.WriteLine("Example: 'TwoPound 20, Penny 100, TenPence 30'");
	        try
	        {
	            var input = Console.ReadLine();
	            var coins = processInputCoins(input);
                cash.AddCoins(coins);
	        }
	        catch (InvalidInputException exception)
	        {
                Console.WriteLine($"Failed to add coins. Error : {exception.Message}");
	        }

	        var message = "Successfully added coins";
            _logger.Debug(message);
            Console.WriteLine(message);
            Console.WriteLine("");
	        cash.ShowAvailableCash();
	        Console.ReadLine();
	    }

	    internal List<Denomination> processInputCoins(string input)
	    {
            var coinsToAdd = new List<Denomination>();
	        var multipleCoins = input.Split(',');
	        foreach (var coin in multipleCoins)
	        {
	            var coinAmount = coin.Split(' ');
	            if (coinAmount.Length > 2)
	            {
                    _logger.Error($"Incorrect Coin input {coin}");
	                throw new InvalidInputException($"Incorrect Coin input {coin}");
                }

                try
	            {
	                var coinName = (DenominationNames) Enum.Parse(typeof(DenominationNames), coinAmount[0], true);
	                var converted = int.TryParse(coinAmount[1], out var amount);
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

	        Console.ReadLine();
	    }

	    internal bool CheckProduct(string productName, string price, string amount)
	    {
	        if (products.ContainsKey(new Product { Name = productName }))
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

            products.Add(new Product(products.Count,productName,inputPrice),intAmount);
            _logger.Debug("Product added");
	        return true;
	    }

	    public void BuyItem()
	    {
	        
	    }
    }
}
