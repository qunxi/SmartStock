using System;
using System.IO;
using System.Net;
using System.Text;
using Infrastructure.Utility;
using Infrastructure.Utility.Logging;

namespace infrastructure.Utility
{
    public class WebRequestHandle : IWebRequestHandle
    {
        private readonly ILogger logger;

        public WebRequestHandle(ILogger logger)
        {
            this.logger = logger;
        }

        public string GetHttpWebRequestNoWebException(string address, Encoding encoding)
        {
            string htmlContent = string.Empty;

            while (true)
            {
                try
                {
                    Uri uri = new Uri(address);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                    request.Proxy = null; // to speed up to access the website
                    request.Timeout = 30 * 1000;
                    request.ReadWriteTimeout = 30 * 1000;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        Stream recvStream = response.GetResponseStream(); //use bufferedStream also to seed up
                        if (recvStream == null)
                            return htmlContent;

                        using (BufferedStream buffer = new BufferedStream(recvStream))
                        {
                            using (StreamReader reader = new StreamReader(buffer, encoding))
                            {
                                htmlContent = reader.ReadToEnd();
                                break;
                            }
                        }
                    }
                }
                catch (WebException e)
                {
                    this.logger.Error(string.Format("HttpWebRequest throw an Exception: {0}, address is{1}", e.Message, address));
                }
            }
           
            return htmlContent;
        }

        public string GetHttpWebRequest(string address, Encoding encoding)
        {
            Uri uri = new Uri(address);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Proxy = null; // to speed up to access the website
            request.Timeout = 30*1000;
            request.ReadWriteTimeout = 30*1000;
            string htmlContent = string.Empty;
            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream recvStream = response.GetResponseStream(); //use bufferedStream also to seed up
                if (recvStream == null) 
                    return htmlContent;

                using (BufferedStream buffer = new BufferedStream(recvStream))
                {
                    using (StreamReader reader = new StreamReader(buffer, encoding))
                    {
                        htmlContent = reader.ReadToEnd();
                    }
                }
            }
           
            return htmlContent;
        }
    }
}
