using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine
{
    public class PriceCalculator
    {
        public decimal CalculateTotalPrice(Dictionary<char, StockKeepingUnitOrder> SkuOrders, List<Promotion> promotions)
        {
            decimal result = 0;
            Dictionary<char, int> unitOrderCounter = SkuOrders.ToDictionary(order => order.Key, order => order.Value.Amount);

            foreach (KeyValuePair<char, StockKeepingUnitOrder> skuOrder in SkuOrders)
            {
                Promotion promotion = FindActivePromotionForOrder(promotions, skuOrder);

                while (OrderContainsRequiredUnitsTotriggerPromotion(unitOrderCounter, promotion.RequiredunitsToTrigger))
                {
                    result += promotion.Price;

                    foreach (var unit in promotion.RequiredunitsToTrigger)
                        unitOrderCounter[unit.Key] -= promotion.RequiredunitsToTrigger[unit.Key];
                }
            }

            return result += unitOrderCounter.Select(c => c.Value * SkuOrders[c.Key].StockKeepingUnit.Price).Sum();
        }

        private static Promotion FindActivePromotionForOrder(List<Promotion> promotions, KeyValuePair<char, StockKeepingUnitOrder> skuOrder) =>
            promotions.Where(p => p.RequiredunitsToTrigger.ContainsKey(skuOrder.Key)).FirstOrDefault(p => p.IsActive);

        private static bool OrderContainsRequiredUnitsTotriggerPromotion(Dictionary<char, int> unitOrderCounter, Dictionary<char, int> requiredunitsToTrigger) =>
            !requiredunitsToTrigger.Select(p => unitOrderCounter.ContainsKey(p.Key) && unitOrderCounter[p.Key] >= p.Value).Contains(false);
    }
}