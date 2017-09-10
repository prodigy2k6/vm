namespace VendingMachine
{
    public interface IVendingMachineState
    {
        string GetAvailableProducts();
        string GetAvailableCash();
        void AddCoins();
        void AddProduct();
        void BuyItem();
        void ClearExistingProducts();
        void ClearExistingCash();
    }
}