using Microsoft.EntityFrameworkCore;
using Tables.Models.Entities;

namespace Tables.Data
{
    public class AppDbContext : DbContext // наследует класс DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // конструктор класса AppDbContext
        {
            
        }

        public DbSet<Client> Clients { get; set; } // создание таблицы клиентов

        public DbSet<Car> Cars { get; set; } // создание таблицы автомобилей
    }
}
