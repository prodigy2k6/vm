using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using VendingMachine.DTO;
using NLog;

namespace VendingMachine
{
    public class VendingCash
    {
        internal SortedDictionary<Denomination, int> internalCash { get; set; }

        private Logger _logger = LogManager.GetCurrentClassLogger();

        public VendingCash()
        {
            internalCash = new SortedDictionary<Denomination, int>(new DenominationComparer());
        }

        public void processNewCoins(IEnumerable<Denomination> coins)
        {
            foreach (var coin in coins)
            {
                if (internalCash.ContainsKey(coin))
                {
                    internalCash[coin]++;
                    _logger.Debug($"Adding {coin.name} coin to existing denomination");
                }
                else
                {
                    internalCash.Add(coin, 1);
                    _logger.Debug($"Adding new {coin.name} coin to cash");
                }
            }
        }


        public decimal CurrentTotal()
        {
            return internalCash.Select(x => x.Key.value * x.Value).Sum();
        }

        public void removeCoins(IEnumerable<Denomination> coins)
        {
            foreach (var coin in coins)
            {
                if (internalCash.ContainsKey(coin))
                {
                    if (internalCash[coin] < 1)
                        throw new Exception($"Not enough {coin.name} to deduct from machine");

                    internalCash[coin]--;
                    _logger.Debug($"{coin.name} deducted. {internalCash[coin]} coins remain.");
                }
                else
                    throw new Exception($"No {coin.name} available in machine");
            }
        }

        public bool canGetChange(decimal changeRequired)
        {
            var changeToReturn = new List<Denomination>();

            var calcChange = true;

            _logger.Debug("Starting allocation");

            foreach (var coin in internalCash.Where(x => x.Value > 0))
            {
                _logger.Debug($"'{coin.Key.name}' {coin.Value} Coins available");
                var currentCoinCount = coin.Value;
                var allocatedCoins = 0;

                while (changeRequired - coin.Key.value >= 0 && currentCoinCount > 0)
                {
                    changeRequired = changeRequired - coin.Key.value;
                    allocatedCoins += 1;
                    currentCoinCount -= 1;

                    _logger.Debug($"1 '{coin.Key.name}' coin allocated");
                    _logger.Debug($"'{coin.Key.name}' coins remaining = {currentCoinCount}");
                    _logger.Debug($"Remaining change to acquire = {changeRequired}");

                }

                for (var i = 0; i < allocatedCoins; i++)
                {
                    changeToReturn.Add(coin.Key);
                }

                if (changeRequired == 0)
                {
                    removeCoins(changeToReturn);

                    _logger.Debug("Change allocated successfully");

                    return true;
                }

            }

            _logger.Debug("Unable to allocate coins");
            return false;

            
        }


    }


}
