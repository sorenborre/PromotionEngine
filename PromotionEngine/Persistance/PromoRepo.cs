using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Persistance
{
    public class PromoRepo : IRepository<Promotion>
    {
        public List<Promotion> GetAll()
        {
            return new List<Promotion>()
            {
                new Promotion(true, 130, new Dictionary<char, int>() { { 'A', 3 } }),
                new Promotion(true, 45, new Dictionary<char, int>() { { 'B', 2 } }),
                new Promotion(true, 30, new Dictionary<char, int>() { { 'C', 1 }, { 'D', 1 } })
            };
        }
    }
}
