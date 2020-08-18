using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        string[] who = { "Бодя", "Вика", "Кот", "Собака", "Программист" };
        string[] how = { "Хорошо", "Мило", "Идеально", "Плохо", "Блестяще" };
        string[] does = { "Танцует", "Ест", "Работает", "Пишет", "Убивает" };
        string[] what = { "Процесс", "Борщ", "Макарену", "Код", "Мемы" };


        public void ConfigureServices(IServiceCollection services)
        {
        }
        public static string GetRandomWord(string[] words)
        {
            Random r = new Random();
            string result = words[r.Next(0, words.Length)];
            return result;
        }


        public string ChooseStrategy(string[] urls)
        {
            IGetSentence strategy;
            if(Program.Keys.Length == 0)
            {
                strategy = new Sync();
            }
            else if(Program.Keys[0] == "a")
            {
                strategy = new Async();
            }
            else
            {
                strategy = new Sync();
            }
            return strategy.GetSentenceFromRemote(urls);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapGet("/", async context =>
                 {
                     context.Response.ContentType = "text/html; charset=utf-8";
                     context.Response.Headers.Add("InCamp-Student", "ShvetsViktoriia");
                     await context.Response.WriteAsync("MyWebApp!");
                 });

                 endpoints.MapGet("/who", async context =>
                 {
                     context.Response.ContentType = "text/html; charset=utf-8";
                     context.Response.Headers.Add("InCamp-Student", "ShvetsViktoriia");
                     await context.Response.WriteAsync(GetRandomWord(who));
                 });

                 endpoints.MapGet("/how", async context =>
                 {
                     context.Response.ContentType = "text/html; charset=utf-8";
                     context.Response.Headers.Add("InCamp-Student", "ShvetsViktoriia");
                     await context.Response.WriteAsync(GetRandomWord(how));
                 });

                 endpoints.MapGet("/does", async context =>
                 {
                     context.Response.ContentType = "text/html; charset=utf-8";
                     context.Response.Headers.Add("InCamp-Student", "ShvetsViktoriia");
                     await context.Response.WriteAsync(GetRandomWord(does));
                 });

                 endpoints.MapGet("/what", async context =>
               {
                   context.Response.ContentType = "text/html; charset=utf-8";
                   context.Response.Headers.Add("InCamp-Student", "ShvetsViktoriia");
                   await context.Response.WriteAsync(GetRandomWord(what));
               });

                 endpoints.MapGet("/quote", async context =>
               {
                   context.Response.ContentType = "text/html; charset=utf-8";
                   context.Response.Headers.Add("InCamp-Student", "ShvetsViktoriia");
                   await context.Response.WriteAsync(GetRandomWord(who) + " " + GetRandomWord(how) + " " + GetRandomWord(does) + " " + GetRandomWord(what));
               });

                 endpoints.MapGet("/incamp18-quote", async context =>
               {
                   context.Response.ContentType = "text/html; charset=utf-8";
                   context.Response.Headers.Add("InCamp-Student", "ShvetsViktoriia");
                   string[] urls = { "http://localhost:5000/" };
                   await context.Response.WriteAsync(ChooseStrategy(urls));
               });
             });
        }
    }
}
