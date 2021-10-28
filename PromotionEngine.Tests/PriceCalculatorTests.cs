using Moq;
using PromotionEngine.Models;
using PromotionEngine.Persistance;
using PromotionEngine.Services;
using PromotionEngine.Services.PromotionStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PromotionEngine.Tests
{
    public class PriceCalculatorTests
    {
        [Theory]
        [InlineData(1, 1, 1, 0, 100)] //Scenario A. No active promotions
        [InlineData(5, 5, 1, 0, 370)] //Scenario B. Active promotions, buy 'n' items of a SKU for a fixed price 
        [InlineData(3, 5, 1, 1, 280)] //Scenario C. Active promotions, buy 'n' items of a SKU for a fixed price AND buy SKU 1 & SKU 2 for a fixed price
        [InlineData(10, 10, 11, 10, 985)] //Scenario D. Active promotions, buy 'n' items of a SKU for a fixed price AND buy SKU 1 & SKU 2 for a fixed price
        [InlineData(10, 10, 10, 11, 980)] //Scenario E. Active promotions, buy 'n' items of a SKU for a fixed price AND buy SKU 1 & SKU 2 for a fixed price
        public async Task CalculateTotalPrice_should_add_prices_using_active_fixed_promotions(
            int amountA,
            int amountB,
            int amountC,
            int amountD,
            decimal expected)
        {
            //arange
            List<Promotion> promotions = new()
            {
                new Promotion(PromotionType.Fixed, true, 130, new Dictionary<char, int>() { { 'A', 3 } }),
                new Promotion(PromotionType.Fixed, true, 45, new Dictionary<char, int>() { { 'B', 2 } }),
                new Promotion(PromotionType.Fixed, true, 30, new Dictionary<char, int>() { { 'C', 1 }, { 'D', 1 } }),
            };

            List<StockKeepingUnit> stockKeepingUnits = new()
            {
                new StockKeepingUnit('A', 50),
                new StockKeepingUnit('B', 30),
                new StockKeepingUnit('C', 20),
                new StockKeepingUnit('D', 15)
            };

            Dictionary<char, StockKeepingUnitOrder> stockKeepingUnitOrders = new()
            {
                { 'A', new StockKeepingUnitOrder(stockKeepingUnits[0], amountA) },
                { 'B', new StockKeepingUnitOrder(stockKeepingUnits[1], amountB) },
                { 'C', new StockKeepingUnitOrder(stockKeepingUnits[2], amountC) },
                { 'D', new StockKeepingUnitOrder(stockKeepingUnits[3], amountD) }
            };

            Mock<IRepository<Promotion>> promotionRepoMock = await CreateRepoMock(promotions);
            Mock<IRepository<StockKeepingUnit>> skuRepoMock = await CreateRepoMock(stockKeepingUnits);

            PriceCalculator sut = new(new PromotionCalculator(new PromotionContext(), skuRepoMock.Object, promotionRepoMock.Object));

            //act
            decimal result = await sut.CalculateTotalPrice(stockKeepingUnitOrders);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact] //buy 'n' items of a SKU at a 5% discount
        public async Task CalculateTotalPrice_should_calculate_percentagetype_promotion()
        {
            //arange
            List<Promotion> promotions = new()
            {
                new Promotion(PromotionType.Percentage, true, 5, new Dictionary<char, int>() { { 'A', 1 } })
            };

            List<StockKeepingUnit> stockKeepingUnits = new()
            {
                new StockKeepingUnit('A', 50),
            };

            Dictionary<char, StockKeepingUnitOrder> stockKeepingUnitOrders = new()
            {
                { 'A', new StockKeepingUnitOrder(stockKeepingUnits[0], 2) },
            };

            Mock<IRepository<Promotion>> promotionRepoMock = await CreateRepoMock(promotions);
            Mock<IRepository<StockKeepingUnit>> skuRepoMock = await CreateRepoMock(stockKeepingUnits);

            PriceCalculator sut = new(new PromotionCalculator(new PromotionContext(), skuRepoMock.Object, promotionRepoMock.Object));

            //act
            var result = await sut.CalculateTotalPrice(stockKeepingUnitOrders);

            //assert
            Assert.Equal(95, result);
        }

        private static async Task<Mock<IRepository<T>>> CreateRepoMock<T>(List<T> list) where T : class
        {
            var promotionRepoMock = new Mock<IRepository<T>>();

            promotionRepoMock.Setup(p => p.GetAll()).Returns(list);
            promotionRepoMock.Setup(p => p.GetAll(It.IsAny<Func<T, bool>>())).Returns((Func<T, bool> func) => list.Where(func).ToList());

            return await Task.FromResult(promotionRepoMock);
        }
    }
}