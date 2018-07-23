using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTokenPropagate.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SampleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SampleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        [HttpGet("resource")]
        public async Task<IActionResult> GetResource()
        {
            var client = _httpClientFactory.CreateClient("AuthorizedClient");

            return Ok(await client.GetStringAsync(Url.Action("GetPropagated", null, null, Request.Scheme)));
        }
        
        [HttpGet("propagated")]
        public IActionResult GetPropagated()
        {
            return Ok($"Propagated for user: {User.FindFirst(ClaimTypes.Name).Value}");
        }
    }
}
