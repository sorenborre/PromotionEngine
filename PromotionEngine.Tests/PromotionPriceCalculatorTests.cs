using System;
using Xunit;

namespace PromotionEngine.Tests
{
    public class PromotionPriceCalculatorTests
    {


        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(10, 10, 100)]
        public void CalculateTotalSkuPrice_Should_Multiply_Amount_With_Price(int amount, decimal price, decimal expected)
        {
            //arange
            PromotionPriceCalculator sut = new();

            //act
            decimal result = sut.CalculateTotalSkuPrice(amount, price);

            //assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(-10, 10)]
        [InlineData(-1, -1)]
        [InlineData(-10, -10)]
        public void CalculateTotalSkuPrice_Should_Throw_Exception_On_Negative_values(int amount, decimal price)
        {
            //arange
            PromotionPriceCalculator sut = new();

            //assert
            Assert.Throws<NegativeNumberNotAllowedException>(()=> sut.CalculateTotalSkuPrice(amount, price));
        }
    }
}
