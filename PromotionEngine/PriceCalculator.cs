using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class PriceCalculator
    {
        public decimal CalculateTotalPrice(List<StockKeepingUnitOrder> stockKeepingUnitOrders)
        {
            decimal result = 0;

            foreach (StockKeepingUnitOrder order in stockKeepingUnitOrders)
                result += order.StockKeepingUnit.Price * order.Amount;

            return result;
        }
    }
}
