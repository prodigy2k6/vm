using System;
using VendingMachine.DTO.Enums;

namespace VendingMachine
{
	public abstract class Denomination
	{
		public virtual DenominationNames name { get; }
		public virtual decimal value { get; }

	}
}
