namespace PromotionEngine.Models
{
    public class StockKeepingUnitOrder
    {
        public StockKeepingUnitOrder(StockKeepingUnit stockKeepingUnit, int amount)
        {
            StockKeepingUnit = stockKeepingUnit;
            Amount = amount;
        }

        public StockKeepingUnit StockKeepingUnit { get; }
        public int Amount { get; }
    }
}

