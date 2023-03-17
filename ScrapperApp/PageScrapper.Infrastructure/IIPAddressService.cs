using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageScrapper.Infrastructure
{
    public interface IIPAddressService
    {
        public string GetLocalIpAddress();

        public string GetLocalizationFromAddress(string address);

        public string getInetAddress();
    }
}
