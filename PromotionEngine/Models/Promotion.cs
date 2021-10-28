using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class Promotion
    {
        /// <summary>
        /// For fixed promotions value represents the total price of the promotion. For Percentage promotions value represents the percentage discount. 
        /// </summary>
        /// <param name="value">
        public Promotion(PromotionType type, bool isActive, decimal value, Dictionary<char, int> requiredUnitsToTrigger)
        {
            Type = type;
            IsActive = isActive;
            Value = value;
            RequiredUnitsToTrigger = requiredUnitsToTrigger;
        }

        public PromotionType Type { get; }
        public bool IsActive { get; set; }
        public decimal Value { get; }
        public Dictionary<char, int> RequiredUnitsToTrigger { get; }
    }
}
