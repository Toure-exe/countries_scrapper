using Azure.Core;
using PageScrapper.Infrastructure;

namespace PageScrapper.API.Middlewares
{
    public class LocalizationMiddleware : IMiddleware
    {
        private readonly IIPAddressService _iPAddressService;

        public LocalizationMiddleware(IIPAddressService iPAddressService)
        {
            _iPAddressService = iPAddressService;
        }

        /*
         * il valore di remoteIpAddress (riga 23) è giusto, pero' siccome siamo in locale ritorna
         * sempre l'indirizzo ip locale, quindi per il test non va usato
         * 
         */
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine(_iPAddressService.GetLocalIpAddress());
           // var remoteIpAddress = (context.Request.HttpContext.Connection.RemoteIpAddress).ToString();
            Console.WriteLine(_iPAddressService.GetLocalizationFromAddress(_iPAddressService.getInetAddress()));
            next(context);
            return Task.CompletedTask;
        }
    }
}
