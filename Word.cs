﻿using Microsoft.AspNetCore.Diagnostics;
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
        private string Text { get; set; }//text
        private string SourceOrigin { get; set; }//source origin
        private string Header { get; set; }

        public Word(HttpStatusCode statusCode, string text, string sourceOrigin, string Header)
        {
            this.StatusCode = statusCode;
            this.Text = text;
            this.SourceOrigin = sourceOrigin;
            this.Header = Header;
        }

        public string CreateLine()
        {
            string line = "";
            if (StatusCode == HttpStatusCode.OK)
            {
                line = Text + " Received from: " + Header + '\n';
            }
            else
            {
                line = "Nothing received from " + SourceOrigin + " because of error " + Convert.ToString(StatusCode) + '\n';
            }
            return line;
        }

        public static string CreateSentence(params Word[] words)
        {
            string sentence = "";
            string description = "";
            foreach (Word word in words)
            {
                sentence += word.Text + " ";
                description += word.CreateLine();
            }
            return sentence + '\n' + description + '\n';
        }

        public static Word GetWordFromRemote(string url)
        {
            string wordResult = "";
            string headerResult = "";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string sourceOrigin = url;
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
            Word word = new Word(statusCode, wordResult, sourceOrigin, headerResult);
            return word;
        }


    }
}
