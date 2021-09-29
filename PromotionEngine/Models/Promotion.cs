using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    public class Promotion
    {
        public Promotion(bool isActive, decimal price, Dictionary<char, int> requiredUnitsToTrigger)
        {
            IsActive = isActive;
            Price = price;
            RequiredUnitsToTrigger = requiredUnitsToTrigger;
        }

        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public Dictionary<char, int> RequiredUnitsToTrigger { get; set; }
    }
}
