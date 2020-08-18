using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace WebApp
{
    public class Word
    {
        private HttpStatusCode StatusCode { get; set; }
        private string WordString { get; set; }
        private string GetFrom { get; set; }
        private string HeaderString { get; set; }

        public Word(HttpStatusCode statusCode, string word, string getFrom, string headerString)
        {
            this.StatusCode = statusCode;
            this.WordString = word;
            this.GetFrom = getFrom;
            this.HeaderString = headerString;
        }

        public string CreateLine()
        {
            string line = "";
            if (StatusCode == HttpStatusCode.OK)
            {
                line = WordString + " Received from: " + HeaderString + '\n';
            }
            else
            {
                line = "Nothing received from " + GetFrom + " because of error " + Convert.ToString(StatusCode) + '\n';
            }
            return line;
        }

        public static string CreateSentence(params Word[] words)
        {
            string sentence = "";
            string description = "";
            foreach (Word word in words)
            {
                sentence += word.WordString + " ";
                description += word.CreateLine();
            }
            return sentence + '\n' + description + '\n';
        }

        public static Word GetWordFromRemote(string url)
        {
            string wordResult = "";
            string headerResult = "";
            string getFrom = "";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            getFrom = url;
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
                    statusCode = HttpStatusCode.OK;


                }
                response.Close();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError && e.Response != null)
                {
                    HttpWebResponse failedResponse = (HttpWebResponse)e.Response;
                    if (failedResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        wordResult = " ";
                        headerResult = " ";
                        statusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        wordResult = " ";
                        headerResult = " ";
                        statusCode = failedResponse.StatusCode;
                    }
                    failedResponse.Close();
                }
                else
                {
                    wordResult = " ";
                    headerResult = " ";
                    statusCode = HttpStatusCode.InternalServerError;
                }
            }
            Word word = new Word(statusCode, wordResult, getFrom, headerResult);
            return word;
        }


    }
}
