using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WebApp
{
    public class Sync : IGetSentence
    {
        public string GetSentenceFromRemote(string[] urls)
        {
            var watch = Stopwatch.StartNew();
            Word who = Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "who");
            Word how = Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "how");
            Word does = Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "does");
            Word what = Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "what");
            watch.Stop();
            string sentence = Word.CreateSentence(who, how, does, what);
            return sentence + " Time: " + Convert.ToString(watch.ElapsedMilliseconds);
        }
    }
}
