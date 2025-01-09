using cryptoanalyzer;
using cryptoanalyzer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<Worker>();
//builder.Services.AddHostedService<MLWorker>();//برای این یه دیتا بیس جدید تعریف می کنم 
var cnnString = builder.Configuration.GetConnectionString("StoreCnn");
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreCryptos>(options => options.UseSqlServer(cnnString));
builder.Services.AddScoped<ICryptoRepository,EFCryptoRepository>();
var app = builder.Build();
//midle wears
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();


app.UseRouting();


app. UseEndpoints(endpoints =>endpoints.MapDefaultControllerRoute());
// how it work "{controller=home}/{action=index}/{id}"
//               {parameter name = default value}
app.Run();
