using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class PromotionPriceCalculator
    {

        public decimal CalculateTotalSkuPrice(int amount, decimal price) 
        {
            if (amount < 0 || price < 0)
                throw new NegativeNumberNotAllowedException($"One or more numbers in {nameof(CalculateTotalSkuPrice)} parameter has a negative value. amount:{amount}, price: {price}");

            return amount * price;
        }
    }
}
