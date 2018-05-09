using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace BoboTech.EncyclopaediaMetallumApiProxy.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : BaseController
    {
        public SearchController(IOptions<Settings.EncyclopaediaMetallum> settings) : base(settings) { }

        // GET: api/search/{searchType}/{keyword}
        [HttpGet("{searchType}/{keyword}", Name = "Search")]
        public Task<Stream> Get(string searchType, string keyword) => GetStreamAsync($"/search/{searchType}/{keyword}");
    }
}