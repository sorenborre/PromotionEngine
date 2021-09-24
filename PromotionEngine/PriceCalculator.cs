using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Models;

namespace PromotionEngine
{
    public class PriceCalculator
    {
        public decimal CalculateTotalPrice2(Dictionary<char, StockKeepingUnitOrder> SkuOrders, List<Promotion> promotions)
        {
            decimal result = 0;

            Promotion promotion = promotions.Where(p => p.RequiredProductsToTrigger.First().Key == 'A').First();

            if (SkuOrders['A'].Amount >= 3)
            {
                result += 130 * Math.Floor((decimal)SkuOrders['A'].Amount / 3);
                result += SkuOrders['A'].StockKeepingUnit.Price * (SkuOrders['A'].Amount % 3);
            }
            else
                result += SkuOrders['A'].StockKeepingUnit.Price * SkuOrders['A'].Amount;

            if (SkuOrders['B'].Amount >= 2)
            {
                result += 45 * Math.Floor((decimal)SkuOrders['B'].Amount / 2);
                result += SkuOrders['B'].StockKeepingUnit.Price * (SkuOrders['B'].Amount % 2);
            }
            else
                result += SkuOrders['B'].StockKeepingUnit.Price * SkuOrders['B'].Amount;

            if (SkuOrders['C'].Amount >= 1 && SkuOrders['D'].Amount >= 1)
            {
                int amountOfTriggeredPromos = Math.Min(SkuOrders['C'].Amount, SkuOrders['D'].Amount);
                result += 30 * amountOfTriggeredPromos;
            }

            if (SkuOrders['C'].Amount > SkuOrders['D'].Amount)
                result += SkuOrders['C'].StockKeepingUnit.Price * (SkuOrders['C'].Amount - SkuOrders['D'].Amount);

            else if (SkuOrders['C'].Amount < SkuOrders['D'].Amount)
                result += SkuOrders['D'].StockKeepingUnit.Price * SkuOrders['D'].Amount - SkuOrders['C'].Amount;

            return result;
        }

        public decimal CalculateTotalPrice(Dictionary<char, StockKeepingUnitOrder> SkuOrders, List<Promotion> promotions)
        {
            decimal result = 0;


            result += CalculateOneTypeFixedPromotion(SkuOrders['A'], promotions);

            result += CalculateOneTypeFixedPromotion(SkuOrders['B'], promotions);


            if (SkuOrders['C'].Amount >= 1 && SkuOrders['D'].Amount >= 1)
            {
                int amountOfTriggeredPromos = Math.Min(SkuOrders['C'].Amount, SkuOrders['D'].Amount);
                result += 30 * amountOfTriggeredPromos;
            }

            if (SkuOrders['C'].Amount > SkuOrders['D'].Amount)
                result += SkuOrders['C'].StockKeepingUnit.Price * (SkuOrders['C'].Amount - SkuOrders['D'].Amount);

            else if (SkuOrders['C'].Amount < SkuOrders['D'].Amount)
                result += SkuOrders['D'].StockKeepingUnit.Price * SkuOrders['D'].Amount - SkuOrders['C'].Amount;
            return result;
        }

        private static decimal CalculateOneTypeFixedPromotion(StockKeepingUnitOrder SkuOrder, List<Promotion> promotions)
        {

            decimal result = 0;
            Promotion promotion = FindActivePromotion(SkuOrder, promotions);
            int RequiredProductsToTrigger = promotion.RequiredProductsToTrigger[SkuOrder.StockKeepingUnit.Id];

            if (SkuOrder.Amount >= RequiredProductsToTrigger)
            {
                result += promotion.Price * Math.Floor((decimal)SkuOrder.Amount / RequiredProductsToTrigger);
                result += SkuOrder.StockKeepingUnit.Price * (SkuOrder.Amount % RequiredProductsToTrigger);
            }
            else
                result += SkuOrder.StockKeepingUnit.Price * SkuOrder.Amount;

            return result;
        }

        private static Promotion FindActivePromotion(StockKeepingUnitOrder SkuOrder, List<Promotion> promotions)
        {
            return promotions
                             .Where(p => p.RequiredProductsToTrigger
                             .First().Key == SkuOrder.StockKeepingUnit.Id)
                             .First();
        }
    }
}