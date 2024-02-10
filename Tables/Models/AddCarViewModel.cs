namespace Tables.Models
{
    public class AddCarViewModel
    {
        public Guid ClientId { get; set; } // Добавьте это свойство
        public string Brand { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
    }
}
