using System;
using System.Collections.Generic;
using System.Linq;
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
				}
				else
					throw new Exception($"No {coin.name} available in machine");
			}
        }

        public bool canGetChange(decimal changeRequired)
        {
            var changeToReturn = new List<Denomination>();

            

            bool calcChange = false;

            foreach (var coin in internalCash.Where(x=> x.Value > 0))
            {
                var currentCoinCount = coin.Value;
                var allocatedCoins = 0;

                while (!calcChange && changeRequired - coin.Value > 0)
                {
                    if (changeRequired - coin.Value == 1)
                    {
                        
                    }
                }
                

            }



            return true;
        }


	}


}
