using System.Collections.Generic;
using VendingMachine.DTO;

namespace VendingMachine.Helpers
{
    public class DenominationComparer : IComparer<Denomination>
    {
        public int Compare(Denomination a, Denomination b)
        {
            if (a.Value == b.Value)
                return 0;

            if (a.Value > b.Value)
                return -1;

            return 1;
        }
    }
}
