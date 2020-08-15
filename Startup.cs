using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Security.Policy;


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
        public string GetRandomWord(string[] words)
        {
            Random r = new Random();
            string result = words[r.Next(0, words.Length)];
            return result;
        }
        public string GetSentenceFromRemote(string[] urls)
        {
            string sentence = "";
            Tuple<string, string> who = GetWordFromRemote(GetRandomWord(urls) + "who");
            Tuple<string, string> how = GetWordFromRemote(GetRandomWord(urls) + "how");
            Tuple<string, string> does = GetWordFromRemote(GetRandomWord(urls) + "does");
            Tuple<string, string> what = GetWordFromRemote(GetRandomWord(urls) + "what");
            sentence += who.Item1 + " " + how.Item1 + " " + does.Item1 + " " + what.Item1 + "\n";
            sentence += who.Item1 + " Received from: " + who.Item2 + "\n";
            sentence += how.Item1 + " Received from: " + how.Item2 + "\n";
            sentence += does.Item1 + " Received from: " + does.Item2 + "\n";
            sentence += what.Item1 + " Received from: " + what.Item2 + "\n";
            return sentence;
        }
        public Tuple<string, string> GetWordFromRemote(string url)
        {
            string wordResult = "";
            string headerResult = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader stream = new StreamReader(
                    response.GetResponseStream(), Encoding.UTF8))
                    {
                        wordResult = stream.ReadToEnd();
                    }
                    headerResult = response.Headers["InCamp-Student"];

                }
                response.Close(); //we should use it because of restriction in response amount
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError && e.Response != null)
                {
                    HttpWebResponse failedResponse = (HttpWebResponse)e.Response;
                    if (failedResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        wordResult = "Nothing";
                        headerResult = url + " because "+ url + " not found "; // error 404
                    }
                    else
                    {
                        wordResult = "Nothing";
                        headerResult = url + " because of undefine error ";
                    }
                    
                }
                else
                {
                    wordResult = "Nothing";
                    headerResult = url + " because " + url + " throws server internal error "; // error 500
                }
            }
            Tuple<string, string> wordHeader = Tuple.Create(wordResult, headerResult);
            return wordHeader;
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
                   string[] urls = { "http://localhost:5002/", "http://localhost:8080/" };
                   await context.Response.WriteAsync(GetSentenceFromRemote(urls));
                   
               });
             });
        }
    }
}
