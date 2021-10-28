using PromotionEngine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Services
{
    public class PriceCalculator
    {
        private readonly IPromotionCalculator _promotionService;

        public PriceCalculator(IPromotionCalculator promotionService)
        {
            _promotionService = promotionService;
        }

        public async Task<decimal> CalculateTotalPrice(Dictionary<char, StockKeepingUnitOrder> SkuOrders)
        {

            (decimal result, Dictionary<char, int> unitOrderCounter) = 
                await _promotionService.CalculateTotalPromotionPrice(SkuOrders.ToDictionary(order => order.Key, order => order.Value.Amount));

            return result += await SumOfUnitsWithNoPromotion(SkuOrders, unitOrderCounter);
        }

        private static async Task<decimal> SumOfUnitsWithNoPromotion(Dictionary<char, StockKeepingUnitOrder> SkuOrders, Dictionary<char, int> unitOrderCounter) =>
            await Task.FromResult(unitOrderCounter.Select(c => c.Value * SkuOrders[c.Key].StockKeepingUnit.Price).Sum());
    }
}