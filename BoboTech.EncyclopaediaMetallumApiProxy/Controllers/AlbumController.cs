using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace BoboTech.EncyclopaediaMetallumApiProxy.Controllers
{
    [Route("api/[controller]")]
    public class AlbumController : BaseController
    {
        public AlbumController(IOptions<Settings.EncyclopaediaMetallum> settings) : base(settings) { }

        /// <summary>
        /// GET: api/album/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<Stream> Get(long id) => GetStreamAsync($"/album/{id}");
    }
}