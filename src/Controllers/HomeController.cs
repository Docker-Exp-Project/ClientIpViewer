using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging;
using ClientIpViewer.Models;
using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ClientIpViewer.Extension;

namespace ClientIpViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var ipAddress = Request.Headers["X-Client-IP"].ToString();
            var accessor = GetRequestIP();
            ViewBag.IpAddress = ipAddress;
            ViewBag.HostIpAddress = GetUserIP();
            ViewBag.ClientRemoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void GetHeaders()
        {
            var realIP = Request.Headers["X-Real-IP"];
            var realForwardFor = Request.Headers["X-Forwarded-For"];
            var forwardedProto = Request.Headers["X-Forwarded-Proto"];
        }

        private string GetUserIP()
        {
            GetHeaders();
            var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
            return feature?.RemoteIpAddress?.ToString();
        }
        public string GetHostIP()
        {
            string Str = "";
            Str = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }
        public string GetRequestIP()
        {
            string ip = GetHeaderValueAs<string>("X-Forwarded-For").SplitCsv().FirstOrDefault();
            var remoteip = GetHeaderValueAs<string>("REMOTE_ADDR");
            return ip;
        }

        public T GetHeaderValueAs<T>(string headerName)
        {
            string svalues = _httpContextAccessor.HttpContext?.Request?.Headers?[headerName];
            return (T)Convert.ChangeType(svalues, typeof(T));

            // if (_httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValue(headerName, out svalues) ?? false)
            // {
            //     string rawValues = svalues.ToString();

            //     if (!rawValues.IsNullOrWhitespace())
            //         return (T)Convert.ChangeType(svalues.ToString(), typeof(T));
            // }
            // return default(T);
        }
    }
}
