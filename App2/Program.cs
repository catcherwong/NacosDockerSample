namespace App2
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            // Don't use HostCreateDefaultBuilder(args)
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
