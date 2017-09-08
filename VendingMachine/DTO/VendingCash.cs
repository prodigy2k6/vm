using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.DTO;

namespace VendingMachine
{
	public class VendingCash
	{
		Dictionary<Denomination, int> internalCash { get; set; }
        Dictionary<string, int> internalCash2 { get; set; }
        SortedDictionary<Denomination, int> internalCash3 { get; set; }
		public List<Denomination> cash { set; get; }

		public VendingCash()
		{
			cash = new List<Denomination>();
			internalCash = new Dictionary<Denomination, int>();
            internalCash3 = new SortedDictionary<Denomination, int>(new DenominationComparer());
		}

        public void addCash(IEnumerable<Denomination> coins)
        {
            cash.AddRange(coins);

            foreach (var coin in coins)
            {
                if (internalCash3.ContainsKey(coin))
                {
                    internalCash3[coin] = internalCash[coin] + 1;
                }
                else
                    internalCash3.Add(coin, 1);
            }

        }

        public void processNewCoins(IEnumerable<Denomination> coins)
        {
			foreach (var coin in coins)
			{
				if (internalCash3.ContainsKey(coin))
				{
                    internalCash3[coin]++;
				}
				else
					internalCash3.Add(coin, 1);
			}
        }
			

		public decimal CurrentTotal()
		{
			return cash.Sum(x => x.value);
		}

		public decimal CurrentTotal2()
		{
            //return internalCash.SelectMany(x => x.Key.value * x.Value);
            var a =  internalCash3.Select(x => x.Key.value * x.Value).Sum();

            return a;
		}

        public void removeCoins(IEnumerable<Denomination> coins)
        {
			foreach (var coin in coins)
			{
				if (internalCash3.ContainsKey(coin))
				{
                    if (internalCash3[coin] < 1)
                        throw new Exception($"Not enough {coin.name} to deduct from machine");
                    
                    internalCash3[coin]--;
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
