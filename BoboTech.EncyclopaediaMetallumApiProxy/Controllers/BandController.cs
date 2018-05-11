using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace BoboTech.EncyclopaediaMetallumApiProxy.Controllers
{
    [Route("api/[controller]")]
    public class BandController : BaseController
    {
        public BandController(IOptions<Settings.EncyclopaediaMetallum> settings) : base(settings) { }

        /// <summary>
        /// GET: api/band/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetById")]
        public Task<Stream> Get(long id) => GetStreamAsync($"/band/{id}");
    }
}