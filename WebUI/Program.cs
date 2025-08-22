using System.Net.Http.Headers;
using System.Text;
using WebUI.Components;
using WebUI.Resources;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Kullanýcý adý / þifre
            var username = "admin";
            var password = "1234";

            // Base64 encode
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            builder.Services.AddScoped(sp =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(Consts.API_URL)
                };
                client.DefaultRequestHeaders.Authorization = authHeader;
                return client;
            });

            //builder.Services.AddHttpClient();


            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
