using System.Collections.Generic;
using VendingMachine.DTO;

namespace VendingMachine
{
	public class VendingMachineState
	{
		List<Product> products { get; set; }
		List<Denomination> cash { get; set; }

		public VendingMachineState(List <Denomination> _cash, List<Product> _products )
		{
			products = _products;
			_cash = cash;
		}
	}
}
