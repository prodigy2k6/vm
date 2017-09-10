using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachine.DTO;
using NLog;
using VendingMachine.Helpers;
using VendingMachine.Interfaces;

namespace VendingMachine
{
    public class VendingCash : IVendingCash
    {
        internal SortedDictionary<Denomination, int> InternalCash { get; set; }

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public VendingCash()
        {
            InternalCash = new SortedDictionary<Denomination, int>(new DenominationComparer());
        }

        public void AddCoins(IEnumerable<Denomination> coins)
        {
            foreach (var coin in coins)
            {
                if (InternalCash.ContainsKey(coin))
                {
                    InternalCash[coin]++;
                    _logger.Debug($"Adding {coin.Name} coin to existing denomination");
                }
                else
                {
                    InternalCash.Add(coin, 1);
                    _logger.Debug($"Adding new {coin.Name} coin to cash");
                }
            }
        }

        public SortedDictionary<Denomination,int> AddCoinsToDictionary(SortedDictionary<Denomination,int> dictionary, IEnumerable<Denomination> coins)
        {
            if (coins != null)
            {
                foreach (var coin in coins)
                {
                    if (dictionary.ContainsKey(coin))
                    {
                        dictionary[coin]++;
                        _logger.Debug($"Adding {coin.Name} coin to existing denomination");
                    }
                    else
                    {
                        dictionary.Add(coin, 1);
                        _logger.Debug($"Adding new {coin.Name} coin to cash");
                    }
                }
            }
            return dictionary;
        }

        public decimal CurrentTotal()
        {
            return InternalCash.Select(x => x.Key.Value * x.Value).Sum();
        }

        public void RemoveCoins(IEnumerable<Denomination> coins)
        {
            foreach (var coin in coins)
            {
                if (InternalCash.ContainsKey(coin))
                {
                    if (InternalCash[coin] < 1)
                        throw new Exception($"Not enough {coin.Name} to deduct from machine");

                    InternalCash[coin]--;
                    _logger.Debug($"{coin.Name} deducted. {InternalCash[coin]} coins remain.");
                }
                else
                    throw new Exception($"No {coin.Name} available in machine");
            }
        }

        public string ShowAvailableCash()
        {
            var stringBuilder = new StringBuilder();

            if (InternalCash.Count == 0)
                return "No coins have been added to the Vending Machine";

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("******************CASH******************");

            foreach (var coin in InternalCash)
            {
                stringBuilder.AppendLine($"{coin.Key} Count: {coin.Value}");
            }

            return stringBuilder.ToString();
        }

        public bool DenominationsAdded()
        {
            return InternalCash.Count == 0;
        }

        public bool OutOfCoins()
        {
            return InternalCash.All(x => x.Value == 0);
        }

        public ReturnChangeResult CanReturnChange(decimal changeRequired, IEnumerable<Denomination> coinsAddedByUser = null)
        {
            var result = new ReturnChangeResult(false, null);

            var changeToReturn = new List<Denomination>();

            _logger.Debug("Starting allocation of coins");

            var copyDictionary = CopyDictionary(InternalCash);

            AddCoinsToDictionary(copyDictionary, coinsAddedByUser);

            foreach (var coin in copyDictionary.Where(x => x.Value > 0))
            {
                _logger.Debug($"'{coin.Key.Name}' {coin.Value} Coins available");
                var currentCoinCount = coin.Value;
                var allocatedCoins = 0;

                while (changeRequired - coin.Key.Value >= 0 && currentCoinCount > 0)
                {
                    changeRequired = changeRequired - coin.Key.Value;
                    allocatedCoins += 1;
                    currentCoinCount -= 1;

                    _logger.Debug($"1 '{coin.Key.Name}' coin allocated");
                    _logger.Debug($"'{coin.Key.Name}' coins remaining = {currentCoinCount}");
                    _logger.Debug($"Remaining change to acquire = {changeRequired}");

                }

                for (var i = 0; i < allocatedCoins; i++)
                {
                    changeToReturn.Add(coin.Key);
                }

                //success so add new coins and remove change to be given
                if (changeRequired == 0)
                {
                    if (coinsAddedByUser != null)
                    {
                        AddCoins(coinsAddedByUser);
                    }

                    RemoveCoins(changeToReturn);

                    _logger.Debug("Change allocated successfully");

                    result = new ReturnChangeResult(true, changeToReturn);

                    return result ;
                }

            }

            _logger.Debug("Unable to allocate coins");

            return result;
        }

        private SortedDictionary<Denomination, int> CopyDictionary(SortedDictionary<Denomination, int> dictionary)
        {
            var newDictionary = new SortedDictionary<Denomination, int>(new DenominationComparer());

            foreach (var coin in dictionary)
            {
                newDictionary.Add(coin.Key,coin.Value);
            }

            return newDictionary;
        }

        public void ClearCash()
        {
            InternalCash.Clear();
        }
    }
}
