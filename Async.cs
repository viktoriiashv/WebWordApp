using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WebApp
{
    public class Async : IGetSentence
    {
        public async Task<string> AsyncGetSentenceFromRemote(string[] urls)
        {
            var watch = Stopwatch.StartNew();
            Task<Word> who = Task.Run(() =>
            {
                return Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "who");
            });
            Task<Word> how = Task.Run(() =>
            {
                return Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "how");
            });
            Task<Word> does = Task.Run(() =>
            {
                return Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "does");
            });
            Task<Word> what = Task.Run(() =>
            {
                return Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "what");
            });

            var words = await Task.WhenAll(who, how, does, what);
            watch.Stop();
            string sentence = Word.CreateSentence(words);
            return sentence + " Async Time: " + Convert.ToString(watch.ElapsedMilliseconds);

        }
        public string GetSentenceFromRemote(string[] urls)
        {
            return AsyncGetSentenceFromRemote(urls).Result;
        }
    }
}
