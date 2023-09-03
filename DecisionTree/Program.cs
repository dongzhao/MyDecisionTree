using DecisionTree.MVC.Application.Document;
using DecisionTree.MVC.Infrastructure;
using DecisionTree.MVC.Infrastructure.DAL;
using DecisionTree.MVC.Infrastructure.Repositories;
using DecisionTree.MVC.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: "appsettings.Development.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddDbContext<AppDbContext>(d => d.UseSqlServer(config.GetConnectionString(nameof(AppDbContext))));
//builder.Services.AddDbContext<AppDbContext>(d => d.UseInMemoryDatabase(nameof(AppDbContext)));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// add dependency of generic repositories
builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
// add dependency of individual extended repository
builder.Services.AddScoped(typeof(IHierarchyItemRepository), typeof(HierarchyItemRepository));

builder.Services.AddScoped(typeof(IDocumentService), typeof(DocumentService));

// Add services to the container.
var newBuilder = builder.Services.AddControllersWithViews();

#if DEBUG
//  enable runtime compilation 
newBuilder.AddRazorRuntimeCompilation(); 
#endif

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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
