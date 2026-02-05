

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryDB")));
builder.Services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Product}/{action=Index}/{id?}");
app.Run();
