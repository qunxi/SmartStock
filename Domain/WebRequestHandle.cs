using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class WebRequestHandle
    {
        protected string GetHttpWebRequest(string address)
        {
            Uri uri = new Uri(address);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream recvStream = response.GetResponseStream();
            string htmlContent = string.Empty;
            if (recvStream != null)
            {
                StreamReader readStream = new StreamReader(recvStream, System.Text.Encoding.GetEncoding("GBK"));
                htmlContent = readStream.ReadToEnd();
            }

            return htmlContent;
        }
    }
}
