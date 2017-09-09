using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.DTO;
using NLog;

namespace VendingMachine
{
    public class VendingCash
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

        public bool CanReturnChange(decimal changeRequired)
        {
            var changeToReturn = new List<Denomination>();

            _logger.Debug("Starting allocation of coins");

            foreach (var coin in InternalCash.Where(x => x.Value > 0))
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

                if (changeRequired == 0)
                {
                    RemoveCoins(changeToReturn);

                    _logger.Debug("Change allocated successfully");

                    return true;
                }

            }

            _logger.Debug("Unable to allocate coins");

            return false;
        }
    }
}
