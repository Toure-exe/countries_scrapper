using HtmlAgilityPack;
using Newtonsoft.Json;
using PageScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PageScrapper.Infrastructure
{
    public class IPAddressService : IIPAddressService
    {
        private string address;

       private readonly ILocalizationRepository _localizationRepository;
    

        public IPAddressService(ILocalizationRepository localizationRepository)
        {
            _localizationRepository = localizationRepository;
        }
        public IPAddressService()
        {
            this.address = string.Empty;
        }

        public string getInetAddress()
        {
            return this.address;
        }
        public string GetLocalIpAddress()
        {
            this.address = GetMyPublicAddressAsync().Result;
            return this.address;
        }

        public string? GetLocalizationFromAddress(string address)
        {
            Localization locInfo = new Localization();
            try
            {
                
                string info = new WebClient().DownloadString("http://ipinfo.io/" + address);
                locInfo = JsonConvert.DeserializeObject<Localization>(info);
                RegionInfo myRI1 = new RegionInfo(locInfo.Country);
                locInfo.Country = myRI1.EnglishName;
                locInfo.ip_address = this.address;
                _localizationRepository.InsertLocalization(locInfo);
            }
            catch (Exception)
            {
                locInfo.Country = null;
            }

            return locInfo != null ? locInfo.Country : null;
        }

        private async Task<string?> GetMyPublicAddressAsync()
        {
            using var client = new HttpClient();
            string htmlCode = await client.GetStringAsync("http://checkip.dyndns.org/");
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlCode);
            HtmlNode addressNode = doc.DocumentNode.SelectSingleNode("/html/body");
            if (addressNode != null)
            {
                string temp = addressNode.InnerText.Trim();
                string[] result = temp.Split(":");
                return result[1].Trim();
            }
            else
            {
                return null;
            }
        }
    }
}
