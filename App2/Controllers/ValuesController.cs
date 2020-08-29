namespace App2.Controllers
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Nacos.AspNetCore;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly INacosServerManager _serverManager;

        public ValuesController(ILoggerFactory loggerFactory, INacosServerManager serverManager)
        {
            _logger = loggerFactory.CreateLogger<ValuesController>();
            _serverManager = serverManager;
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
            var baseUrl = _serverManager.GetServerAsync("App1").GetAwaiter().GetResult();

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return "empty";
            }

            var url = $"{baseUrl}/api/values";

            _logger.LogInformation("sending reqesut to App2, the url is {0}", url);

            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync(url).GetAwaiter().GetResult();
                return result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }
    }
}
