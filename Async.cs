using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WebApp
{
    public class Async : IGetSentence 
    {
        public async Task<List<Word>> AsyncGetWordListFromRemote(string[] urls)
        {
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
            List<Word> words = new List<Word>(await Task.WhenAll(who, how, does, what));
            
            return words;

        }
        public Word[] GetWordListFromRemote(string[] urls)
        {
            Word[] words = AsyncGetWordListFromRemote(urls).Result.ToArray();
            return words;
        }
    }
}
