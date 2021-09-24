﻿using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Persistance
{
    public class SkuRepo : IRepository<StockKeepingUnit>
    {
        private readonly List<StockKeepingUnit> stockKeepingUnits;

        public SkuRepo()
        {
            stockKeepingUnits = new()
            {
                new StockKeepingUnit('A', 50),
                new StockKeepingUnit('B', 30),
                new StockKeepingUnit('C', 20),
                new StockKeepingUnit('D', 15)
            };
        }
        public List<StockKeepingUnit> GetAll() => stockKeepingUnits;
    }
}