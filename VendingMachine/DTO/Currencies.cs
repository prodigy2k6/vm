using System;

namespace VendingMachine
{
	public class Pound : Denomination
	{
		public override string name { get { return "Pound"; } }

		public override decimal value { get { return 1.00m; } }
	}

	public class FiftyPence : Denomination
	{
		public override string name { get { return "50p"; } }

		public override decimal value { get { return 0.50m; } }
	}

	public class TwoPound : Denomination
	{
		public override string name { get { return "TwoPound"; } }

		public override decimal value { get { return 2.00m; } }
	}

	public class TwentyPence : Denomination
	{
		public override string name { get { return "TwentyPence"; } }

		public override decimal value { get { return 0.20m; } }
	}

	public class TenPence : Denomination
	{
		public override string name { get { return "TenPence"; } }

		public override decimal value { get { return 0.10m; } }
	}

	public class FivePence : Denomination
	{
		public override string name { get { return "FivePence"; } }

		public override decimal value { get { return 0.05m; } }
	}

	public class TwoPence : Denomination
	{
		public override string name { get { return "TwoPence"; } }

		public override decimal value { get { return 0.02m; } }
	}

	public class Penny : Denomination
	{
		public override string name { get { return "Penny"; } }

		public override decimal value { get { return 0.01m; } }
	}
}
