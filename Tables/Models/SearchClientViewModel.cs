using Tables.Models.Entities;

namespace Tables.Models
{
    public class SearchClientViewModel // модель представления для странички поиска
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Client> SearchResults { get; set; } = new List<Client>();  // список для хранения результатов поиска
    }

}
