using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WebApp
{
    public class Sync : IGetSentence
    {
        public Word[] GetWordListFromRemote(string[] urls) 
        {
            Word who = Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "who");
            Word how = Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "how");
            Word does = Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "does");
            Word what = Word.GetWordFromRemote(Startup.GetRandomWord(urls) + "what");
            Word[] words = { who, how, does, what };
            return words;
            
        }
    }
}
