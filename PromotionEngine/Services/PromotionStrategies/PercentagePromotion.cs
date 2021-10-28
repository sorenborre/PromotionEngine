using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Services.PromotionStrategies
{
    public class PercentagePromotion : IPromotionStrategy
    {
        private readonly List<StockKeepingUnit> _stockKeepingUnits;

        public PercentagePromotion(List<StockKeepingUnit> stockKeepingUnits)
        {
            _stockKeepingUnits = stockKeepingUnits;
        }

        public async Task<decimal> CalculatePromotionDiscount(Promotion promotion)
        {
            StockKeepingUnit sku = _stockKeepingUnits.FirstOrDefault(s => s.Id == promotion.RequiredUnitsToTrigger.First().Key);

            if (sku == null)
                throw new Exception($"Could not find stockKeepingUnit in {this.GetType().Name}.");

            return await Task.FromResult((1 - promotion.Value / 100) * sku.Price);
        }
    }
}
