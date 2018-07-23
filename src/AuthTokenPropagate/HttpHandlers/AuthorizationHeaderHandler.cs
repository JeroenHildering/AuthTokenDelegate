using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace AuthTokenPropagate.HttpHandlers
{
public class AuthorizationHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authHeader))
        {
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
}
