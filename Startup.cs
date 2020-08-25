using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Diagnostics;



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
            if (Program.Keys.Length == 0)
            {
                strategy = new Async();
            }
            else if (Program.Keys[0] == "s")
            {
                strategy = new Sync();
            }
            else
            {
                strategy = new Async();
            }
            var watch = Stopwatch.StartNew();
            Word[] words = strategy.GetWordListFromRemote(urls);
            watch.Stop();
            string sentence = Word.CreateSentence(words);
            sentence += strategy.GetType()+ " Time" + Convert.ToString(watch.ElapsedMilliseconds);
            return sentence;

        }

        public void ConfigContext(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            context.Response.Headers.Add("InCamp-Student", Dns.GetHostName());
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
                     ConfigContext(context);
                     await context.Response.WriteAsync("MyWebApp!");
                 });

                 endpoints.MapGet("/who", async context =>
                 {
                     ConfigContext(context);
                     await context.Response.WriteAsync(GetRandomWord(who));
                 });

                 endpoints.MapGet("/how", async context =>
                 {
                     ConfigContext(context);
                     await context.Response.WriteAsync(GetRandomWord(how));
                 });

                 endpoints.MapGet("/does", async context =>
                 {
                     ConfigContext(context);
                     await context.Response.WriteAsync(GetRandomWord(does));
                 });

                 endpoints.MapGet("/what", async context =>
               {
                   ConfigContext(context);
                   await context.Response.WriteAsync(GetRandomWord(what));
               });

                 endpoints.MapGet("/quote", async context =>
               {
                   ConfigContext(context);
                   await context.Response.WriteAsync(GetRandomWord(who) + " " + GetRandomWord(how) + " " + GetRandomWord(does) + " " + GetRandomWord(what));
               });

                 endpoints.MapGet("/incamp18-quote", async context =>
               {
                   ConfigContext(context);
                   var urls = Environment.GetEnvironmentVariable("urls_list").Split(" ");
                   await context.Response.WriteAsync(ChooseStrategy(urls));
               });
             });
        }
    }
}
