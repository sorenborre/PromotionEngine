using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Persistance
{
    public class PromoRepo : IRepository<Promotion>
    {
        private readonly List<Promotion> promotions;

        public PromoRepo()
        {
            promotions = new()
            {
                new Promotion(true, 130, new Dictionary<char, int>() { { 'A', 3 } }),
                new Promotion(true, 45, new Dictionary<char, int>() { { 'B', 2 } }),
                new Promotion(true, 30, new Dictionary<char, int>() { { 'C', 1 }, { 'D', 1 } })
            };
        }

        public List<Promotion> GetAll() => promotions;
    }
}
