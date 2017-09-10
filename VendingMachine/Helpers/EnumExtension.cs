using VendingMachine.DTO;
using VendingMachine.DTO.Enums;
using VendingMachine.Exceptions;

namespace VendingMachine.Helpers
{
    public static class EnumExtension
    {
        public static Denomination GetCurrency(this DenominationNames denominationName)
        {
            switch (denominationName)
            {
                case DenominationNames.FiftyPence:
                    return Currency.FiftyPence;
                case DenominationNames.FivePence:
                    return Currency.FivePence;
                case DenominationNames.Penny:
                    return Currency.Penny;
                case DenominationNames.Pound:
                    return Currency.Pound;
                case DenominationNames.TenPence:
                    return Currency.TenPence;
                case DenominationNames.TwentyPence:
                    return Currency.TwentyPence;
                case DenominationNames.TwoPence:
                    return Currency.TwoPence;
                case DenominationNames.TwoPound:
                    return Currency.TwoPound;
                default:
                    throw new InvalidInputException("Invalid Denomination name used");
            }
        }
    }
}
