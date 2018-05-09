using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoboTech.EncyclopaediaMetallumApiProxy.Controllers
{
    public class BaseController : Controller
    {
        IOptions<Settings.EncyclopaediaMetallum> Settings { get; }

        public BaseController(IOptions<Settings.EncyclopaediaMetallum> settings) => Settings = settings;

        protected async Task<Stream> GetStreamAsync(string query)
        {
            var memoryStream = new MemoryStream();
            using (var client = new HttpClient())
            {
                var stream = await client.GetStreamAsync(new Uri($"{Settings.Value.BaseUrl}{query}?api_key={Settings.Value.ApiKey}"));
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}