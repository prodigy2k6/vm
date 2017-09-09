
using VendingMachine.DTO.Enums;

namespace VendingMachine.DTO
{
	public class Pound : Denomination
	{
		public override DenominationNames Name => DenominationNames.Pound;

	    public override decimal Value => 1.00m;
	}

	public class FiftyPence : Denomination
	{
        public override DenominationNames Name => DenominationNames.FiftyPence;

        public override decimal Value => 0.50m;
	}

	public class TwoPound : Denomination
	{
		public override DenominationNames Name => DenominationNames.TwoPound;

	    public override decimal Value => 2.00m;
	}

	public class TwentyPence : Denomination
	{
		public override DenominationNames Name => DenominationNames.TwentyPence;

	    public override decimal Value => 0.20m;
	}

	public class TenPence : Denomination
	{
		public override DenominationNames Name => DenominationNames.TenPence;

	    public override decimal Value => 0.10m;
	}

	public class FivePence : Denomination
	{
		public override DenominationNames Name => DenominationNames.FivePence;

	    public override decimal Value => 0.05m;
	}

	public class TwoPence : Denomination
	{
		public override DenominationNames Name => DenominationNames.TwoPence;

	    public override decimal Value => 0.02m;
	}

	public class Penny : Denomination
	{
		public override DenominationNames Name => DenominationNames.Penny;

	    public override decimal Value => 0.01m;
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
