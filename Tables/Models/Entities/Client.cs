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


      //  public virtual ICollection<Car> Cars { get; set; } // Navigation property to represent the relationship
                                                           // Это свойство навигации позволяет навигировать от клиента
                                                           // к его автомобилям
    }
}
