using PromotionEngine.Models;
using PromotionEngine.Persistance;
using PromotionEngine.Services.PromotionStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Services
{
    public class PromotionCalculator : IPromotionCalculator
    {
        private readonly PromotionContext _context;
        private readonly IRepository<StockKeepingUnit> _skuRepo;
        private readonly IRepository<Promotion> _promotoRepo;

        public PromotionCalculator(PromotionContext context, IRepository<StockKeepingUnit> stockKeepingUnitRepo, IRepository<Promotion> promotionRepo)
        {

            _context = context;
            _skuRepo = stockKeepingUnitRepo;
            _promotoRepo = promotionRepo;
        }

        public async Task<(decimal, Dictionary<char, int>)> CalculateTotalPromotionPrice(Dictionary<char, int> unitOrders)
        {
            decimal result = 0;
            Dictionary<char, int> excessUnitOrders = new(unitOrders);

            foreach (var unitOrder in unitOrders)
            {
                var promotions = await FindActivePromotionForOrder(unitOrder.Key);

                foreach (var promotion in promotions)
                {
                    while (await OrderContainsRequiredUnitsTotriggerPromotion(excessUnitOrders, promotion.RequiredUnitsToTrigger))
                    {
                        result += await ApplyPromotion(promotion);
                        foreach (var unit in promotion.RequiredUnitsToTrigger)
                            excessUnitOrders[unit.Key] -= promotion.RequiredUnitsToTrigger[unit.Key];
                    }
                }
            }
            return (result, excessUnitOrders);
        }

        private async Task<IEnumerable<Promotion>> FindActivePromotionForOrder(char skuOrderKey) =>
            await Task.FromResult(_promotoRepo.GetAll(p => p.RequiredUnitsToTrigger.ContainsKey(skuOrderKey) && p.IsActive));

        private static async Task<bool> OrderContainsRequiredUnitsTotriggerPromotion(Dictionary<char, int> unitOrderCounter, Dictionary<char, int> requiredunitsToTrigger) =>
           await Task.FromResult(!requiredunitsToTrigger.Select(p => unitOrderCounter.ContainsKey(p.Key) && unitOrderCounter[p.Key] >= p.Value).Contains(false));

        private async Task<decimal> ApplyPromotion(Promotion promotion)
        {
            //TODO fix newables to DI
            if (promotion.Type == PromotionType.Fixed)
                _context.SetStrategy(new FixedPromotion());

            if (promotion.Type == PromotionType.Percentage)
                _context.SetStrategy(new PercentagePromotion(_skuRepo.GetAll()));

            return await _context.Execute(promotion);
        }
    }
}

