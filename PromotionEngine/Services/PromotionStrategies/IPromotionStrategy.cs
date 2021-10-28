using PromotionEngine.Models;
using System.Threading.Tasks;

namespace PromotionEngine.Services.PromotionStrategies
{
    public interface IPromotionStrategy
    {
        public Task<decimal> CalculatePromotionDiscount(Promotion promotion);

    }
}
