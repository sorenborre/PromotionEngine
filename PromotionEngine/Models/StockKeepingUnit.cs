namespace PromotionEngine.Models
{
    public class StockKeepingUnit
    {
        public StockKeepingUnit(char id, decimal price)
        {
            Id = id;
            Price = price;
        }

        public char Id { get; }
        public decimal Price { get; }
    }
}
