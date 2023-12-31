using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;

namespace GeekShopping.Web
{
    public class Program
    {
    
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);            

            // Configuração para acessar o appsettings.json
            // var configuration = new ConfigurationBuilder()
            // .SetBasePath(builder.Environment.ContentRootPath)
            // .AddJsonFile("appsettings.json")
            // .Build();

            
            // builder.Services.AddHttpClient<IProductService, ProductServer>(
            //     c => c.BaseAddress = new Uri(configuration["ServiceUrls:ProductAPI"]!)
            // );


            // Add services to the container.
            builder.Services.AddHttpClient<IProductService, ProductServer>(
                c => c.BaseAddress = new(builder.Configuration["ServiceUrls:ProductAPI"]!)
            );

            builder.Services.AddControllersWithViews();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}