namespace Tables.Models.Entities
{
    public class Client // модель для таблицы clients, для описания конкретного клиента
    {
        public Guid Id { get; set; } // идентификатор клиента, Guid - уникальный идентификатор
        public string Name { get; set; } // имя клиента
        public string Email { get; set; } 
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Car> Cars { get; set; }  // Коллекция автомобилей клиента
    }
}
