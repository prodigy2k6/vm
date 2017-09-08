using System;
using System.Collections.Generic;

namespace VendingMachine.DTO
{
    public class DenominationComparer : IComparer<Denomination>
    {
        public int Compare(Denomination a, Denomination b)
        {
            if (a.value == b.value)
                return 0;

            if (a.value > b.value)
                return -1;

            return 1;
        }
    }
}
