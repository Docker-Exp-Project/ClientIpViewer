using System;
using Microsoft.AspNetCore.Builder;

namespace ClientIpViewer.Extension
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyRequestDelegate>();
        }
    }
}
