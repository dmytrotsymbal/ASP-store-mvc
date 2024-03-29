﻿using Microsoft.AspNetCore.Mvc; // Импортирует базовые компоненты ASP.NET Core MVC для контроллеров и действий.
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


        //CREATE A NEW CLIENT FUNCTION============================================================================

        [HttpGet] // Метод для отображения формы добавления клиента.
                  // [HttpGet] указывает на то, что он обрабатывает GET-запросы.
        public IActionResult Add() // Возвращает представление для добавления нового клиента.
                                   // Возвращает представление Add.cshtml
        {
            return View(); // метод View() для отображения представления
        }


        [HttpPost] // Метод для отправки формы и добавления клиента.
                   // [HttpPost] указывает на то, что он обрабатывает POST-запросы.
        public async Task<IActionResult> Add(AddClientViewModel viewModel) // Принимает модель представления с данными формы.
                                                                           // То есть наш ViewModel
        {
            var client = new Client // Создает новый экземпляр клиента с данными из модели представления. ViewModel
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Address = viewModel.Address,
                CreatedAt = DateTime.Now
            };

            await dbContext.Clients.AddAsync(client); // Добавляет нового клиента в контекст базы данных для последующего сохранения.
            await dbContext.SaveChangesAsync(); // Асинхронно сохраняет изменения в базе данных.

            return RedirectToAction("List");

            // return View(); // Возвращает представление после добавления клиента.
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


        //DELETE FUNCTION============================================================================
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id) // Метод для удаления клиента.
        {
            var client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id); // Загружает клиента с указанным идентификатором из базы данных.

            if (client != null) // Если клиент существует
            { 
                dbContext.Clients.Remove(client); // Удаляет клиента из базы данных.
                await dbContext.SaveChangesAsync(); // Сохраняет изменения в базе данных.
            }

            return RedirectToAction("List"); // Возвращает представление со списком клиентов.
        }


        //SEARCH FUNCTION============================================================================

        [HttpGet]
        public IActionResult Search() // Метод для отображения формы поиска.
        {
            return View(new SearchClientViewModel());  // ViewModel с пустым списком результатов,
                                                       // чтобы избежать ошибок в представлении
        }


        [HttpPost]
        public async Task<IActionResult> Search(SearchClientViewModel searchModel) // Метод для поиска клиента.
        {
            var query = dbContext.Clients.AsQueryable(); // Загружает список всех клиентов из базы данных.

            if (searchModel.Id != Guid.Empty) // Если Id не пустой
            {
                query = query.Where(x => x.Id == searchModel.Id); // Фильтрует список клиентов по Id.
            }

            if (!string.IsNullOrEmpty(searchModel.Name)) // Если имя не пустое
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name)); // Фильтрует список клиентов по имени.
            }

            if (!string.IsNullOrEmpty(searchModel.Email)) // Если почта не пустая 
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email)); // Фильтрует список клиентов по почте.
            }

            if (!string.IsNullOrEmpty(searchModel.Phone)) // Если телефон не пустой 
            {
                query = query.Where(x => x.Phone.Contains(searchModel.Phone)); // Фильтрует список клиентов по телефону.
            }

            if (!string.IsNullOrEmpty(searchModel.Address)) // Если адрес не пустой
            {
                query = query.Where(x => x.Address.Contains(searchModel.Address)); // Фильтрует список клиентов по адресу.
            }
           
            searchModel.SearchResults = await query.ToListAsync();  // Загрузка и сохранение результатов поиска в ViewModel

            return View(searchModel); // Возвращение обновленной ViewModel в вид
        }

        //CLIENTS DETAILS PAGE FUNCTION============================================================================

        [HttpGet]
        public async Task<IActionResult> Details(Guid id) // Метод для отображения деталей клиента.
        {
            var client = await dbContext.Clients 
                .Include(c => c.Cars) // Предполагается, что у вас есть навигационное свойство Cars в модели Client
                .FirstOrDefaultAsync(c => c.Id == id); // Загружает клиента с указанным идентификатором из базы данных.

            if (client == null) // Если клиент не существует
            {
                return NotFound(); // Возвращает ошибку 404
            }

            var viewModel = new ClientDetailsViewModel 
            {
                Client = client,
                Cars = client.Cars.ToList() // Предполагается, что Cars - это коллекция в Client
            };

            return View(viewModel);
        }

        // CREATE CAR FUNCTION============================================================================

        [HttpGet]
        public IActionResult AddCar(Guid clientId) // Принимает clientId из URL
        {
            var carViewModel = new AddCarViewModel
            {
                ClientId = clientId // Устанавливает clientId в модели представления
            };
            return View(carViewModel);
        }



        [HttpPost] // Метод для отправки формы и добавления клиента.
                   // [HttpPost] указывает на то, что он обрабатывает POST-запросы.
        public async Task<IActionResult> AddCar(AddCarViewModel carViewModel) // Принимает модель представления с данными формы.
                                                                           // То есть наш ViewModel
        {
            var car = new Car // Создает новый экземпляр клиента с данными из модели представления. ViewModel
            {
                ClientId = carViewModel.ClientId,
                Brand = carViewModel.Brand,
                Color = carViewModel.Color,
                Year = carViewModel.Year,
                Price = carViewModel.Price,
            };

            await dbContext.Cars.AddAsync(car); // Добавляет нового клиента в контекст базы данных для последующего сохранения.
            await dbContext.SaveChangesAsync(); // Асинхронно сохраняет изменения в базе данных.

            return RedirectToAction("Details", new { id = carViewModel.ClientId });

            // return View(); // Возвращает представление после добавления клиента.
            // Можно изменить на редирект, если требуется перенаправление после добавления.
        }
    }
}
