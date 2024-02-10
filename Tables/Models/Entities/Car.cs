namespace Tables.Models.Entities
{
    public class Car // Модель данных для таблицы Cars
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public Guid ClientId { get; set; } // Внешний ключ для связи с таблицей Clients
        public Client Client { get; set; } // Навигационное свойство
    }
}
