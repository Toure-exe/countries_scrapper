using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PageScrapper
{
    public class DownloaderService : IDownloaderService
    {
        /* test link :https://www.scrapethissite.com/pages/simple/ */
        public string Url { get; set; }

        public DownloaderService() {

           Url = string.Empty;
        }

        public async Task<string> DownloadHtmlAsync(string targetUrl)
        {
            string result = "";


            using (HttpClient client = new HttpClient())
            {
                using HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(targetUrl, UriKind.Absolute);
                using HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            Url = targetUrl;
            return result;
        }
        
    }
}
