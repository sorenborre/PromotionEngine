using PromotionEngine.Models;
using System.Threading.Tasks;

namespace PromotionEngine.Services.PromotionStrategies
{
    public class FixedPromotion : IPromotionStrategy
    {
        public async Task<decimal> CalculatePromotionDiscount(Promotion promotion)
        {
            return await Task.FromResult(promotion.Value);
        }
    }
}
