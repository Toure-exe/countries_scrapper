using PageScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageScrapper.Infrastructure
{
    public interface ILocalizationRepository
    {
        public void InsertAddress(string address);

        public void InsertLocalization(Localization loc);


    }
}
