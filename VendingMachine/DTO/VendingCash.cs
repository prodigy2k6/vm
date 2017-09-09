using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.DTO;

namespace VendingMachine
{
	public class VendingCash
	{
        internal SortedDictionary<Denomination, int> internalCash { get; set; }

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
				}
				else
					internalCash.Add(coin, 1);
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


            //var dict = internalCash

            return true;
        }


	}


}
