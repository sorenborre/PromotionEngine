using PromotionEngine.Models;
using System.Collections.Generic;
using Xunit;

namespace PromotionEngine.Tests
{
    public class PriceCalculatorTests
    {
        private readonly PriceCalculator _sut = new();

        public PriceCalculatorTests(PriceCalculator sut)
        {
            _sut = sut;
        }

        [Theory]
        [InlineData(1, 1, 1, 0, 100)] //Scenario A. No active promotions
        [InlineData(5, 5, 1, 0, 370)] //Scenario B. Active promotions, buy 'n' items of a SKU for a fixed price 
        [InlineData(3, 5, 1, 1, 280)] //Scenario C. Active promotions, buy 'n' items of a SKU for a fixed price AND buy SKU 1 & SKU 2 for a fixed price
        public void CalculateTotalPrice_should_add_prices_using_active_promotions(
            int amountA,
            int amountB,
            int amountC,
            int amountD,
            decimal expected)
        {
            //arange
            List<StockKeepingUnitOrder> stockKeepingUnitOrders = new()
            {
                new StockKeepingUnitOrder(new StockKeepingUnit('A', 50), amountA),
                new StockKeepingUnitOrder(new StockKeepingUnit('B', 30), amountB),
                new StockKeepingUnitOrder(new StockKeepingUnit('C', 20), amountC),
                new StockKeepingUnitOrder(new StockKeepingUnit('D', 15), amountD),
            };

            //act
            decimal result = _sut.CalculateTotalPrice(stockKeepingUnitOrders);

            //assert
            Assert.Equal(expected, result);
        }
    }
}