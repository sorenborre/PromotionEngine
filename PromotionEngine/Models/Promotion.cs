﻿
using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class Promotion
    {

        public Promotion(bool isActive, decimal price, Dictionary<char, int> requiredProductsToTrigger)
        {
            IsActive = isActive;
            Price = price;
            RequiredProductsToTrigger = requiredProductsToTrigger;
        }

        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public Dictionary<char, int> RequiredProductsToTrigger { get; set; }
    }
}