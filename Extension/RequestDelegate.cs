using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ClientIpViewer.Extension
{
    public class MyRequestDelegate
    {
        private readonly RequestDelegate _next;

        public MyRequestDelegate(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var headers = context.Request.Headers;
            await _next(context);
        }
    }
}