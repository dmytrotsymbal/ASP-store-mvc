using Microsoft.EntityFrameworkCore;
using Tables.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// –егистраци€ контекста базы данных AppDbContext в сервисах приложени€,
// позвол€юща€ внедр€ть зависимости контекста в другие части приложени€.
//  онфигураци€ контекста дл€ использовани€ SQL Server с использованием строки подключени€,
// определенной в файле конфигурации приложени€ (обычно appsettings.json).
builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
