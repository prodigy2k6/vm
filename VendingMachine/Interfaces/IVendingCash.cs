using System.Collections.Generic;
using VendingMachine.DTO;

namespace VendingMachine.Interfaces
{
    public interface IVendingCash
    {
        void AddCoins(IEnumerable<Denomination> coins);
        decimal CurrentTotal();
        void RemoveCoins(IEnumerable<Denomination> coins);
        string ShowAvailableCash();
        bool DenominationsAdded();
        bool OutOfCoins();
        ReturnChangeResult CanReturnChange(decimal changeRequired,IEnumerable<Denomination> newCoins = null);
        void ClearCash();
    }
}