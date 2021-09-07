using PromotionEngine.Models;
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
            var result = sut.CalculateTotalSkuPrice(amount, price);

            Assert.Equal(expected, result);
        }
    }
}
