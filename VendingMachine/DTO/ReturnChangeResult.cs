
using System.Collections.Generic;
using System.Linq;

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

        public override string ToString()
        {
            return string.Join(",",ChangeToReturn.Select(x=>x.Name));
        }
    }
}
