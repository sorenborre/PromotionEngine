using PromotionEngine.Models;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class PriceCalculator
    {
        public decimal CalculateTotalPrice(List<StockKeepingUnitOrder> stockKeepingUnitOrders)
        {
            decimal result = 0;

            foreach (StockKeepingUnitOrder order in stockKeepingUnitOrders)
            {
                if (order.Amount >= 3 && order.StockKeepingUnit.Id == 'A')
                {
                    result += 130 * Math.Floor((decimal)order.Amount / 3);
                    result += order.StockKeepingUnit.Price * (order.Amount % 3);
                }
                else if (order.Amount >= 2 && order.StockKeepingUnit.Id == 'B') 
                {
                    result += 45 * Math.Floor((decimal)order.Amount / 2);
                    result += order.StockKeepingUnit.Price * (order.Amount % 2);
                }
                else
                    result += order.StockKeepingUnit.Price * order.Amount;
            }
            return result;
        }
    }
}