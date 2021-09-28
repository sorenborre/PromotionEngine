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
            bool containsKeys;
            IEnumerable<bool> check;
            Dictionary<char, int> counterDict = SkuOrders.ToDictionary(order => order.Key, order => order.Value.Amount);

            foreach (KeyValuePair<char, StockKeepingUnitOrder> skuOrder in SkuOrders)
            {
                Promotion promotion = promotions.Where(p => p.RequiredProductsToTrigger.ContainsKey(skuOrder.Key)).FirstOrDefault(p => p.IsActive);

                containsKeys = promotion.RequiredProductsToTrigger.Select(p => counterDict.ContainsKey(p.Key) && counterDict[p.Key] >= p.Value).Contains(false) == false;

                if (containsKeys)
                {
                    check = promotion.RequiredProductsToTrigger.Select(p => counterDict[p.Key] >= p.Value);

                    while (!check.Contains(false))
                    {
                        result += promotion.Price;

                        foreach (var item in promotion.RequiredProductsToTrigger)
                            counterDict[item.Key] -= promotion.RequiredProductsToTrigger[item.Key];

                        check = promotion.RequiredProductsToTrigger.Select(p => counterDict[p.Key] >= p.Value);
                    }
                }
            }

            result += counterDict.Select(c => c.Value * SkuOrders[c.Key].StockKeepingUnit.Price).Sum();

            return result;
        }
    }
}