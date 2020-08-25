using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    interface IGetSentence
    {
        public Word[] GetWordListFromRemote(string[] urls);
    }
}
