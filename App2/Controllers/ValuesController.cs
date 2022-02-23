namespace App2.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Net.Http;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly Nacos.V2.INacosNamingService _svc;

        public ValuesController(ILoggerFactory loggerFactory, Nacos.V2.INacosNamingService svc)
        {
            _logger = loggerFactory.CreateLogger<ValuesController>();
            _svc = svc;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "App2222222222222222222222222" };
        }

        // GET api/values/test
        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            var instance = _svc.SelectOneHealthyInstance("App1", "DEFAULT_GROUP").GetAwaiter().GetResult();
            var host = $"{instance.Ip}:{instance.Port}";
            var baseUrl = instance.Metadata.TryGetValue("secure", out _)
                ? $"https://{host}"
                : $"http://{host}";

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return "empty";
            }

            var url = $"{baseUrl}/api/values";

            _logger.LogInformation("sending reqesut to App1, the url is {0}", url);

            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync(url).GetAwaiter().GetResult();
                return result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }
    }
}
