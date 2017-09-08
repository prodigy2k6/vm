using System;

namespace VendingMachine
{
	public abstract class Denomination
	{
		public virtual string name { get; }
		public virtual decimal value { get; }

	}
}
