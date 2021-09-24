using PromotionEngine.Models;
using PromotionEngine.Persistance;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PromotionEngine.Tests
{
    public class PriceCalculatorTests
    {
        private readonly PriceCalculator _sut;
        private readonly IRepository<StockKeepingUnit> _Skurepo;
        private readonly IRepository<Promotion> _promotionRepo;

        private readonly Dictionary<char, StockKeepingUnit> _stockKeepingUnits;
        private readonly List<Promotion> _promotions;

        public PriceCalculatorTests()
        {
            _sut = new();
            _promotionRepo = new PromoRepo();
            _Skurepo = new SkuRepo();
            _stockKeepingUnits = _Skurepo.GetAll().ToDictionary(Sku => Sku.Id, Sku => Sku);
            _promotions = _promotionRepo.GetAll();
        }

        [Theory]
        [InlineData(1, 1, 1, 0, 100)] //Scenario A. No active promotions
        [InlineData(5, 5, 1, 0, 370)] //Scenario B. Active promotions, buy 'n' items of a SKU for a fixed price 
        [InlineData(3, 5, 1, 1, 280)] //Scenario C. Active promotions, buy 'n' items of a SKU for a fixed price AND buy SKU 1 & SKU 2 for a fixed price
        [InlineData(10, 10, 11, 10, 985)] //Scenario D. Active promotions, buy 'n' items of a SKU for a fixed price AND buy SKU 1 & SKU 2 for a fixed price

        public void CalculateTotalPrice_should_add_prices_using_active_promotions(
            int amountA,
            int amountB,
            int amountC,
            int amountD,
            decimal expected)
        {
            
            //arange
            Dictionary<char, StockKeepingUnitOrder> stockKeepingUnitOrders = new()
            {
                { 'A', new StockKeepingUnitOrder(_stockKeepingUnits['A'], amountA) },
                { 'B', new StockKeepingUnitOrder(_stockKeepingUnits['B'], amountB) },
                { 'C', new StockKeepingUnitOrder(_stockKeepingUnits['C'], amountC) },
                { 'D', new StockKeepingUnitOrder(_stockKeepingUnits['D'], amountD) }
            };


            //act
            decimal result = _sut.CalculateTotalPrice(stockKeepingUnitOrders, _promotions);

            //assert
            Assert.Equal(expected, result);
        }
    }
}