using PromotionEngine.Models;
using PromotionEngine.Services.PromotionStrategies;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PromotionEngine.Tests
{
    public class PercentagePromotionTests
    {
        [Fact]
        public async Task CalculatePromotionDiscount_should_subtract_promotionvalue_as_percentage_off_Sku()
        {
            //arange
            Promotion promotion = new(PromotionType.Percentage, true, 5, new Dictionary<char, int>() { { 'A', 1 } });

            List<StockKeepingUnit> stockKeepingUnits = new()
            {
                new StockKeepingUnit('A', 50),
            };

            PercentagePromotion sut = new(stockKeepingUnits);

            //act
            var result = await sut.CalculatePromotionDiscount(promotion);

            //assert
            Assert.Equal((decimal)47.5, result);
        }
    }
}
