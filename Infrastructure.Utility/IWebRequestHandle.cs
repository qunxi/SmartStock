using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public interface IWebRequestHandle
    {
        string GetHttpWebRequestNoWebException(string address, Encoding encoding);
        string GetHttpWebRequest(string address, Encoding encoding);
    }
}
