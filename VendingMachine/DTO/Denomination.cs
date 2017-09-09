
using VendingMachine.DTO.Enums;

namespace VendingMachine.DTO
{
	public abstract class Denomination
	{
		public virtual DenominationNames Name { get; }
		public virtual decimal Value { get; }

	}
}
