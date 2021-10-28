using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromotionEngine.Services
{
    public interface IPromotionCalculator
    {
        Task<(decimal, Dictionary<char, int>)> CalculateTotalPromotionPrice(Dictionary<char, int> unitOrders);
    }
}