using EntityFramework_Slider.Data;
using EntityFramework_Slider.Services;
using EntityFramework_Slider.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //service cooki ile bagli method yazanda bunu yazmaliyiq


builder.Services.AddScoped<ILayoutService,LayoutService>();   ///servicleri burda addScoped edirik

builder.Services.AddScoped<IBasketService, BasketService>(); ///servicleri burda addScoped edirik

builder.Services.AddScoped<IProductService, ProductService>(); ///servicleri burda addScoped edirik

builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ISliderService, SliderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
