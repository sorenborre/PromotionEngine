using System;

namespace PromotionEngine
{
    public class NegativeNumberNotAllowedException : Exception
    {
        public NegativeNumberNotAllowedException(string message) : base(message)
        {

        }
    }
}
