﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageScrapper
{
    public interface IDownloaderService
    {
        public Task<string> DownloadHtmlAsync(string targetUrl);
    }
}
