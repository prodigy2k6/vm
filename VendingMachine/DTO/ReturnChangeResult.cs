
using System.Collections.Generic;

namespace VendingMachine.DTO
{
    public class ReturnChangeResult
    {
        public bool Success { get; set; }
        public IReadOnlyCollection<Denomination> ChangeToReturn { get; set; }

        public ReturnChangeResult(bool success, IReadOnlyCollection<Denomination> change)
        {
            Success = success;
            ChangeToReturn = change;
        }
    }
}
