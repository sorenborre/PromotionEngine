using PromotionEngine.Models;
using System.Threading.Tasks;

namespace PromotionEngine.Services.PromotionStrategies
{
    public class PromotionContext
    {
        public IPromotionStrategy _promotionStrategy;

        public void SetStrategy(IPromotionStrategy strategy)
        {
            _promotionStrategy = strategy;
        }

        public async Task<decimal> Execute(Promotion promotion)
        {
            return await _promotionStrategy.CalculatePromotionDiscount(promotion);
        }
    }
}