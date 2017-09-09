
using VendingMachine.DTO.Enums;

namespace VendingMachine.DTO
{
	public class Pound : Denomination
	{
		public override DenominationNames name => DenominationNames.Pound;

	    public override decimal value => 1.00m;
	}

	public class FiftyPence : Denomination
	{
        public override DenominationNames name => DenominationNames.FiftyPence;

        public override decimal value => 0.50m;
	}

	public class TwoPound : Denomination
	{
		public override DenominationNames name => DenominationNames.TwoPound;

	    public override decimal value => 2.00m;
	}

	public class TwentyPence : Denomination
	{
		public override DenominationNames name => DenominationNames.TwentyPence;

	    public override decimal value => 0.20m;
	}

	public class TenPence : Denomination
	{
		public override DenominationNames name => DenominationNames.TenPence;

	    public override decimal value => 0.10m;
	}

	public class FivePence : Denomination
	{
		public override DenominationNames name => DenominationNames.FivePence;

	    public override decimal value => 0.05m;
	}

	public class TwoPence : Denomination
	{
		public override DenominationNames name => DenominationNames.TwoPence;

	    public override decimal value => 0.02m;
	}

	public class Penny : Denomination
	{
		public override DenominationNames name => DenominationNames.Penny;

	    public override decimal value => 0.01m;
	}

    public class Currency
    {
        public static Denomination TwoPound => new TwoPound();

        public static Denomination Pound => new Pound();

        public static Denomination FiftyPence => new FiftyPence();

        public static Denomination TwentyPence => new TwentyPence();

        public static Denomination TenPence => new TenPence();

        public static Denomination FivePence => new FivePence();

        public static Denomination TwoPence => new TwoPence();

        public static Denomination Penny => new Penny();
    }
}
