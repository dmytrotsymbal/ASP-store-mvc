using Microsoft.AspNetCore.Mvc; // Импортирует базовые компоненты ASP.NET Core MVC для контроллеров и действий.
using Microsoft.EntityFrameworkCore;
using Tables.Data; // Импортирует пространство имен, где определен контекст базы данных (AppDbContext).
using Tables.Models; // Импортирует пространство имен, где могут быть определены модели представления.
using Tables.Models.Entities; // Импортирует пространство имен, где определены сущности, используемые в приложении (например, Client).

namespace Tables.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext dbContext; // переменная для доступа к контексту базы данных.

        public ClientsController(AppDbContext dbContext)  // Конструктор, создающий контекст базы данных
        {
            this.dbContext = dbContext; // Сохраняет предоставленный контекст базы данных в переменную экземпляра.
        }

        [HttpGet] // Метод для отображения формы добавления клиента.
                  // [HttpGet] указывает на то, что он обрабатывает GET-запросы.
        public IActionResult Add() // Возвращает представление для добавления нового клиента.
        {
            return View(); // метод View() для отображения представления
        }


        //CREATE A NEW CLIENT FUNCTION============================================================================

        [HttpPost] // Метод для отправки формы и добавления клиента.
                   // [HttpPost] указывает на то, что он обрабатывает POST-запросы.
        public async Task<IActionResult> Add(AddClientViewModel viewModel) // Принимает модель представления с данными формы. То есть наш ViewModel
        {
            var client = new Client // Создает новый экземпляр клиента с данными из модели представления. ViewModel
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Address = viewModel.Address
            };

            await dbContext.Clients.AddAsync(client); // Добавляет нового клиента в контекст базы данных для последующего сохранения.
            await dbContext.SaveChangesAsync(); // Асинхронно сохраняет изменения в базе данных.

            return View(); // Возвращает представление после добавления клиента.
                           // Можно изменить на редирект, если требуется перенаправление после добавления.
        }



        //LIST FUNCTION============================================================================
        [HttpGet]
        public async Task<IActionResult> List() // Метод для отображения списка клиентов.
        {
           var client = await dbContext.Clients.ToListAsync(); // Загружает список всех клиентов из базы данных.

            return View(client); // Возвращает представление со списком клиентов.
        }


        //EDIT FUNCTION============================================================================

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) // Метод для отображения формы редактирования клиента.
        {
            var client = await dbContext.Clients.FindAsync(id); // Загружает клиента с указанным идентификатором из базы данных.

            return View(client); // Возвращает представление с формой редактирования клиента.
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Client viewModel) // Метод для обновления данных клиента.
        {
            var client = await dbContext.Clients.FindAsync(viewModel.Id); // Загружает клиента с указанным идентификатором из базы данных.

            if (client is not null) // Если клиент существует
            {
                // Обновляет данные клиента
                client.Name = viewModel.Name;
                client.Email = viewModel.Email;
                client.Phone = viewModel.Phone;
                client.Address = viewModel.Address;

                await dbContext.SaveChangesAsync(); // Сохраняет изменения в базе данных.
            }

            return RedirectToAction("List", "Clients"); // Возвращает представление со списком клиентов.
        } 
    }
}
