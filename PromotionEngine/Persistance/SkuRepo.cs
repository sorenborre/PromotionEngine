using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Persistance
{
    public class SkuRepo : IRepository<StockKeepingUnit>
    {
        public List<StockKeepingUnit> GetAll()
        {
            return new List<StockKeepingUnit>() { 
                new StockKeepingUnit('A', 50), 
                new StockKeepingUnit('B', 30), 
                new StockKeepingUnit('C', 20), 
                new StockKeepingUnit('D', 15) };
        }
    }
}
